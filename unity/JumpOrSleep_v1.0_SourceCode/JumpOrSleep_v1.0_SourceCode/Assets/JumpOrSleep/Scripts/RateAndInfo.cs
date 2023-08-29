using UnityEngine;
using System.Collections;

public class RateAndInfo : MonoBehaviour {
	/// <summary>
	///rate or show your infor
	/// </summary>
	public string link;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void OpenLink()
	{
		Application.OpenURL (link);
	}
}
