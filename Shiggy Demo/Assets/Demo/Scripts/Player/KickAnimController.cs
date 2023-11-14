using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KickAnimController : MonoBehaviour
{
    Animator _anim;
    public Image img;
    public PlayerController playerController;
    public float AnimDuration = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        img.enabled = false;
        //playerController = GetComponent<PlayerController>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)&&!GravityGun.isGrabbed && !playerController.isCrouching)
        {
            CancelInvoke();
            img.enabled = true;
            _anim.SetTrigger("KickTrigger");
            Invoke("DisableImage", AnimDuration);
        }
    }
    void DisableImage()
    {
        img.enabled = false;
    }
}
