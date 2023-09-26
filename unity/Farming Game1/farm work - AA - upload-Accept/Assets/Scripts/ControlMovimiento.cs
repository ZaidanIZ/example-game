//using Boo.Lang;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
//using UnityScript.Lang;

[Serializable]
public class ControlMovimiento : MonoBehaviour
{
	

	public int NumeroNivel;

	public float Velocidad;

	public int Puntos;

	public Vector3 Destino;

	public int x;

	public int y;

	public int LastScoreSum;

	public int Lastdx;

	public int Lastdy;

	public int LastD;

	public int EstadoJuego;

	public float AlfaTime;

	public AudioClip Sonido_BotonClick;

	public AudioClip Sonido_ArrancarMotor;

	public AudioClip Sonido_Repetir;

	public AudioClip Sonido_Perder;

	public ControlMovimiento()
	{
		this.Velocidad = 1.5f;
	}

	public void Start()
	{
		if (ADsummoner.adSummoner != null)
			ADsummoner.adSummoner.LoadInterstitial();

			this.NumeroNivel = (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
		UnityEngine.Debug.Log("Cargado Nivel numero:" + this.NumeroNivel);
		this.x = (int)Mathf.Round(this.transform.position.x / (float)4);
		this.y = (int)Mathf.Round(this.transform.position.z / (float)4);
		this.transform.position = new Vector3((float)(this.x * 4), (float)0, (float)(this.y * 4));
		this.Destino = this.transform.position;
		this.LastScoreSum = 0;
		this.Lastdx = 0;
		this.Lastdy = 0;
		this.LastD = 0;
		this.EstadoJuego = 0;
		this.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("VolumenSonido");
		//((Image)GameObject.Find("Mapa").GetComponent(typeof(Image))).sprite = (((Sprite)Resources.Load("Nivel" + this.NumeroNivel.ToString("D2"), typeof(Sprite))) as Sprite);
		this.Pausar("Nuevo");
	}

	public void Update()
	{
		this.transform.position = Vector3.MoveTowards(this.transform.position, this.Destino, this.Velocidad * 4f * Time.deltaTime);
		this.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(this.transform.forward, this.Destino - this.transform.position, Time.deltaTime * this.Velocidad * 3f, (float)0));
		this.x = (int)Mathf.Round(this.transform.position.x / (float)4);
		this.y = (int)Mathf.Round(this.transform.position.z / (float)4);
		if (this.transform.position == this.Destino && this.EstadoJuego == 0)
		{
			GameObject exists = GameObject.FindWithTag("ParcelaLibre");
			if (exists)
			{
				if (!this.CheckDestino(1, 0) && !this.CheckDestino(-1, 0) && !this.CheckDestino(0, 1) && !this.CheckDestino(0, -1))
				{
					this.Pausar("Perder");
				}
			}
			else
			{
				this.Pausar("Ganar");
			}
		}
		if (this.AlfaTime > (float)0)
		{
			this.AlfaTime -= Time.deltaTime;
			float a = this.AlfaTime / 0.3f;
			Color color = ((Text)GameObject.Find("PuntosSumados").GetComponent(typeof(Text))).color;
			float num = color.a = a;
			Color color2 = ((Text)GameObject.Find("PuntosSumados").GetComponent(typeof(Text))).color = color;
		}
	}

	public void Mover(string Direccion)
	{
		if (this.transform.position == this.Destino)
		{
			if (Direccion == "Arriba")
			{
				if (this.CheckDestino(0, 1) && this.EstadoJuego == 0)
				{
					this.Destino = this.transform.position + new Vector3((float)0, (float)0, 4f);
					this.CambiarArtualAArada(1);
					this.CambiarDestinoAPisada(0, 1);
				}
			}
			else if (Direccion == "Abajo")
			{
				if (this.CheckDestino(0, -1) && this.EstadoJuego == 0)
				{
					this.Destino = this.transform.position - new Vector3((float)0, (float)0, 4f);
					this.CambiarArtualAArada(2);
					this.CambiarDestinoAPisada(0, -1);
				}
			}
			else if (Direccion == "Izquierda")
			{
				if (this.CheckDestino(-1, 0) && this.EstadoJuego == 0)
				{
					this.Destino = this.transform.position - new Vector3(4f, (float)0, (float)0);
					this.CambiarArtualAArada(3);
					this.CambiarDestinoAPisada(-1, 0);
				}
			}
			else if (Direccion == "Derecha")
			{
				if (this.CheckDestino(1, 0) && this.EstadoJuego == 0)
				{
					this.Destino = this.transform.position + new Vector3(4f, (float)0, (float)0);
					this.CambiarArtualAArada(4);
					this.CambiarDestinoAPisada(1, 0);
				}
			}
		}
	}

	public IEnumerator PulsarBoton(string Opcion)
	{
	
					
					if (Opcion == "Stop")
					{
						GetComponent<AudioSource>().PlayOneShot(Sonido_BotonClick);
						Pausar("Pausar");
					
					}
					else if (Opcion == "Continue")
					{
						GetComponent<AudioSource>().PlayOneShot(Sonido_BotonClick);
						Pausar("Continue");
			Debug.Log("panggil  ads interstitial");
			if (ADsummoner.adSummoner != null)
            {
				ADsummoner.adSummoner.ShowInterstitial();
				ADsummoner.adSummoner.LoadInterstitial();
			}
				

			;
					}
					else if (Opcion == "Empezar")
					{
						GetComponent<AudioSource>().PlayOneShot(Sonido_BotonClick);
						Pausar("Empezar");
				
					}
					else if (Opcion == "ReStart")
					{
						GetComponent<AudioSource>().PlayOneShot(Sonido_Repetir, 0.8f);
						if (EstadoJuego == 1)
						{
							((Animation)GameObject.Find("Canvas").GetComponent(typeof(Animation))).Play("EsconderCartelPausa2");
						}
						else if (EstadoJuego == 2)
						{
							if (GameObject.Find("Tapon").GetComponent<AudioSource>().enabled)
							{
								((Animation)GameObject.Find("Canvas").GetComponent(typeof(Animation))).Play("EsconderCartelGanarRecord");
							}
							else
							{
								((Animation)GameObject.Find("Canvas").GetComponent(typeof(Animation))).Play("EsconderCartelGanar");
							}
						}
						else if (EstadoJuego == 3)
						{
							((Animation)GameObject.Find("Canvas").GetComponent(typeof(Animation))).Play("EsconderCartelPerder");
						}
						else{}
						//GameObject.Find("Fades").SendMessage("BeginFade", 1);
					}
					else if (Opcion == "ExitMenu")
					{
						GetComponent<AudioSource>().PlayOneShot(Sonido_BotonClick);
						if (EstadoJuego == 1)
						{
							((Animation)GameObject.Find("Canvas").GetComponent(typeof(Animation))).Play("EsconderCartelPausa2");
						}
						else if (EstadoJuego == 2)
						{
							if (GameObject.Find("Tapon").GetComponent<AudioSource>().enabled)
							{
								((Animation)GameObject.Find("Canvas").GetComponent(typeof(Animation))).Play("EsconderCartelGanarRecord");
							}
							else
							{
								((Animation)GameObject.Find("Canvas").GetComponent(typeof(Animation))).Play("EsconderCartelGanar");
							}
						}
						else if (EstadoJuego == 3)
						{
							((Animation)GameObject.Find("Canvas").GetComponent(typeof(Animation))).Play("EsconderCartelPerder");
						}
						else if (EstadoJuego == 4)
						{
							((Animation)GameObject.Find("Canvas").GetComponent(typeof(Animation))).Play("EsconderCartelNuevo2");
						}else{}
						
					}
					else if (Opcion == "NextNivel")
					{
						UnityEngine.Debug.Log("NextNivel");
						GetComponent<AudioSource>().PlayOneShot(Sonido_BotonClick);
						if (GameObject.Find("Tapon").GetComponent<AudioSource>().enabled)
						{
							((Animation)GameObject.Find("Canvas").GetComponent(typeof(Animation))).Play("EsconderCartelGanarRecord");
						}
						else
						{
							((Animation)GameObject.Find("Canvas").GetComponent(typeof(Animation))).Play("EsconderCartelGanar");
						}
						UnityEngine.Debug.Log(" NumeroNivel:" + NumeroNivel + " JuegoComprado:" + PlayerPrefs.GetInt("JuegoComprado") + " NivelDesbloqueado:" + PlayerPrefs.GetInt("NivelDesbloqueado") + " NivelPagado:" + PlayerPrefs.GetInt("NivelPagado"));
						
						((ControlFechas)GameObject.Find("Txt_Disponible2").GetComponent(typeof(ControlFechas))).Activo = true;
						((Animation)GameObject.Find("NivelNoDisponible").GetComponent(typeof(Animation))).Play("SacarCarteNoDisponible");
						UnityEngine.Debug.Log("Nivel Bloqueado");
							NumeroNivel = NumeroNivel + 1;
					UnityEngine.SceneManagement.SceneManager.LoadScene("Nivel" + NumeroNivel.ToString("D2"));
						
					}
					else
					{
						if (Opcion == "SalirNoDisponible")
						{
							GameObject.Find("Tapon").GetComponent<AudioSource>().enabled = false;
							((ControlFechas)GameObject.Find("Txt_Disponible2").GetComponent(typeof(ControlFechas))).Activo = false;
							if (PlayerPrefs.GetInt("Idioma") == 1)
							{
								((Text)GameObject.Find("GanarPerder").GetComponent(typeof(Text))).text = "Lo dicho...has cobrado y tu siguiente trabajo te espera";
							}
							else
							{
								((Text)GameObject.Find("GanarPerder").GetComponent(typeof(Text))).text = "I said ... you've collected and awaits your next job";
							}
							((Text)GameObject.Find("Txt_Salario").GetComponent(typeof(Text))).text = string.Empty;
							((Animation)GameObject.Find("NivelNoDisponible").GetComponent(typeof(Animation))).Play("EsconderCarteNoDisponible");
							((Animation)GameObject.Find("Canvas").GetComponent(typeof(Animation))).Play("SacarCartelGanar3");
						}
					}
					
			//	case 2:
			//		UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
			//		goto IL_798;
			//	case 3:
			//		UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
			//		goto IL_798;
			//	case 4:
			//		NumeroNivel = NumeroNivel + 1;
			//		UnityEngine.SceneManagement.SceneManager.LoadScene("Nivel" + NumeroNivel.ToString("D2"));
			//		goto IL_661;
				yield return new WaitForSeconds(1);
				
			}


	//	return new ControlMovimiento._PulsarBoton_112(Opcion, this).GetEnumerator();
	

	public void Pausar(string Caso)
	{
		if (Caso == "Ganar")
		{
			UnityEngine.Debug.Log("GANASTE!!!!");
			Debug.Log("panggil  ads interstitial");

			if (ADsummoner.adSummoner != null)
			
				ADsummoner.adSummoner.ShowInterstitial();
			


			this.EstadoJuego = 2;
			if (PlayerPrefs.GetInt("Idioma") == 1)
			{
				((Text)GameObject.Find("GanarPerder").GetComponent(typeof(Text))).text = "Has cobrado y tu siguiente trabajo te espera";
			}
			else
			{
				((Text)GameObject.Find("GanarPerder").GetComponent(typeof(Text))).text = "You've collected and awaits your next job";
			}
			((Animation)GameObject.Find("TiempoBg").GetComponent(typeof(Animation))).Stop();
			((Animation)GameObject.Find("Canvas").GetComponent(typeof(Animation))).Play("SacarCartelGanar2");
			if (PlayerPrefs.GetInt("NivelDesbloqueado") == this.NumeroNivel)
			{
				PlayerPrefs.SetInt("NivelDesbloqueado", this.NumeroNivel + 1);
			}
			if (this.NumeroNivel >= 3 && PlayerPrefs.GetInt("JuegoComprado") != 1 && PlayerPrefs.GetInt("NivelDesbloqueado") != PlayerPrefs.GetInt("NivelPagado") && this.NumeroNivel >= PlayerPrefs.GetInt("NivelPagado"))
			{
				this.GuardarFecha();
			}
			((ControlFechas)GameObject.Find("Txt_Disponible").GetComponent(typeof(ControlFechas))).Activo = true;
            //Adcontrol.instance.ShowInterstitial();

        }
        else if (Caso == "Perder")
		{
			UnityEngine.Debug.Log("PERDISTE!!!!");
			this.EstadoJuego = 3;
			if (PlayerPrefs.GetInt("Idioma") == 1)
			{
				((Text)GameObject.Find("GanarPerder").GetComponent(typeof(Text))).text = "Te has quedado atrapado y no puedes continuar";
			}
			else
			{
				((Text)GameObject.Find("GanarPerder").GetComponent(typeof(Text))).text = "You have been trapped and can not continue";
               // Adcontrol.instance.ShowInterstitial();
			}
			((Animation)GameObject.Find("Canvas").GetComponent(typeof(Animation))).Play("SacarCartelPerder");
			((AudioSource)GameObject.Find("MusicaDeFondo").GetComponent(typeof(AudioSource))).Stop();
			((AudioSource)GameObject.Find("MusicaDeFondo").GetComponent(typeof(AudioSource))).PlayOneShot(this.Sonido_Perder);
		}
		else if (Caso == "Pausar")
		{
			this.EstadoJuego = 1;
			if (PlayerPrefs.GetInt("Idioma") == 1)
			{
				((Text)GameObject.Find("GanarPerder").GetComponent(typeof(Text))).text = "Trabajo Pausado.\nEstas haciendo un descansito?";
			}
			else
			{
				((Text)GameObject.Find("GanarPerder").GetComponent(typeof(Text))).text = "Job paused.\nAre you doing a little break?";
              //  Adcontrol.instance.ShowInterstitial();

            }
            ((Animation)GameObject.Find("Canvas").GetComponent(typeof(Animation))).Play("SacarCartelPausa");
		}
		else if (Caso == "Continue")
		{
			this.EstadoJuego = 0;
			((Animation)GameObject.Find("Canvas").GetComponent(typeof(Animation))).Play("EsconderCartelPausa");
		}
		else if (Caso == "Nuevo")
		{
			this.EstadoJuego = 4;
			if (PlayerPrefs.GetInt("Idioma") == 1)
			{
				((Text)GameObject.Find("GanarPerder").GetComponent(typeof(Text))).text = "Listo el trabajo nº: " + this.NumeroNivel.ToString("D2");
			}
			else
			{
				((Text)GameObject.Find("GanarPerder").GetComponent(typeof(Text))).text = "Ready for Job No." + this.NumeroNivel.ToString("D2");
			}
			((Animation)GameObject.Find("Canvas").GetComponent(typeof(Animation))).Play("SacarCartelNuevo");
		}
		else if (Caso == "Empezar")
		{
			this.EstadoJuego = 0;
			((Animation)GameObject.Find("Canvas").GetComponent(typeof(Animation))).Play("EsconderCartelNuevo");
		}
	}

	public void GuardarFecha()
	{
		DateTime dateTime = DateTime.Now.AddMinutes((double)90);
		if (PlayerPrefs.GetString("TiempoHastaPagarNivel") == string.Empty)
		{
			PlayerPrefs.SetString("TiempoHastaPagarNivel", dateTime.ToString());
		}
		UnityEngine.Debug.Log("Guardar Fecha Para Liberar Nivel: " + dateTime);
		UnityEngine.Debug.Log("Fechar Guardada Real: " + PlayerPrefs.GetString("TiempoHastaPagarNivel"));
	}

	public void PublicidadVista()
	{
		PlayerPrefs.SetInt("NivelPagado", PlayerPrefs.GetInt("NivelDesbloqueado"));
		PlayerPrefs.SetString("TiempoHastaPagarNivel", string.Empty);
		this.StartCoroutine(this.PulsarBoton("SalirNoDisponible"));
	}

	public bool CheckDestino(int dx, int dy)
	{
		GameObject exists = GameObject.Find("Parcelita:" + (this.x + dx) + "," + (this.y + dy));
		bool arg_A4_0;
		if (exists)
		{
			ControlParcela controlParcela = (ControlParcela)GameObject.Find("Parcelita:" + (this.x + dx) + "," + (this.y + dy)).GetComponent(typeof(ControlParcela));
			arg_A4_0 = !controlParcela.Arada;
		}
		else
		{
			arg_A4_0 = false;
		}
		return arg_A4_0;
	}

	public void CambiarArtualAArada(int Dir)
	{
		GameObject gameObject = GameObject.Find("Parcelita:" + this.x + "," + this.y);
		int num = 0;
		num = 1;
		if (this.LastD != Dir)
		{
			if ((this.LastD == 2 && Dir == 4) || (this.LastD == 1 && Dir == 3) || (this.LastD == 4 && Dir == 1) || (this.LastD == 3 && Dir == 2))
			{
				num = 3;
			}
			else
			{
				num = 2;
			}
		}
		this.LastD = Dir;
		if (gameObject)
		{
			gameObject.SendMessage("ChangeToArada", num);
		}
	}

	public void CambiarDestinoAPisada(int dx, int dy)
	{
		GameObject gameObject = GameObject.Find("Parcelita:" + (this.x + dx) + "," + (this.y + dy));
		if (gameObject)
		{
			gameObject.SendMessage("ChangeToPisada", this.QueAngulo(dx, dy));
			this.UpdateScore(dx, dy);
		}
	}

	public float QueAngulo(int dx, int dy)
	{
		return (dx != -1) ? ((dx != 1) ? ((dy != -1) ? ((float)0) : ((float)180)) : ((float)90)) : ((float)270);
	}

	public void UpdateScore(int dx, int dy)
	{
		if (this.Puntos == 0)
		{
			this.GetComponent<AudioSource>().PlayOneShot(this.Sonido_ArrancarMotor, 0.8f);
		}
		if (this.Lastdx == dx && this.Lastdy == dy)
		{
			this.LastScoreSum += 10;
		}
		else
		{
			this.LastScoreSum = 10;
			this.Lastdx = dx;
			this.Lastdy = dy;
		}
		this.Puntos += this.LastScoreSum;
		((Text)GameObject.Find("PuntosSumados").GetComponent(typeof(Text))).text = "+" + this.LastScoreSum;
		((Text)GameObject.Find("PuntosSumados").GetComponent(typeof(Text))).color = this.SetSumColor(this.LastScoreSum / 10);
		this.AlfaTime = 0.5f;
		((Animation)GameObject.Find("Canvas").GetComponent(typeof(Animation))).Play("SumandoPuntos");
		((Text)GameObject.FindWithTag("Puntuacion").GetComponent(typeof(Text))).text = string.Empty + this.Puntos;
	}

	public Color32 SetSumColor(int valor)
	{
		Color32 result = new Color32(152, 170, 255, 255);
		int num = valor % 5;
		if (num == 2)
		{
			result = new Color32(255, 239, 152, 255);
		}
		else if (num == 3)
		{
			result = new Color32(249, 152, 255, 255);
		}
		else if (num == 4)
		{
			result = new Color32(152, 255, 178, 255);
		}
		else if (num == 0)
		{
			result = new Color32(255, 152, 152, 255);
		}
		return result;
	}

	public void Main()
	{
	}
}
