using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public GameObject handsImage;
    [HideInInspector] public BigKickEnergy bigKickEnergy;
    [HideInInspector] public GravityGun gravityGun;
    [HideInInspector] public AudioManager audioManager;
    [HideInInspector] public HealthSystem healthSystem;
    [HideInInspector] public CharacterController characterController;
    public Transform groundCheck;
    public Transform groundCheck2;
    public LayerMask groundMask;
    GameObject walkTriggerCube;

    [Header("Variables")]
    public float crouchSpeed = 6;
    public float crouchHeight = 0.5f;
    public float standingHeight = 2f;
    public float timeToCrouch = 0.25f;
    [HideInInspector] public Vector3 crouchingCenter = new Vector3(0, 0.5f, 0);
    [HideInInspector] public Vector3 standingCenter = new Vector3(0, 0, 0);
    
    public float groundDistance = 0.4f;
    public float footstepTimer;
    public float speed = 12;
    public float speed2 = 12;
    public float gravity = -9.8f;
    public float jumpHeight = 3;

    [Header("Bools")]
    public bool isGrounded;
    public bool isGroundedAndCrouching;
    public bool isMoving;
    public bool isCrouching;
    public bool canCrouch = true;
    public bool duringCrouchAnimation;
    private bool ShouldCrouch => Input.GetKeyDown(KeyCode.LeftControl) && !duringCrouchAnimation ;

    Vector3 velocity;
    private int stepCount = 0;
    
    private void Start()
    {
        audioManager = GetComponent<AudioManager>();
        characterController = GetComponent<CharacterController>();
        bigKickEnergy = GetComponent<BigKickEnergy>();
        gravityGun = GetComponent<GravityGun>();
        healthSystem = GetComponent<HealthSystem>();
        handsImage = GameObject.Find("Hands");
        walkTriggerCube = gameObject.transform.GetChild(0).gameObject;
    }

    void Update()
    {
        

        //Grounded Conditions
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        isGroundedAndCrouching = Physics.CheckSphere(groundCheck2.position, groundDistance, groundMask);

        //Velocity Update When Grounded
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        ////Get Input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        //float x = Input.GetAxisRaw("Horizontal") * 100 * Time.deltaTime;
        //float z = Input.GetAxisRaw("Vertical") * 100 * Time.deltaTime;

        //Update Player Position
        Vector3 move = transform.right * x + transform.forward * z;
        characterController.Move(move * speed * Time.deltaTime);
       
        if (characterController.velocity.magnitude > 0 )
        {
            walkTriggerCube.SetActive(true);
            if (!isMoving)
            {
                //PlayFootSound();
            }
        }
        else
        {
            walkTriggerCube.SetActive(false);
        }
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);      
        if (canCrouch)
        {
            Crouch();
           
        }
        if (isCrouching)
        {
            #region unused
            //Vector3 targetRot = new Vector3(45, 0, 0);
            //float singleStep = speed * Time.deltaTime;
            //Vector3 newDirection = Vector3.RotateTowards(handsImage.transform.forward, targetRot, singleStep, 0);
            //transform.rotation = Quaternion.LookRotation(newDirection);
            #endregion

            if (Input.GetButtonDown("Jump") && isGroundedAndCrouching)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
            speed = crouchSpeed;
            bigKickEnergy.canKick = false;            
        }
        else
        {
            speed = speed2;
            bigKickEnergy.canKick = true;         
        }
        #region Unused
        //if (x != 0 && z != 0)
        //{

        //}

        //if (isMoving == true)
        //{
        //    string soundName;
        //    string Walk1 = "Foot1";
        //    string Walk2 = "Foot2";

        //    if (_stepCount == 0)
        //    {
        //        soundName = Walk1;
        //    }
        //    else
        //    {
        //        soundName = Walk2;
        //    }

        //    if (!FindObjectOfType<AudioManager>().IsPlaying(Walk1) && !FindObjectOfType<AudioManager>().IsPlaying(Walk2))
        //    {
        //        FindObjectOfType<AudioManager>().Play(soundName);
        //        _stepCount++;
        //        _stepCount %= 2;
        //    }
        //}
        #endregion
    }
    void PlayFootSound()
    {
        StartCoroutine("PlayStepSound", footstepTimer);
    }
    IEnumerator PlayStepSound(float timer)
    {

        if (isGrounded)
        {
            string soundName;
            string Foot1 = "Foot1";
            string Foot2 = "Foot2";
            string Foot3 = "Foot3";
            string Foot4 = "Foot4";

            if (stepCount == 1)
            {
                soundName = Foot1;
                isMoving = true;
                FindObjectOfType<AudioManager>().Play(soundName);
                yield return new WaitForSeconds(timer);
                isMoving = false;
                stepCount++;
            }
            else if (stepCount == 2)
            {
                soundName = Foot2;
                isMoving = true;
                FindObjectOfType<AudioManager>().Play(soundName);
                yield return new WaitForSeconds(timer);
                isMoving = false;
                stepCount++;
            }
            else if (stepCount == 3)
            {
                soundName = Foot3;
                isMoving = true;
                FindObjectOfType<AudioManager>().Play(soundName);
                yield return new WaitForSeconds(timer);
                isMoving = false;
                stepCount++;
            }
            else
            {
                soundName = Foot4;
                isMoving = true;
                FindObjectOfType<AudioManager>().Play(soundName);
                yield return new WaitForSeconds(timer);
                isMoving = false;
                stepCount = 1;
            }
        }

    }
    private void Crouch()
    {
        if (ShouldCrouch)
        {
            StartCoroutine(CrouchStand());
        }
    }
    #region unused
    //void PlayFootSound()
    //{
    //    StartCoroutine("PlayStepSound", footstepTimer);
    //}
    //IEnumerator PlayStepSound(float timer)
    //{
    //    if (isGrounded)
    //    {
    //        if (_stepCount % 2 == 0)
    //        {
    //            audioManager.AudioSource.clip = AudioManager[1];
    //            audioManager.AudioSource.Play();
    //            isMoving = true;

    //            yield return new WaitForSeconds(timer);

    //            isMoving = false;
    //            _stepCount++;
    //        }
    //        else
    //        {
    //            audioManager.AudioSource.clip = AudioManager.footStepSound[1];
    //            audioManager.AudioSource.Play();
    //            isMoving = true;

    //            yield return new WaitForSeconds(timer);

    //            isMoving = false;
    //            _stepCount++;
    //        }
    //    }

    //}
    #endregion

    IEnumerator CrouchStand()
    {
        if (isCrouching && Physics.Raycast(Camera.main.transform.position, Vector3.up, 2f))
        {
            yield break;
        }
        duringCrouchAnimation = true;

        float timeElapsed = 0;
        float targetHeight = isCrouching ? standingHeight : crouchHeight;
        float currentHeight = characterController.height;
        Vector3 targetCenter = isCrouching ? standingCenter : crouchingCenter;
        Vector3 currentCenter = characterController.center;

        while (timeElapsed < timeToCrouch)
        {
            characterController.height = Mathf.Lerp(currentHeight, targetHeight, timeElapsed / timeToCrouch);
            characterController.center = Vector3.Lerp(currentCenter, targetCenter, timeElapsed / timeToCrouch);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        characterController.height = targetHeight;
        characterController.center = targetCenter;

        isCrouching = !isCrouching;

        duringCrouchAnimation = false;
    }
}
