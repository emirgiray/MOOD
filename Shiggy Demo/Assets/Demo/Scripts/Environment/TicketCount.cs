using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketCount : MonoBehaviour
{
    public Transform topPart;

    private Vector3 topPartTarget;

    public float moveSpeed = 5f;

    private bool canGetKey = false;

    public float neededTickets = 5;

    private void Start()
    {        
        topPartTarget = topPart.position + new Vector3(0, 2f, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            other.gameObject.layer = 0;
            GameController.ticketCount++;
            if (GameController.ticketCount >= neededTickets)
            {
                canGetKey = true;
            }
        }
    }
    private void Update()
    {
        if (canGetKey)
        {
            topPart.transform.position = Vector3.Lerp(topPart.position, topPartTarget, moveSpeed * Time.deltaTime);
        }
    }
}
