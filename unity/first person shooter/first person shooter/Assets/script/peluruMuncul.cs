using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class peluruMuncul : MonoBehaviour {
	public Rigidbody peluru;
	[Range(0,100)]
	public float speed = 100;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (CrossPlatformInputManager.GetButtonDown("Fire1"))
		{
			Rigidbody peluruBaru = (Rigidbody)GameObject.Instantiate(peluru, transform.position, transform.rotation);
			peluruBaru.velocity = transform.TransformDirection(Vector3.forward * speed);
			Destroy(peluruBaru.gameObject,3);
		}
	}
}