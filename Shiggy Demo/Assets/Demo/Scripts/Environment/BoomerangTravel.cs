using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangTravel : MonoBehaviour
{   
    public Transform target;

    public float returnForce = 20f;

    public bool isTurning = false;
    public static bool isHit = false;

    Rigidbody _r;

    private void Start()
    {
        _r = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            BoomerangReturn();            
        }
        if (isTurning)
        {
            _r.AddTorque(transform.right*returnForce, ForceMode.Acceleration);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        isTurning = false;
        isHit = true;
    }
    public void BoomerangReturn()
    {
        _r.AddForce((target.position - transform.position) * returnForce, ForceMode.Acceleration);
        isTurning = true;
    }
}
