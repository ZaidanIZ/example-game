using System;
using UnityEngine;

[Serializable]
public class DisparaAnimacion : MonoBehaviour
{
	public string MiAnimationString;

	public void Start()
	{
		this.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("VolumenSonido");
	}

	public void OnTriggerEnter(Collider Col)
	{
		if (Col.name == "CubeContenedor")
		{
			this.GetComponent<Animation>().Play(this.MiAnimationString);
		}
	}

	public void Main()
	{
	}
}
