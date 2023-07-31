using UnityEngine;
using System.Collections;

public class SameTextThatParent : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<TextMesh>().text = transform.parent.GetComponent<TextMesh>().text;
	}
	

}
