using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Bird") {
			col.gameObject.GetComponent<Bird>().coinGetAlready = true;
			col.gameObject.GetComponent<Bird> ().GetCoin (transform.position);
			Destroy (gameObject);
		}
	}
}
