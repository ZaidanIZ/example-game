using System;
using UnityEngine;

[Serializable]
public class FollowCamera : MonoBehaviour
{
	public void Start()
	{
		((MeshRenderer)this.GetComponent(typeof(MeshRenderer))).enabled = false;
		this.transform.position = GameObject.FindWithTag("Player").transform.position;
	}

	public void Update()
	{
		this.transform.position = GameObject.FindWithTag("Player").transform.position;
	}

	public void Main()
	{
	}
}
