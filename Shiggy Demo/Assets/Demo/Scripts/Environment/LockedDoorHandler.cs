using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoorHandler : MonoBehaviour
{
    //public string keyColor;
    public bool blueDoor;
    public bool redDoor;
    public bool goldenDoor;

    public Transform bathroomDoorLeft;
    public Transform bathroomDoorRight;

    public bool openDoor = false;
    public bool closeDoor = false;

    private Vector3 doorLeft_TargetPos;
    private Vector3 doorRight_TargetPos;

    private Vector3 doorLeft_OriginalPos;
    private Vector3 doorRight_OriginalPos;

    public float moveBy = 5f;
    public float doorSpeed = 5f;
    void Start()
    {
        doorLeft_OriginalPos = bathroomDoorLeft.position;
        doorRight_OriginalPos = bathroomDoorRight.position;
        doorLeft_TargetPos = doorLeft_OriginalPos + new Vector3(moveBy, 0, 0);
        doorRight_TargetPos = doorRight_OriginalPos + new Vector3(-moveBy, 0, 0);
    }
    void Update()
    {
        if (openDoor && !closeDoor)
        {
            bathroomDoorLeft.transform.position = Vector3.Lerp(bathroomDoorLeft.position, doorLeft_TargetPos, doorSpeed * Time.deltaTime);
            bathroomDoorRight.transform.position = Vector3.Lerp(bathroomDoorRight.position, doorRight_TargetPos, doorSpeed * Time.deltaTime);
        }

    }
    public void OpenDoor()
    {
        openDoor = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Blue Key")
        {
            if (blueDoor == true)
            {
                OpenDoor();
            }
            
        }
        if (other.gameObject.name == "Golden Key")
        {
            if (goldenDoor)
            {
                OpenDoor();
            }
            
        }
       
        if (other.gameObject.name == "Red Key")
        {
            if (redDoor)
            {
                OpenDoor();
            }
        }
    }
}
