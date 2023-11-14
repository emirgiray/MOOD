using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigKickEnergy : MonoBehaviour
{
    public bool canKick = true;

    public Camera cam;

    public float maxKickDistance = 10f;
    public float kickForceBase = 0f;
    public float kickForce = 20f;
    public float kickForceUp = 10f;
    public float kickTorque = 5f;
    public float kickForceDoor = 40f;
    float kickTorqueRandom;

    Rigidbody kickedRB;

    RaycastHit hit;

    private void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {        
        if (Input.GetKeyDown(KeyCode.E) && canKick)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, maxKickDistance))
            {
                if (hit.collider.tag == "Heavy Object")
                {
                    if(hit.collider.name == "doorUnbroken")
                    {
                        kickForceBase = kickForceDoor;
                    }
                    else
                    {
                        kickForceBase = kickForce;
                    }
                    kickedRB = hit.collider.gameObject.GetComponent<Rigidbody>();
                    Debug.Log("is Heavy");
                    kickedRB.AddForce(cam.transform.forward * kickForceBase, ForceMode.VelocityChange);
                    kickedRB.AddForce(cam.transform.up * kickForceUp, ForceMode.VelocityChange);

                    kickTorqueRandom = Random.Range(-kickTorque, kickTorque);
                    kickedRB.AddTorque(cam.transform.up * kickTorqueRandom, ForceMode.VelocityChange);
                    kickedRB.AddTorque(cam.transform.forward * kickTorqueRandom, ForceMode.VelocityChange);
                }
            }
        }
    }
}
