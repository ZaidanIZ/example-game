using System;
using UnityEngine;

[Serializable]
public class MusicaDeFondo : MonoBehaviour
{
	public AudioClip TemaPrincipalSound;

	public float DelayTime;

	public void Start()
	{
		this.Invoke("PlayMusica", this.DelayTime);
	}

	public void PlayMusica()
	{
		this.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("VolumenMusica");
		this.GetComponent<AudioSource>().PlayOneShot(this.TemaPrincipalSound);
	}

	public void Main()
	{
	}
}
