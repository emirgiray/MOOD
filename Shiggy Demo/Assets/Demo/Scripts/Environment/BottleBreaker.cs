using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleBreaker : MonoBehaviour
{
    public GameObject brokenBottle;
    public GameObject wallBreak;

    public float decalDiff = 0.2f;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Wall" || collision.gameObject.name == "Crowbar")
        {
            if (collision.relativeVelocity.magnitude >= 15 || gameObject.tag == "Heavy Object") // Check if the object is hitting something with adequate force
            {
                Instantiate(brokenBottle, transform.position, transform.rotation);

                ContactPoint contact = collision.contacts[0];
                Vector3 hitPos = contact.point;
                Quaternion hitRot = Quaternion.LookRotation(contact.normal);


                if(collision.gameObject.name != "Crowbar")
                {
                    GameObject bulletHole = Instantiate(wallBreak, hitPos, hitRot);
                    bulletHole.transform.position += bulletHole.transform.forward * decalDiff;
                }

                Destroy(gameObject);
            }                       
        }
    }
}
