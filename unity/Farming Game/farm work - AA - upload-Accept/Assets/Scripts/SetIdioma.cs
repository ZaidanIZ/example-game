using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class SetIdioma : MonoBehaviour
{
	public Sprite[] Sprite_Alternativo;

	public void Start()
	{
		if (PlayerPrefs.GetInt("Idioma") == 1)
		{
			((Image)this.GetComponent(typeof(Image))).sprite = this.Sprite_Alternativo[0];
		}
	}

	public void Main()
	{
	}
}
