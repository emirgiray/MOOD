using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomboxTrigger : MonoBehaviour
{
    public PlayRandomSong songPick;
    public float maxSpeed = 10f;

    private void Start()
    {        
        //songPick = (PlayRandomSong) songPick.GetComponent(typeof(PlayRandomSong));
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude >= maxSpeed)
        {
            songPick.SwitchSong();
        }        
    }
}
