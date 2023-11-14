using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    
    // It simply brings it up, idk how many floors we will do

    public GameObject moveThePlatform;


    private void OnTriggerStay()
    {
        moveThePlatform.transform.position += moveThePlatform.transform.up* Time.deltaTime;
    }

    
}
