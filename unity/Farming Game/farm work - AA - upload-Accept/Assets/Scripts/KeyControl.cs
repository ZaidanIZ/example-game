using System;
using UnityEngine;

[Serializable]
public class KeyControl : MonoBehaviour
{
	public void Start()
	{
	}

	public void Update()
	{
		if (UnityEngine.Input.GetKey("up"))
		{
			GameObject.FindWithTag("Player").SendMessage("Mover", "Arriba");
		}
		if (UnityEngine.Input.GetKey("down"))
		{
			GameObject.FindWithTag("Player").SendMessage("Mover", "Abajo");
		}
		if (UnityEngine.Input.GetKey("left"))
		{
			GameObject.FindWithTag("Player").SendMessage("Mover", "Izquierda");
		}
		if (UnityEngine.Input.GetKey("right"))
		{
			GameObject.FindWithTag("Player").SendMessage("Mover", "Derecha");
		}
	}

	public void Main()
	{
	}
}
