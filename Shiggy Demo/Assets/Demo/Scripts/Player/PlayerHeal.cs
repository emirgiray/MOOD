using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeal : MonoBehaviour
{
    public float healAmount;
    public HealthSystem healthSystem;

    void Start()
    {
        healthSystem = GameObject.Find("Player").GetComponent<HealthSystem>();
    }


    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && healthSystem.health < healthSystem.healthMax)
        {
            Debug.Log("brr");
            healthSystem.Heal(healAmount);
            Destroy(gameObject);

        }
    }


}
