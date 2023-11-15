using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GravityGun : MonoBehaviour
{
    //GameObject target;
    //bool isAttracting;
    //bool isLaunching;
    public Image reticle;
    FMODUnity.StudioEventEmitter eventEmitter;
    public float staminaDecrease = 10f;

    public StaminaController staminaController;

    public Camera cam;

    public Image img;

    public GameObject doorTrigger;
    private GameObject pianoKey;

    //public Transform boomerangTarget;
    //public Transform boomerang;

    public static bool isGrabbed = false;
    private bool isTurning = false;

    private float maxGrabDistance = 500f;
    public float maxGrabDistanceBase = 10;
    public float throwForce = 20f;
    public float lerpSpeed = 10f;

    public float boomerangTimer = 2f;
    public float returnForce = 20f;

    public Transform objectHolder;
    [HideInInspector] public Rigidbody grabbedRB;
    [HideInInspector] public GameObject grabbedObject;
    [HideInInspector] public PlayerController playerController;
    Rigidbody boomerangR;

    void Start()
    {
        //BoomerangTravel.isHit = false;
        //boomerangR = boomerang.GetComponent<Rigidbody>();
        //cam = Camera.main;
        playerController = GetComponent<PlayerController>();
        eventEmitter = GetComponent<FMODUnity.StudioEventEmitter>();
    }
    #region unused

    //private void Attract()
    //{
    //    RaycastHit hit;
    //    if (target == null)
    //    {
    //        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, maxGrabDistance))
    //        {
    //            if (hit.transform.tag == "CanGrab")
    //            {
    //                target = hit.transform.gameObject;
    //                grabbedRB = target.GetComponent<Rigidbody>();
    //                target.transform.position = Vector3.MoveTowards(target.transform.position, objectHolder.position, 0.03f);
    //                grabbedRB.useGravity = false;
    //            }
    //        }
    //    }
    //    else
    //    {
    //        target.transform.position = Vector3.MoveTowards(target.transform.position, objectHolder.position, 0.03f);
    //    }
    //}

    //private void FixedUpdate()
    //{
    //    if (isAttracting)
    //    {
    //        Attract();
    //    }
    //}
    #endregion
    void Update()
    {
        //Vector3 originalPos = objectHolder.transform.position;
        if (grabbedRB)
        {
            grabbedRB.MovePosition(Vector3.Lerp(grabbedRB.position, objectHolder.transform.position, Time.deltaTime * lerpSpeed));
            //For scroll wheel forward/backward
            //objectHolder.transform.position = objectHolder.transform.position + cam.transform.forward * Input.GetAxis("Mouse ScrollWheel") * 250 * Time.deltaTime;
            //grabbedObject.transform.rotation = Quaternion.identity;
            if (Input.GetMouseButton(0) && staminaController.stamina >= 10)
            {
                staminaController.UseStamina(staminaDecrease);
                grabbedRB.useGravity = true;
                isGrabbed = false;
               // grabbedRB.isKinematic = false;
                grabbedRB.AddForce(cam.transform.forward * throwForce, ForceMode.VelocityChange);
                if (grabbedRB.gameObject.name == "Boomerang")
                {
                    BoomerangTravel.isHit = false;
                    isTurning = true;
                    Boomerang();
                    Invoke("BoomerangReturn", boomerangTimer);
                }
                grabbedRB = null;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (grabbedRB)
            {
                //grabbedRB.isKinematic = false;
                grabbedRB.useGravity = true;
                grabbedRB = null;
                isGrabbed = false;
            }
            else
            {
                RaycastHit hit;
                Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
                Physics.Raycast(ray, out hit, maxGrabDistance);
                if (hit.collider.tag == "Light Object")
                {
                    float dist = Vector3.Distance(hit.transform.position, transform.position);
                    
                    if (hit.collider.name == "Jonathan" || dist <= maxGrabDistanceBase)
                    {
                        img.enabled = false;
                        isGrabbed = true;
                        grabbedRB = hit.collider.gameObject.GetComponent<Rigidbody>();
                        grabbedObject = hit.collider.gameObject;
                        if (grabbedRB)
                        {
                            //grabbedRB.isKinematic = true;
                            grabbedRB.useGravity = false;
                        }
                    }
                }
                if (hit.collider.tag == "Trigger")
                {
                    //BathroomDoorTrigger doorTrig = (BathroomDoorTrigger)doorTrigger.GetComponent(typeof(BathroomDoorTrigger));
                    //doorTrig.CloseDoor();
                    //Invoke("OpenDoor", 5f);
                    SceneManager.LoadScene("You Win");
                }
                if (hit.collider.tag == "PianoKeys")
                {
                    float dist2 = Vector3.Distance(hit.transform.position, transform.position);

                    if (dist2 <= maxGrabDistanceBase)
                    {
                        img.enabled = false;

                        pianoKey = hit.collider.gameObject;
                        Debug.Log(pianoKey.name);

                        pianoKey.GetComponent<MovePianoKey>().isKeyPressed = true;
                        pianoKey.GetComponent<MovePianoKey>().playSound = true;
                    }
                }
            }
        }
        #region Reticle Color
        RaycastHit hit2;
        Ray ray2 = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        Physics.Raycast(ray2, out hit2, maxGrabDistance);
        if (hit2.collider.tag == "Light Object")
        {
            reticle.color = Color.blue;
        }
        else if (hit2.collider.tag == "Heavy Object")
        {
            reticle.color = Color.red;
        }
        else
        {
            reticle.color = Color.white;
        }
        #endregion
    }
    void Boomerang()
    {
        if (isTurning)
        {
            grabbedRB.AddTorque(-grabbedRB.transform.right * throwForce * 100, ForceMode.Acceleration);
        }
    }

    private void BoomerangReturn()
    {
        if (!BoomerangTravel.isHit)
        {
            //boomerangR.AddForce((boomerangTarget.position - boomerang.transform.position) * returnForce, ForceMode.Acceleration);
        }
    }
    private void OpenDoor()
    {
        BathroomDoorTrigger doorTrig = (BathroomDoorTrigger)doorTrigger.GetComponent(typeof(BathroomDoorTrigger));
        doorTrig.OpenDoor();
    }

}
