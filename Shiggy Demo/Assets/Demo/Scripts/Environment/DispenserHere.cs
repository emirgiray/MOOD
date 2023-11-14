using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispenserHere : MonoBehaviour
{
    public GameObject ticket;
    public Transform spawnPoint;
    public float throwSpeed = 10f;

    // Update is called once per frame
    void Update()
    {
        if (GameController.bowlingScore>=6)
        {
            Dispense();
            GameController.bowlingScore = 0;
        }
    }
    public void Dispense()
    {
        Debug.Log("ticket dispensed");
        Rigidbody _rb = Instantiate(ticket, spawnPoint.position, transform.rotation).GetComponent<Rigidbody>();
        _rb.AddForce(transform.right * throwSpeed, ForceMode.Impulse);        
    }
}
