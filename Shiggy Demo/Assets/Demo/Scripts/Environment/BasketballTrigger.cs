using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketballTrigger : MonoBehaviour
{
    public DispenserHere DispenserScript;
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Light Object")
        {
            DispenserScript.Dispense();
        }
    }
}
