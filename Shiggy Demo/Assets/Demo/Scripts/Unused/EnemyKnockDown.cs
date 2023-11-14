using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockDown : MonoBehaviour
{
    public bool isTargetHit = false;
    public Animator animator;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude >= 15)
        {
            if (collision.gameObject.tag == "Light Object" || collision.gameObject.tag == "Heavy Object")
            {
                isTargetHit = true;
            }
        }
        

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isTargetHit)
        {
            animator.SetBool("IsDead", true);
        }
    }
}
