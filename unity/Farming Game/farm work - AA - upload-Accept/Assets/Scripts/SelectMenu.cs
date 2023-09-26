//using Boo.Lang;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class SelectMenu : MonoBehaviour
{
	

	

	public int PaginaActual;

	public float VolumenSonido;

	public AudioClip OnClickSound;

	public AudioClip ErrorSound;

	public Sprite Sprite_PagAnt;

	public Sprite Sprite_PagAnt_D;

	public Sprite Sprite_PagSig;

	public Sprite Sprite_PagSig_D;

	public Sprite Sprite_PuntoGordo;

	public Sprite Sprite_PuntoChico;

	public Sprite Sprite_Musica_On;

	public Sprite Sprite_Musica_Off;

	public Sprite Sprite_Sonido_On;

	public Sprite Sprite_Sonido_Off;

	public bool HacambiadoIdioma;

	public void Start()
	{
				PlayerPrefs.SetInt("Idioma", 0);

		this.CheckValoresIniciales();
//		((Animation)GameObject.Find("Letras").GetComponent(typeof(Animation))).Play("MostrarLetras");
	//	((Animation)GameObject.Find("Letras").GetComponent(typeof(Animation))).PlayQueued("AnimacionLetras", QueueMode.CompleteOthers);
//		((Animation)GameObject.Find("Canvas").GetComponent(typeof(Animation))).Play("Menu-MostrarBotones");
		this.PaginaActual = PlayerPrefs.GetInt("NivelDesbloqueado") / 10 + 1;
		this.VolumenSonido = PlayerPrefs.GetFloat("VolumenSonido");
		//((Slider)GameObject.Find("SliderSonido").GetComponent(typeof(Slider))).value = this.VolumenSonido;
		//((Slider)GameObject.Find("SliderMusica").GetComponent(typeof(Slider))).value = PlayerPrefs.GetFloat("VolumenMusica");
		//PlayerPrefs.SetInt("Idioma", 0);

	}

	public void CheckValoresIniciales()
	{
		if (PlayerPrefs.GetInt("Iniciado") == 0)
		{
			UnityEngine.Debug.Log("Iniciando Valores");
			PlayerPrefs.SetInt("Idioma", 0);
			PlayerPrefs.SetInt("NivelDesbloqueado", 1);
			PlayerPrefs.SetInt("NivelPagado", 1);
			PlayerPrefs.SetFloat("VolumenSonido", 0.8f);
			PlayerPrefs.SetFloat("VolumenMusica", 0.8f);
			PlayerPrefs.SetInt("Iniciado", 1);
			PlayerPrefs.SetInt("JuegoComprado", 1);
		}
	}

	public void MenuPlay()
	{
		StartCoroutine(PlaySiguientePagado());
	}

	public IEnumerator PlaySiguientePagado()
	{
              GetComponent<AudioSource>().PlayOneShot(OnClickSound, VolumenSonido);
					((Animation)GameObject.Find("Letras").GetComponent(typeof(Animation))).Play("OcultarLetras");
					((Animation)GameObject.Find("Canvas").GetComponent(typeof(Animation))).Play("Menu-OcultarBotones");
				//	GameObject.Find("Fades").SendMessage("BeginFade", 1);
					//arg_EE_0 = (this.Yield(2, new WaitForSeconds(1f)) ? 1 : 0);
yield return new WaitForSeconds(1);
					UnityEngine.SceneManagement.SceneManager.LoadScene("Nivel0" + PlayerPrefs.GetInt("NivelPagado"));

		//return new SelectMenu._PlaySiguientePagado_118(this).GetEnumerator();
	}

	public void SeleccionarNivel()
	{
		this.GetComponent<AudioSource>().PlayOneShot(this.OnClickSound, this.VolumenSonido);
		this.PaginaActual = PlayerPrefs.GetInt("NivelDesbloqueado") / 10 + 1;
		this.PaginaActual = 1;
		((Animation)GameObject.Find("Letras").GetComponent(typeof(Animation))).Play("OcultarLetras");
		((Animation)GameObject.Find("Canvas").GetComponent(typeof(Animation))).Play("Menu-OcultarBotones");
		((Animation)GameObject.Find("PanelNiveles").GetComponent(typeof(Animation))).Play("Menu-MostrarNiveles");
		((Animation)GameObject.Find("Niveles-Pag" + this.PaginaActual).GetComponent(typeof(Animation))).Play("Menu-MostrarSubNiveles");
		this.CheckFlechasPaginas();
	}

	public void PulsarSalir()
	{
		this.GetComponent<AudioSource>().PlayOneShot(this.OnClickSound, this.VolumenSonido);
		((Animation)GameObject.Find("Letras").GetComponent(typeof(Animation))).Play("MostrarLetras");
		((Animation)GameObject.Find("Letras").GetComponent(typeof(Animation))).PlayQueued("AnimacionLetras", QueueMode.CompleteOthers);
		((Animation)GameObject.Find("Canvas").GetComponent(typeof(Animation))).Play("Menu-MostrarBotones");
		((Animation)GameObject.Find("PanelNiveles").GetComponent(typeof(Animation))).Play("Menu-OcultarNiveles");
		((Animation)GameObject.Find("Niveles-Pag" + this.PaginaActual).GetComponent(typeof(Animation))).Play("Menu-OcultarSubNiveles");
	}

	public void SeleccionarOpciones()
	{
		this.GetComponent<AudioSource>().PlayOneShot(this.OnClickSound, this.VolumenSonido);
		((Animation)GameObject.Find("Letras").GetComponent(typeof(Animation))).Play("OcultarLetras");
		((Animation)GameObject.Find("Canvas").GetComponent(typeof(Animation))).Play("Menu-OcultarBotones");
		((Animation)GameObject.Find("PanelOpciones").GetComponent(typeof(Animation))).Play("Menu-MostrarNiveles");
	}

	public void PulsarSalirOpciones()
	{
		((Animation)GameObject.Find("Letras").GetComponent(typeof(Animation))).Play("MostrarLetras");
		((Animation)GameObject.Find("Letras").GetComponent(typeof(Animation))).PlayQueued("AnimacionLetras", QueueMode.CompleteOthers);
		((Animation)GameObject.Find("Canvas").GetComponent(typeof(Animation))).Play("Menu-MostrarBotones");
		((Animation)GameObject.Find("PanelOpciones").GetComponent(typeof(Animation))).Play("Menu-OcultarNiveles");
		PlayerPrefs.SetFloat("VolumenMusica", ((Slider)GameObject.Find("SliderMusica").GetComponent(typeof(Slider))).value);
		this.VolumenSonido = ((Slider)GameObject.Find("SliderSonido").GetComponent(typeof(Slider))).value;
		PlayerPrefs.SetFloat("VolumenSonido", this.VolumenSonido);
		this.GetComponent<AudioSource>().PlayOneShot(this.OnClickSound, this.VolumenSonido);
		if (this.HacambiadoIdioma)
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
		}
	}

	public void PalsarPagSig()
	{
		if (this.PaginaActual < 10)
		{
			this.GetComponent<AudioSource>().PlayOneShot(this.OnClickSound, this.VolumenSonido);
			((Image)GameObject.Find("Paginacion" + this.PaginaActual).GetComponent(typeof(Image))).sprite = this.Sprite_PuntoChico;
			((Animation)GameObject.Find("Niveles-Pag" + this.PaginaActual).GetComponent(typeof(Animation))).Play("Menu-SalirIzqSubNiveles");
			this.PaginaActual++;
			this.CheckFlechasPaginas();
			((Image)GameObject.Find("Paginacion" + this.PaginaActual).GetComponent(typeof(Image))).sprite = this.Sprite_PuntoGordo;
			((Animation)GameObject.Find("Niveles-Pag" + this.PaginaActual).GetComponent(typeof(Animation))).Play("Menu-MostrarSubNiveles");
		}
		else
		{
			this.GetComponent<AudioSource>().PlayOneShot(this.ErrorSound, this.VolumenSonido);
		}
	}

	public void PalsarPagAnt()
	{
		if (this.PaginaActual > 1)
		{
			this.GetComponent<AudioSource>().PlayOneShot(this.OnClickSound, this.VolumenSonido);
			((Image)GameObject.Find("Paginacion" + this.PaginaActual).GetComponent(typeof(Image))).sprite = this.Sprite_PuntoChico;
			((Animation)GameObject.Find("Niveles-Pag" + this.PaginaActual).GetComponent(typeof(Animation))).Play("Menu-OcultarSubNiveles");
			this.PaginaActual--;
			this.CheckFlechasPaginas();
			((Image)GameObject.Find("Paginacion" + this.PaginaActual).GetComponent(typeof(Image))).sprite = this.Sprite_PuntoGordo;
			((Animation)GameObject.Find("Niveles-Pag" + this.PaginaActual).GetComponent(typeof(Animation))).Play("Menu-EntrarIzqSubNiveles");
		}
		else
		{
			this.GetComponent<AudioSource>().PlayOneShot(this.ErrorSound, this.VolumenSonido);
		}
	}

	public void CheckFlechasPaginas()
	{
	}

	public void ValiarMusica()
	{
		if (((Slider)GameObject.Find("SliderMusica").GetComponent(typeof(Slider))).value == (float)0)
		{
			((Image)GameObject.Find("ImageSliderMusica").GetComponent(typeof(Image))).sprite = this.Sprite_Musica_Off;
		}
		else
		{
			((Image)GameObject.Find("ImageSliderMusica").GetComponent(typeof(Image))).sprite = this.Sprite_Musica_On;
		}
		((AudioSource)GameObject.Find("MusicaDeFondo").GetComponent(typeof(AudioSource))).GetComponent<AudioSource>().volume = ((Slider)GameObject.Find("SliderMusica").GetComponent(typeof(Slider))).value;
	}

	public void ValiarSonido()
	{
		if (((Slider)GameObject.Find("SliderSonido").GetComponent(typeof(Slider))).value == (float)0)
		{
			((Image)GameObject.Find("ImageSliderSonido").GetComponent(typeof(Image))).sprite = this.Sprite_Sonido_Off;
		}
		else
		{
			((Image)GameObject.Find("ImageSliderSonido").GetComponent(typeof(Image))).sprite = this.Sprite_Sonido_On;
		}
	}

	public void FinVariacion()
	{
		this.GetComponent<AudioSource>().PlayOneShot(this.OnClickSound, ((Slider)GameObject.Find("SliderSonido").GetComponent(typeof(Slider))).value);
	}

	public IEnumerator PlayNivel(int Nivel)
	{
	//	this._self__123.GetComponent<AudioSource>().PlayOneShot(this._self__123.OnClickSound, this._self__123.VolumenSonido);
	//				((Animation)GameObject.Find("PanelNiveles").GetComponent(typeof(Animation))).Play("Menu-OcultarNiveles");
	//				((Animation)GameObject.Find("Niveles-Pag" + this._self__123.PaginaActual).GetComponent(typeof(Animation))).Play("Menu-OcultarSubNiveles");
	//				GameObject.Find("Fades").SendMessage("BeginFade", 1);
	yield return new WaitForSeconds(1);
					UnityEngine.SceneManagement.SceneManager.LoadScene("Nivel" + PlayerPrefs.GetInt("NivelPagado"));

		//return new SelectMenu._PlayNivel_121(Nivel, this).GetEnumerator();
	}

	public void PulsarMaker()
	{
		this.GetComponent<AudioSource>().PlayOneShot(this.OnClickSound, this.VolumenSonido);
		UnityEngine.SceneManagement.SceneManager.LoadScene("Maker00");
	}

	public void PlayNivelError()
	{
		this.GetComponent<AudioSource>().PlayOneShot(this.ErrorSound, this.VolumenSonido);
	}

	public void ResetearJuego()
	{
		UnityEngine.Debug.Log("Resetear Juego");
		PlayerPrefs.DeleteAll();
		this.CheckValoresIniciales();
		UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
	}

	public void SeleccionarIdioma_0()
	{
		PlayerPrefs.SetInt("Idioma", 0);
		this.HacambiadoIdioma = true;
		this.PulsarSalirOpciones();
	}



	public void Main()
	{
	}
}
