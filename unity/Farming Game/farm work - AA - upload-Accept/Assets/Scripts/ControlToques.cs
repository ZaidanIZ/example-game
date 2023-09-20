using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class ControlToques : MonoBehaviour
{
	public string Mensage;

	public ControlToques()
	{
		this.Mensage = string.Empty;
	}

	public void Start()
	{
		this.Mensage = string.Empty;
	}

	public void Update()
	{
		if (this.Mensage != string.Empty)
		{
			GameObject.FindWithTag("Player").SendMessage("Mover", this.Mensage);
		}
	}

	public void Soltar()
	{
		this.Mensage = string.Empty;
	}

	public void Arriba_Pulsar()
	{
		this.Mensage = "Arriba";
	}

	public void Abajo_Pulsar()
	{
		this.Mensage = "Abajo";
	}

	public void Derecha_Pulsar()
	{
		this.Mensage = "Derecha";
	}

	public void Izquierda_Pulsar()
	{
		this.Mensage = "Izquierda";
	}

	public void PulsarPause()
	{
		GameObject.FindWithTag("Player").SendMessage("PulsarBoton", "Stop");
	}

	public void PulsarSalirAMenu()
	{
		SceneManager.LoadScene("MainMenu");
		//GameObject.FindWithTag("Player").SendMessage("PulsarBoton", "ExitMenu");
	}

	public void PulsarContinue()
	{
		GameObject.FindWithTag("Player").SendMessage("PulsarBoton", "Continue");
	}

	public void PulsarEmpecemos()
	{
		UnityEngine.Debug.Log("Pulsar Empezar");
		GameObject.FindWithTag("Player").SendMessage("PulsarBoton", "Empezar");
	}

	public void PulsarReStart()
	{

				SceneManager.LoadScene(SceneManager.GetActiveScene().name);

		//GameObject.FindWithTag("Player").SendMessage("PulsarBoton", "ReStart");
	}

	public void PulsarSiguiente()
	{
		int Level  = PlayerPrefs.GetInt("LEVEL",1);
		PlayerPrefs.SetInt("LEVEL",Level+1);
		SceneManager.LoadScene("Level"+(Level+1));

		//GameObject.FindWithTag("Player").SendMessage("PulsarBoton", "NextNivel");
	}

	public void PulsarSalirNoDisponible()
	{
		GameObject.FindWithTag("Player").SendMessage("PulsarBoton", "SalirNoDisponible");
	}

	public void Main()
	{
	}
}
