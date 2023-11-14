using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    public Rigidbody rigidbody;
	public float depthBeforeSubmerged = 20f;
	public float displacementAmount = 150f;
	public int floaterAmount =50;
	public float waterDrag = 0.1f;
	public float waterAngularDrag = 50;

	public bool trigger;
	public void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
		if (other.CompareTag("Liquid"))
		{
			trigger = true;
		}
	}
    private void OnTriggerExit(Collider other)
    {
		if (other.CompareTag("Liquid"))
		{
			trigger = false;
		}
	}

    private void FixedUpdate()
	{
		rigidbody.AddForceAtPosition(Physics.gravity / floaterAmount, transform.position, ForceMode.Acceleration);
		if (trigger)
		{
			float displacementMultiplier = Mathf.Clamp01(-transform.position.y / depthBeforeSubmerged) * displacementAmount;
			rigidbody.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f), transform.position, ForceMode.Acceleration);
			rigidbody.AddForce(displacementMultiplier * -rigidbody.velocity * waterDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
			rigidbody.AddTorque(displacementMultiplier * -rigidbody.angularVelocity * waterAngularDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
		}
	}
}
