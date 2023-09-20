using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fading : MonoBehaviour
{
	public Texture2D fadeOutTexture;

	public float fadeSpeed = 0.8f;

	private int drawDepth = -1000;

	private float alpha = 1f;

	private int fadeDir = -1;

	private void OnGUI()
	{
		this.alpha += (float)this.fadeDir * this.fadeSpeed * Time.deltaTime;
		this.alpha = Mathf.Clamp01(this.alpha);
		GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, this.alpha);
		GUI.depth = this.drawDepth;
		GUI.DrawTexture(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), this.fadeOutTexture);
	}

	public float BeginFade(int direction)
	{
		this.fadeDir = direction;
		return this.fadeSpeed;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		this.BeginFade(-1);
	}
}
