using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

///Developed by Indie Studio
///https://www.assetstore.unity3d.com/en/#!/publisher/9268
///www.indiestd.com
///info@indiestd.com
///copyright © 2016 IGS. All rights reserved.
public class Logo : MonoBehaviour {

	public float sleepTime = 5;
	// Use this for initialization
	void Start () {
		Invoke ("LoadMainScene", sleepTime);
	}

	private void LoadMainScene(){
		SceneManager.LoadScene ("Main");
	}
	
}
