using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisioChecker : MonoBehaviour
{
    public static bool openGoldenDoor = false;
    public static bool closeGoldenDoor = false;

    public static bool openBlueDoor = false;
    public static bool closeBlueDoor = false;

    public static bool openRedDoor = false;
    public static bool closeRedDoor = false;

    public Transform doorLeft_golden;
    public Transform doorRight_golden;

    public Transform doorTop_blue;
    public Transform doorBottom_blue;

    //public Transform doorLeft_red;
    //public Transform doorRight_red;

    public float doorSpeed = 2f;

    private Vector3 doorLeft_goldenTargetPos;
    private Vector3 doorRight_goldenTargetPos;
    private Vector3 doorLeft_goldenOriginalPos;
    private Vector3 doorRight_goldenOriginalPos;

    private Vector3 doorTop_blueTargetPos;
    private Vector3 doorBottom_blueTargetPos;
    private Vector3 doorTop_blueOriginalPos;
    private Vector3 doorBottom_blueOriginalPos;

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.name);
        if(collision.gameObject.name == "Golden Key")
        {            
            GameController.switcher1++;
            if (GameController.switcher1 % 2 != 0)
            {
                openGoldenDoor = true;
                closeGoldenDoor = false;
            }
            else
            {
                closeGoldenDoor = true;
                openGoldenDoor = false;
            }
        }
            
        if (collision.gameObject.name == "Blue Key")
        {
            GameController.switcher2++;
            if (GameController.switcher2 % 2 != 0)
            {
                openBlueDoor = true;
                closeBlueDoor = false;
            }
            else
            {
                closeBlueDoor = true;
                openBlueDoor = false;
            }
        }
        if (collision.gameObject.name == "Red Key")
        {
            openRedDoor = true;
        }
    }
    private void Start()
    {
        //----GOLD
        doorLeft_goldenOriginalPos = doorLeft_golden.position;
        doorRight_goldenOriginalPos = doorRight_golden.position;

        doorLeft_goldenTargetPos = doorLeft_golden.position + new Vector3(0, 0, -5);
        doorRight_goldenTargetPos = doorRight_golden.position + new Vector3(0, 0, 5);

        //----BLUE
        doorTop_blueOriginalPos = doorTop_blue.position;
        doorBottom_blueOriginalPos = doorBottom_blue.position;

        doorTop_blueTargetPos = doorTop_blue.position + new Vector3(0, 5, 0);
        doorBottom_blueTargetPos = doorBottom_blue.position + new Vector3(0, -5, 0);
    }
    private void Update()
    {
        if (openGoldenDoor)
        {
            doorLeft_golden.transform.position = Vector3.Lerp(doorLeft_golden.position, doorLeft_goldenTargetPos, doorSpeed * Time.deltaTime);
            doorRight_golden.transform.position = Vector3.Lerp(doorRight_golden.position, doorRight_goldenTargetPos, doorSpeed * Time.deltaTime);
        }
        if (closeGoldenDoor)
        {
            doorLeft_golden.transform.position = Vector3.Lerp(doorLeft_golden.position, doorLeft_goldenOriginalPos, doorSpeed * Time.deltaTime);
            doorRight_golden.transform.position = Vector3.Lerp(doorRight_golden.position, doorRight_goldenOriginalPos, doorSpeed * Time.deltaTime);
        }
        if (openBlueDoor)
        {
            doorTop_blue.transform.position = Vector3.Lerp(doorTop_blue.position, doorTop_blueTargetPos, doorSpeed * Time.deltaTime);
            doorBottom_blue.transform.position = Vector3.Lerp(doorBottom_blue.position, doorBottom_blueTargetPos, doorSpeed * Time.deltaTime);
        }
        if (closeBlueDoor)
        {
            doorTop_blue.transform.position = Vector3.Lerp(doorTop_blue.position, doorTop_blueOriginalPos, doorSpeed * Time.deltaTime);
            doorBottom_blue.transform.position = Vector3.Lerp(doorBottom_blue.position, doorBottom_blueOriginalPos, doorSpeed * Time.deltaTime);
        }
    }
}
