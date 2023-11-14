using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalking : MonoBehaviour
{
    private FMOD.Studio.EventInstance instance;

    public FMODUnity.EventReference fmodEvent;

    public FMODUnity.StudioEventEmitter eventEmitter;

    Rigidbody _rb;

    //[SerializeField]
    //[Range(0, 1)]
    //private float _range;
    //public float _speed;
    //bool trigger;
    // Start is called before the first frame update
    void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        instance.start();

        eventEmitter = GetComponent<FMODUnity.StudioEventEmitter>();
    }

    // Update is called once per frame
    void Update()
    {
        //_speed = _rb.inertiaTensor.magnitude;

        //instance.setParameterByName("Hit", _range);

        //if (_speed > 0.01f)
        //{
        //    _range = 1;
        //}
        //else
        //    _range = 0;
        //if (trigger)
        //{
        //    instance.setParameterByName("Hit", 1);
        //    //eventEmitter.PlayInstance();
        //}
        //else
        //{
        //    eventEmitter.Stop();
        //    instance.setParameterByName("Hit", 0);
        //}
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 4)
        {
            //_range = 1;
            //instance.setParameterByName("Hit", _range);
            //trigger = true;
            eventEmitter.Play();

        }
        else 
        {
            eventEmitter.Stop();
        }                
    }
    //private void OnCollisionExit(Collision collision)
    //{
    //    _range = 0;
    //    instance.setParameterByName("Hit", _range);
    //    eventEmitter.Stop();
    //}
}
