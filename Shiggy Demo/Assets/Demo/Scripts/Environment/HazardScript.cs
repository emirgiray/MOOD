using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardScript : MonoBehaviour
{
    private bool trigger = false;
    public float damageAmount = 10;
    void Start()
    {
        
    }

    void Update()
    {
        //FindObjectOfType<HealthSystem>().Damage(10);
        if (trigger)
        {
            FindObjectOfType<HealthSystem>().Damage(damageAmount * Time.deltaTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            trigger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            trigger = false;
        }
    }
}
