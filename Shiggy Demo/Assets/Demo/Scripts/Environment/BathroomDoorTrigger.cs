using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathroomDoorTrigger : MonoBehaviour
{
    public bool isDoorOnXAxis;
    
    public Transform bathroomDoorLeft;
    public Transform bathroomDoorRight;

    public bool openDoor = false;
    public bool closeDoor = false;

    private Vector3 doorLeft_TargetPos;
    private Vector3 doorRight_TargetPos;

    private Vector3 doorLeft_OriginalPos;
    private Vector3 doorRight_OriginalPos;

    public float moveBy = 3f;
    public float doorSpeed = 5f;

    public float switcher = 0;

    private void Start()
    {
        doorLeft_OriginalPos = bathroomDoorLeft.position;
        doorRight_OriginalPos = bathroomDoorRight.position;
        if (isDoorOnXAxis == false)
        {
            doorLeft_TargetPos = doorLeft_OriginalPos + new Vector3(0, 0, moveBy);
            doorRight_TargetPos = doorRight_OriginalPos + new Vector3(0, 0, -moveBy);
        }
        else
        {
            doorLeft_TargetPos = doorLeft_OriginalPos + new Vector3(moveBy, 0, 0);
            doorRight_TargetPos = doorRight_OriginalPos + new Vector3(-moveBy, 0, 0);
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy" || other.gameObject.tag == "Light Object")
        {
             OpenDoor();          
        }        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy" || other.gameObject.tag == "Light Object")
        {
            CloseDoor();
        }
    }
    private void Update()
    {
        if (openDoor&&!closeDoor)
        {
            bathroomDoorLeft.transform.position = Vector3.Lerp(bathroomDoorLeft.position, doorLeft_TargetPos, doorSpeed * Time.deltaTime);
            bathroomDoorRight.transform.position = Vector3.Lerp(bathroomDoorRight.position, doorRight_TargetPos, doorSpeed * Time.deltaTime);
        }
        if (closeDoor&&!openDoor)
        {
            bathroomDoorLeft.transform.position = Vector3.Lerp(bathroomDoorLeft.position, doorLeft_OriginalPos, doorSpeed * Time.deltaTime);
            bathroomDoorRight.transform.position = Vector3.Lerp(bathroomDoorRight.position, doorRight_OriginalPos, doorSpeed * Time.deltaTime);
        }
    }
    public void OpenDoor()
    {
        openDoor = true;
        closeDoor = false;
    }
    public void CloseDoor()
    {
        openDoor = false;
        closeDoor = true;
    }
}
