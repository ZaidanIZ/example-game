using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class SetIdiomaTXT : MonoBehaviour
{
	public string[] TXT_Alternativo;

	public void Start()
	{
		if (PlayerPrefs.GetInt("Idioma") == 1)
		{
			((Text)this.GetComponent(typeof(Text))).text = this.TXT_Alternativo[0];
		}
	}

	public void Main()
	{
	}
}
