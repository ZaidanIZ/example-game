using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[Serializable]
public class RollABall : MonoBehaviour
{
	public Vector3 tilt;

	public float speed;

	private float circ;

	private Vector3 previousPosition;

	public RollABall()
	{
		this.tilt = Vector3.zero;
	}

	public void Start()
	{
		this.circ = 6.28318548f * this.GetComponent<Collider>().bounds.extents.x;
		this.previousPosition = this.transform.position;
	}

	public void Update()
	{
		this.tilt.x = -Input.acceleration.y;
		this.tilt.z = Input.acceleration.x;
		this.GetComponent<Rigidbody>().AddForce(this.tilt * this.speed * Time.deltaTime);
	}

	public void LateUpdate()
	{
		Vector3 a = this.transform.position - this.previousPosition;
		a = new Vector3(a.z, (float)0, -a.x);
		this.transform.Rotate(a / this.circ * (float)360, Space.World);
		this.previousPosition = this.transform.position;
	}

	public void Main()
	{
	}
}
