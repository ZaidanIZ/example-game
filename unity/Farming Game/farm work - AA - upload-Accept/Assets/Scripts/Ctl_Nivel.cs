using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Ctl_Nivel : MonoBehaviour
{
	public int EsteNivel;

	public Sprite NivelLogo;

	public bool EsActivo;

	public void Start()
	{
		int num = 0;
		int num2 = 0;
		num = PlayerPrefs.GetInt("NivelDesbloqueado");
		((Text)this.transform.Find("Numero").GetComponent(typeof(Text))).text = string.Empty + this.EsteNivel;
		((Image)this.transform.Find("NivelLogo").GetComponent(typeof(Image))).sprite = this.NivelLogo;
		if (this.EsteNivel <= num)
		{
			this.transform.Find("BotonInactivo").gameObject.SetActive(false);
			this.EsActivo = true;
			num2 = PlayerPrefs.GetInt("MaxScoreNivel" + this.EsteNivel);
			((Text)this.transform.Find("Puntuacion").GetComponent(typeof(Text))).text = num2.ToString("D4");
		}
		else
		{
			this.transform.Find("BotonActivo").gameObject.SetActive(false);
			this.EsActivo = false;
		}
	}

	public void PlayEsteNivel()
	{
		if (this.EsActivo)
		{
			GameObject.Find("Control de Menu").SendMessage("PlayNivel", this.EsteNivel);
		}
		else
		{
			GameObject.Find("Control de Menu").SendMessage("PlayNivelError");
		}
	}

	public void Main()
	{
	}
}
