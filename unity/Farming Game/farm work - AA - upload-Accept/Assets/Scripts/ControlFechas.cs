using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ControlFechas : MonoBehaviour
{
	public bool Activo;

	public bool esBoton;

	public ControlFechas()
	{
		this.Activo = true;
		this.esBoton = true;
	}

	public void Start()
	{
		if (this.CheckSiProcede())
		{
			UnityEngine.Debug.Log("Cuenta atras empieza Conectado");
		}
		else
		{
			UnityEngine.Debug.Log("Cuenta atras empieza desconectado");
		}
	}

	public void Update()
	{
		if (this.Activo && ((ControlMovimiento)GameObject.FindGameObjectWithTag("Player").GetComponent(typeof(ControlMovimiento))).EstadoJuego == 2)
		{
			if (this.CheckSiProcede())
			{
				this.ContarTiempo();
			}
			else
			{
				((Text)this.GetComponent(typeof(Text))).text = string.Empty;
				if (!this.esBoton)
				{
					GameObject.FindWithTag("Player").SendMessage("PulsarBoton", "SalirNoDisponible");
				}
			}
		}
	}

	public bool CheckSiProcede()
	{
		DateTime t = default(DateTime);
		DateTime t2 = default(DateTime);
		DateTime.TryParse(PlayerPrefs.GetString("TiempoHastaPagarNivel"), out t);
		t2 = DateTime.Now;
		bool arg_A2_0;
		if (t < t2)
		{
			UnityEngine.Debug.Log("Cuenta atras Cumplida, pagamos Nivel y desconectamos");
			this.Activo = false;
			PlayerPrefs.SetInt("NivelPagado", PlayerPrefs.GetInt("NivelDesbloqueado"));
			PlayerPrefs.SetString("TiempoHastaPagarNivel", string.Empty);
			arg_A2_0 = false;
		}
		else
		{
			arg_A2_0 = (((ControlMovimiento)GameObject.FindGameObjectWithTag("Player").GetComponent(typeof(ControlMovimiento))).NumeroNivel == PlayerPrefs.GetInt("NivelPagado"));
		}
		return arg_A2_0;
	}

	public void ContarTiempo()
	{
		DateTime d = default(DateTime);
		DateTime d2 = default(DateTime);
		long num = 0L;
		long num2 = 0L;
		long num3 = 0L;
		DateTime.TryParse(PlayerPrefs.GetString("TiempoHastaPagarNivel"), out d);
		d2 = DateTime.Now;
		num = (long)(d - d2).Hours;
		num2 = (long)(d - d2).Minutes;
		num3 = (long)(d - d2).Seconds;
		num2 += num * 60L;
		((Text)this.GetComponent(typeof(Text))).text = num2.ToString("D2") + "m " + num3.ToString("D2") + "s";
		if (this.esBoton)
		{
			if (PlayerPrefs.GetInt("Idioma") == 1)
			{
				((Text)this.GetComponent(typeof(Text))).text = "Disponible en \n" + ((Text)this.GetComponent(typeof(Text))).text;
			}
			else
			{
				((Text)this.GetComponent(typeof(Text))).text = "Available in \n" + ((Text)this.GetComponent(typeof(Text))).text;
			}
		}
	}

	public void Main()
	{
	}
}
