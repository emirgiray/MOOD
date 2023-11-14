using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRandomForce : MonoBehaviour
{
    Rigidbody _r;
    public float force = 0f;

    // Start is called before the first frame update
    void Start()
    {
        _r = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _r.AddForce(Random.insideUnitCircle.normalized*force);
        }
    }
}
