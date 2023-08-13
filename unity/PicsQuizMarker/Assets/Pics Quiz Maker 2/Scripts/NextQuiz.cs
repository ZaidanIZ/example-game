/// <summary>
///
///----------- ESPAÑOL -----------
/// 
/// La funcion "nextQuizScene" es llamada cuando se presiona el boton de PLAY o el de "Siguiente nivel".
/// La funcion "Awake" se llama cuando se abre el juego por primera vez para decirle a la camara que no se
/// destruya al recargar la escena.
/// 
///----------- ENGLISH -----------
/// 
/// The "nextQuizScene" function is called when is pressed the "PLAY" button or "Next Level" button.
/// The "Awake" function is called when the game is opening in the first time for don't destroy the camera
/// when the scene is reload.
/// 
/// </summary>

using UnityEngine;
using System.Collections;

public class NextQuiz : MonoBehaviour 
{

	void nextQuizScene () 
	{
		GameObject.Find("Main Camera").transform.position = new Vector3 (0, 0, -10);
		Application.LoadLevel(Application.loadedLevelName);
	}

	void Awake ()
	{

		DontDestroyOnLoad(this);
		
		if (FindObjectsOfType(GetType()).Length > 1)
		{
			Destroy(gameObject);
		}

	}
	
}
