using UnityEngine;
using System.Collections;

public class SameTextOfParent : MonoBehaviour 
{

	// Use this for initialization
	void Update () 
	{
		GetComponent<TextMesh>().text = transform.parent.GetComponent<TextMesh>().text;
	}

}
