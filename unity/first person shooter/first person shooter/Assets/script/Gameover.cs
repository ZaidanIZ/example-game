﻿using UnityEngine;
using UnityEngine.SceneManagement;
public class Gameover : MonoBehaviour {
	float timer = 0;
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if(timer > 3f)
		{
			Data.score = 0;
			SceneManager.LoadScene("Gameplay");
		}
	}
}