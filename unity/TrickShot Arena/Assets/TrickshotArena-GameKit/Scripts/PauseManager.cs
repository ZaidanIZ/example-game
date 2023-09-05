using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Reflection;
//using UnityEditor;
using System;

namespace TrickshotArena
{
	public class PauseManager : MonoBehaviour
	{
		/// <summary>
		/// This class manages pause and unpause states.
		/// </summary>

		internal bool isPaused;
		private float savedTimeScale;
		public GameObject pausePlane;

		enum Page { PLAY, PAUSE }
		private Page currentPage = Page.PLAY;

		void Awake()
		{
			isPaused = false;

			Time.timeScale = 1.0f;
			Time.fixedDeltaTime = 0.02f;

			if (pausePlane)
				pausePlane.SetActive(false);
		}

		void Update()
		{
			if (!GlobalGameManager.gameIsStarted)
				return;

			//optional pause in Editor & Windows (just for debug)
			if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyUp(KeyCode.Escape))
			{
				//PAUSE THE GAME
				switch (currentPage)
				{
					case Page.PLAY:
						PauseGame();
						break;
					case Page.PAUSE:
						UnPauseGame();
						break;
					default:
						currentPage = Page.PLAY;
						break;
				}
			}
		}

		void LateUpdate()
		{
			//debug restart
			if (Input.GetKeyDown(KeyCode.R))
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			}
		}

		/// <summary>
		/// Pause the game
		/// </summary>
		void PauseGame()
		{
			print("Game is Paused...");
			isPaused = true;
			savedTimeScale = Time.timeScale;
			Time.timeScale = 0;
			AudioListener.volume = 0;

			if (pausePlane)
				pausePlane.SetActive(true);

			currentPage = Page.PAUSE;
		}

		/// <summary>
		/// Unpause the game
		/// </summary>
		void UnPauseGame()
		{
			print("Unpause");
			isPaused = false;
			Time.timeScale = savedTimeScale;
			AudioListener.volume = 1.0f;

			if (pausePlane)
				pausePlane.SetActive(false);


			currentPage = Page.PLAY;
		}

		public void ClickOnPauseButton()
		{
			switch (currentPage)
			{
				case Page.PLAY:
					PauseGame();
					break;
				case Page.PAUSE:
					UnPauseGame();
					break;
				default:
					currentPage = Page.PLAY;
					break;
			}
		}

		public void ClickOnResumeButton()
		{
			switch (currentPage)
			{
				case Page.PLAY:
					PauseGame();
					break;
				case Page.PAUSE:
					UnPauseGame();
					break;
				default:
					currentPage = Page.PLAY;
					break;
			}
		}

		public void ClickOnRestartButton()
		{
			UnPauseGame();
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}	
}