using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePianoKey : MonoBehaviour
{
    public bool isKeyPressed = false;

    private Vector3 targetPos;
    private Vector3 originalPos;

    public bool keyDown = false;
    public bool playSound = false;
    public bool canPlaySound = true;

    public float moveBy = 0f;

    AudioSource keySound;
    // Start is called before the first frame update
    void Start()
    {
        keySound = gameObject.GetComponent<AudioSource>();

        originalPos = transform.position;
        targetPos = new Vector3(originalPos.x, originalPos.y - moveBy, originalPos.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (isKeyPressed)
        {            
            if (playSound && canPlaySound)
            {
                PlaySound();
                playSound = false;
                canPlaySound = false;
            }
            
            if (!keyDown)
            {
                MoveKeyDown();
                if (transform.position == targetPos)
                    keyDown = true;
            }
            if (keyDown)
            {
                MoveKeyUp();
                if (transform.position == originalPos)
                {
                    keyDown = false; 
                    isKeyPressed = false;
                    canPlaySound = true;
                }                  
            }            
        }
        //if (transform.position.y - targetPos.y <= 0.001f) --- somehow this code makes it look much smoother but it doesnt really work
        //{
        //    MoveKeyUp();
        //    if(transform.position.y - originalPos.y <= 0.001f)
        //    {
        //        isKeyPressed = false;
        //    }
        //}

    }
    void MoveKeyDown()
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, 0.25f);
    }
    void MoveKeyUp()
    {
        transform.position = Vector3.Lerp(transform.position, originalPos, 0.25f);
    }
    void PlaySound()
    {        
        keySound.Play();
    }
}
