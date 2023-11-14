using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinDown : MonoBehaviour
{
    private bool isPinDown = false;
    private bool looping = true;

    private void Update()
    {
        if (looping)
        {
            if (transform.up.y < 0.5f)
            {
                isPinDown = true;
            }
            if (isPinDown)
            {
                GameController.bowlingScore++;
                looping = false;
            }
        }        
    }
}
