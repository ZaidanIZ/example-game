using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour {
	/// <summary>
	///speed of cloud
	/// </summary>
	public float speed;
	float offset;
	// Use this for initialization
	void Start () {
	
	}
	/// <summary>
	///move left the cloud
	/// </summary>
	// Update is called once per frame
	void Update () {
	
		offset = speed * Time.deltaTime;
		transform.position = new Vector3 (transform.position.x - offset,
			transform.position.y, transform.position.z);
		if(transform.position.x <= -5.5f)
			transform.position = new Vector3 (5.5f,
				transform.position.y, transform.position.z);
	}
}
