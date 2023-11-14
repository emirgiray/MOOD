using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTargetMovement : MonoBehaviour
{
    Quaternion targetAngle90 = Quaternion.Euler(90, 0, 0);
    Quaternion targetAngle0 = Quaternion.Euler(0, 0, 0);
    public Quaternion currentAngle;

    bool isTargetHit = false;
    public float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        currentAngle = targetAngle0;
        Vector3 targetRot = new Vector3(transform.rotation.x+90f, transform.rotation.y, transform.rotation.z);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isTargetHit)
        {
            timer++;
            currentAngle = targetAngle90;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(transform.localPosition.x+90f,transform.localPosition.y, transform.localPosition.z), timer*Time.deltaTime);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Light Object")
        {
            isTargetHit = true;
        }
        
    }
}
