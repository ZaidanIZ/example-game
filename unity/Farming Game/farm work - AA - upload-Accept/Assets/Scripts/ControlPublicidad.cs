using System;
using UnityEngine;

public class ControlPublicidad : MonoBehaviour
{
	private void Start()
	{
	}

	public void VerPublicidad()
	{
		GameObject.FindWithTag("Player").SendMessage("PublicidadVista");
	}
}
