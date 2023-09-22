using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ContarPaga : MonoBehaviour
{
	public int Puntos;

	public int PuntosTiempo;

	public int NumeroNivel;

	public AudioClip Sonido_R1;

	public AudioClip Sonido_RMulti;

	public AudioClip MusicaAlGanar;

	private GameObject ObjetoSlider;

	public void Start()
	{
		((AudioSource)GameObject.Find("MusicaDeFondo").GetComponent(typeof(AudioSource))).Stop();
		this.NumeroNivel = ((ControlMovimiento)GameObject.FindGameObjectWithTag("Player").GetComponent(typeof(ControlMovimiento))).NumeroNivel;
		this.Puntos = ((ControlMovimiento)GameObject.FindGameObjectWithTag("Player").GetComponent(typeof(ControlMovimiento))).Puntos;
		((Text)GameObject.Find("Txt_Salario").GetComponent(typeof(Text))).text = this.Puntos.ToString();
		this.ObjetoSlider = GameObject.Find("SliderTiempo");
		this.PuntosTiempo = (int)(((Slider)this.ObjetoSlider.GetComponent(typeof(Slider))).value * (float)100);
		this.InvokeRepeating("SuenaCampana", (float)0, 0.25f);
		this.TerminarPaga();
	}

	public void SuenaCampana()
	{
		if (((Slider)this.ObjetoSlider.GetComponent(typeof(Slider))).value > (float)0)
		{
			((AudioSource)GameObject.Find("Player").GetComponent(typeof(AudioSource))).PlayOneShot(this.Sonido_R1);
		}
	}

	public void Update()
	{
		int num = 0;
		if (((Slider)this.ObjetoSlider.GetComponent(typeof(Slider))).value > (float)0)
		{
			((Slider)this.ObjetoSlider.GetComponent(typeof(Slider))).value = ((Slider)this.ObjetoSlider.GetComponent(typeof(Slider))).value - Time.deltaTime / (float)(this.PuntosTiempo / 15);
			num = (int)((float)(this.Puntos + this.PuntosTiempo) - ((Slider)this.ObjetoSlider.GetComponent(typeof(Slider))).value * (float)100);
			((Text)GameObject.Find("Txt_Salario").GetComponent(typeof(Text))).text = num.ToString();
			this.TerminarPaga();
		}
	}

	public void TerminarPaga()
	{
		if (((Slider)this.ObjetoSlider.GetComponent(typeof(Slider))).value <= (float)0)
		{
			((Animation)GameObject.Find("Canvas").GetComponent(typeof(Animation))).Play("EsconderMarcadores");
			GameObject.Find("MultiStartPaga").SendMessage("Emitir");
			((AudioSource)GameObject.Find("Player").GetComponent(typeof(AudioSource))).PlayOneShot(this.Sonido_RMulti);
			this.Invoke("SacaTapon", 1f);
		}
	}

	public void SacaTapon()
	{
		if (PlayerPrefs.GetInt("MaxScoreNivel" + this.NumeroNivel) < this.Puntos + this.PuntosTiempo)
		{
			((Animation)GameObject.Find("Canvas").GetComponent(typeof(Animation))).Play("SacarTapon");
			PlayerPrefs.SetInt("MaxScoreNivel" + this.NumeroNivel, this.Puntos + this.PuntosTiempo);
		}
		((AudioSource)GameObject.Find("MusicaDeFondo").GetComponent(typeof(AudioSource))).PlayOneShot(this.MusicaAlGanar);
	}

	public void Main()
	{
	}
}
