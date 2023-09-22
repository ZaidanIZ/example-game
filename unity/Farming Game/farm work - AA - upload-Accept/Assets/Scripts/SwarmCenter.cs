using System;
using UnityEngine;

public class SwarmCenter : MonoBehaviour
{
	public float speed;

	private void Start()
	{
		base.transform.Rotate(new Vector3(0f, UnityEngine.Random.Range(0f, 360f), 0f));
	}

	private void Update()
	{
		base.transform.Rotate(new Vector3(0f, this.speed * Time.deltaTime, 0f));
	}
}
