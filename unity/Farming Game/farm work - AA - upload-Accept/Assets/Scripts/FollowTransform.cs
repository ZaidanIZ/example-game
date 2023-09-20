using System;
using UnityEngine;

[Serializable]
public class FollowTransform : MonoBehaviour
{
	public Transform targetTransform;

	public bool faceForward;

	private Transform thisTransform;

	public void Start()
	{
		this.thisTransform = this.transform;
	}

	public void Update()
	{
		this.thisTransform.position = this.targetTransform.position;
		if (this.faceForward)
		{
			this.thisTransform.forward = this.targetTransform.forward;
		}
	}

	public void Main()
	{
	}
}
