using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Reference Values
    public float speed = 10;

    //Private References
    Rigidbody _rb;

    //Public References
    //HealthSystem _HS = new HealthSystem();

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 nextPos = _rb.transform.position + transform.forward * Time.fixedDeltaTime * speed;
        _rb.MovePosition(nextPos);
        _rb.freezeRotation = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Enemy" || collision.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Enemy")
        {
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());        
        }


    }

}
