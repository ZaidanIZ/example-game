using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TrickshotArena
{
	public class InitManager : MonoBehaviour
	{
		/// <summary>
		/// We need to use this loader to init the AdManager singleton 
		/// </summary>

		IEnumerator Start()
		{
			//PlayerPrefs.DeleteAll();
			Application.targetFrameRate = 60;
			yield return new WaitForSeconds(0.05f);
			SceneManager.LoadScene("Game");
		}
	}
}