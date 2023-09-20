using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ContarPuntosPre : MonoBehaviour
{
	public void Start()
	{
		int puntos = ((ControlMovimiento)GameObject.FindGameObjectWithTag("Player").GetComponent(typeof(ControlMovimiento))).Puntos;
		((Text)GameObject.Find("Txt_Salario").GetComponent(typeof(Text))).text = puntos.ToString();
	}

	public void Main()
	{
	}
}
