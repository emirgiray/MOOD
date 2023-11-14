using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    public Transform glass1;
    Vector3 targetPos;
    public bool trigger;
    public float speed = 5;
    public Light _L;
    void Start()
    {
        targetPos = glass1.transform.position + Vector3.up * 9;
    }

    void Update()
    {
        if (trigger)
        {
            //Vector3 move = new Vector3(0, 11.25f, 0);
            //glass1.position = Vector3.MoveTowards(gameObject.transform.position, move, Time.deltaTime * speed);

            glass1.transform.position = Vector3.Lerp(glass1.transform.position, targetPos, speed * Time.deltaTime);
            _L.intensity = Mathf.PingPong(Time.time, 2);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Light Object" || collision.gameObject.tag == "Light Object" )
        {
            Debug.Log("hit");
            trigger = true;
        }
    }
}
