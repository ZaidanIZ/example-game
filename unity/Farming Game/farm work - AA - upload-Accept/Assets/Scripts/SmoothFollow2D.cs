using System;
using UnityEngine;

[Serializable]
public class SmoothFollow2D : MonoBehaviour
{
	public Transform target;

	public float smoothTime;

	private Transform thisTransform;

	private Vector2 velocity;

	public SmoothFollow2D()
	{
		this.smoothTime = 0.3f;
	}

	public void Start()
	{
		this.thisTransform = this.transform;
	}

	public void Update()
	{
		float x = Mathf.SmoothDamp(this.thisTransform.position.x, this.target.position.x, ref this.velocity.x, this.smoothTime);
		Vector3 position = this.thisTransform.position;
		float num = position.x = x;
		Vector3 vector = this.thisTransform.position = position;
		float y = Mathf.SmoothDamp(this.thisTransform.position.y, this.target.position.y, ref this.velocity.y, this.smoothTime);
		Vector3 position2 = this.thisTransform.position;
		float num2 = position2.y = y;
		Vector3 vector2 = this.thisTransform.position = position2;
	}

	public void Main()
	{
	}
}
