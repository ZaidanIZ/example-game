using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ContadorDeTiempo : MonoBehaviour
{
	public int TotalParcela;

	public Sprite Imagen1;

	public Sprite Imagen2;

	public AudioClip Sonido_FinTiempo;

	private ControlMovimiento controlmovimiento;

	private GameObject ObjetoFill;

	public void Start()
	{
		((Slider)this.GetComponent(typeof(Slider))).value = (float)1;
		this.controlmovimiento = (ControlMovimiento)GameObject.Find("Player").GetComponent(typeof(ControlMovimiento));
		this.ObjetoFill = GameObject.Find("Fill");
	}

	public void Update()
	{
		float num = 0f;
		float num2 = 0f;
		if (this.controlmovimiento.EstadoJuego == 0)
		{
			num = ((Slider)this.GetComponent(typeof(Slider))).value;
			((Slider)this.GetComponent(typeof(Slider))).value = ((Slider)this.GetComponent(typeof(Slider))).value - Time.deltaTime / (float)(this.TotalParcela * 3);
			num2 = ((Slider)this.GetComponent(typeof(Slider))).value;
			if (num >= 0.33f && num2 < 0.33f)
			{
				((Image)this.ObjetoFill.GetComponent(typeof(Image))).sprite = this.Imagen2;
			}
			else if (num >= 0.66f && num2 < 0.66f)
			{
				((Image)this.ObjetoFill.GetComponent(typeof(Image))).sprite = this.Imagen1;
			}
			if (num > (float)0 && num2 <= (float)0)
			{
				GameObject.Find("TiempoBg").GetComponent<Animation>().Play("RelojSuena");
				GameObject.Find("Player").GetComponent<AudioSource>().PlayOneShot(this.Sonido_FinTiempo);
			}
		}
	}

	public void Main()
	{
	}
}
