using System;
using UnityEngine;

public class Floating : MonoBehaviour
{
	[SerializeField]
	private float Length = 0.05f;

	[SerializeField]
	private float speed = 1f;

	[SerializeField]
	private Vector3 direction = Vector3.up;

	private float currentAngle;

	private Vector3 startPos;

	private void Awake()
	{
		this.startPos = base.transform.position;
		this.direction = this.direction.normalized;
	}

	private void Update()
	{
		this.currentAngle += Time.deltaTime * this.speed;
		if (this.currentAngle >= 6.28318548f)
		{
			this.currentAngle -= 6.28318548f;
		}
		Vector3 position = this.startPos + Mathf.Sin(this.currentAngle) * this.Length * this.direction;
		base.transform.position = position;
	}
}
