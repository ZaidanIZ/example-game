using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class CargarMenu : MonoBehaviour
{
	public void LoadLevel()
	{
		SceneManager.LoadScene("Menu");
	}

	public void Main()
	{
	}
}
