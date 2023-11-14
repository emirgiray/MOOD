using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCamera : MonoBehaviour
{
    float xRotation = 0;
    public float lookSpeed = 1;
    public float lookSpeedX = 100;

    public Transform Player;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        if (PauseMenu.isGamePaused == false)
        {
            xRotation -= mouseY * lookSpeed;
            xRotation = Mathf.Clamp(xRotation, -100, 60);

            transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

            float yRotation = mouseX * lookSpeedX;
            Player.Rotate(Vector3.up * yRotation);
        }
        else
        {
            xRotation -= mouseY * 0;
            xRotation = Mathf.Clamp(xRotation, -100, 60);

            transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

            float yRotation = mouseX * 0;
            Player.Rotate(Vector3.up * yRotation);
        }
       

        // min = -100, max = 60
        
    }
}
