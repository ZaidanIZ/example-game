using UnityEngine;
using System.Collections;

public class Spike : MonoBehaviour {

	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Bird") {

			col.gameObject.GetComponent<Bird> ().Die ();
			
			
			//col.gameObject.transform.parent = null;
		}
	}
}
