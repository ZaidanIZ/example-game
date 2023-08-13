using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

///Developed by Indie Studio
///https://www.assetstore.unity3d.com/en/#!/publisher/9268
///www.indiestd.com
///info@indiestd.com
///copyright © 2016 IGS. All rights reserved.
#pragma warning disable 0168 // variable declared but not used.

[DisallowMultipleComponent]
public class UIEvents : MonoBehaviour
{
		public void ChangeMusicLevel (Slider slider)
		{
				if (slider == null) {
						return;
				}
				GameObject.Find ("AudioSources").GetComponents<AudioSource> () [0].volume = slider.value;
		}

		public void ChangeEffectsLevel (Slider slider)
		{
				if (slider == null) {
						return;
				}
				GameObject.Find ("AudioSources").GetComponents<AudioSource> () [1].volume = slider.value;
		}

		public void ShowResetGameConfirmDialog ()
		{
				GameObject.Find ("ResetGameConfirmDialog").GetComponent<ConfirmDialog> ().Show ();
				AudioSources.instance.PlayWaterBubbleSound ();
		}

		public void ShowExitConfirmDialog ()
		{
				GameObject.Find ("ExitConfirmDialog").GetComponent<ConfirmDialog> ().Show ();
				AudioSources.instance.PlayWaterBubbleSound ();
		}

		public void ResetGameConfirmDialogEvent (GameObject value)
		{
				if (value == null) {
						return;
				}

				if (value.name.Equals ("YesButton")) {
						Debug.Log ("Reset Game Confirm Dialog : No button clicked");
						DataManager.instance.ResetGameData ();
				} else if (value.name.Equals ("NoButton")) {
						Debug.Log ("Reset Game Confirm Dialog : No button clicked");
				}
				GameObject.Find ("ResetGameConfirmDialog").GetComponent<ConfirmDialog> ().Hide ();
				AudioSources.instance.PlayWaterBubbleSound ();
		}

		public void ExitConfirmDialogEvent (GameObject value)
		{
				if (value.name.Equals ("YesButton")) {
						Debug.Log ("Exit Confirm Dialog : Yes button clicked");
						Application.Quit ();
				} else if (value.name.Equals ("NoButton")) {
						Debug.Log ("Exit Confirm Dialog : No button clicked");
				}
				GameObject.Find ("ExitConfirmDialog").GetComponent<ConfirmDialog> ().Hide ();
				AudioSources.instance.PlayWaterBubbleSound ();
		}

		public void MissionButtonEvent (Mission value)
		{
				if (value == null) {
						Debug.Log ("Event parameter value is undefined");
						return;
				}

				Mission.selectedMission = value;
				LoadLevelsScene ();
		}

		public void LevelButtonEvent (TableLevel value)
		{
				if (value == null) {
						Debug.Log ("Event parameter value is undefined");
						return;
				}

				TableLevel.selectedLevel = value;
				LevelsTable.selectedLevelID = TableLevel.selectedLevel.ID;
				LoadGameScene ();
		}

		public void GridCellButtonEvent (GridCell gridCell)
		{
				if (gridCell == null || !GameManager.enableClick) {
						return;
				}

				GameManager gameManager = GameObject.FindObjectOfType (typeof(GameManager)) as GameManager;

		
				if (gridCell.alreadyUsed) {
						return;
				}

				if (GameManager.previousGridCell != null ? GameManager.previousGridCell.index == gridCell.index : false) {
						//Reset the current grid cell on twice click
						gameManager.ResetGridCells (0, 0, new GridCell[] {
							gridCell
						});
						GameManager.previousGridCell = null;
						return;
				}

				//Enable the behind blured image 
				gridCell.transform.Find ("Blured").GetComponent<Animator> ().SetTrigger("isRunning");
				//Set onclick sprite
				gridCell.transform.Find ("Content").GetComponent<Image> ().sprite = GameManager.currentLevel.pairs [gridCell.pairID].onClickSprite;
			
				if (GameManager.previousGridCell == null) {
						GameManager.previousGridCell = gridCell;
				} else {
						if (gridCell.pairID == GameManager.previousGridCell.pairID) {
								gridCell.alreadyUsed = GameManager.previousGridCell.alreadyUsed = true;
								bool completed = gameManager.CheckLevelComplete ();
								if (!completed) {
										//Play correct sound effect
										gameManager.PlayCorrectSFX ();
								} else {
										//Play completed sound effect
										gameManager.PlayCompletedSFX ();
								}
								//Hide the behind blured image
								GameManager.previousGridCell.transform.Find ("Blured").GetComponent<Animator> ().SetBool("isRunning",false);
								gridCell.transform.Find ("Blured").GetComponent<Animator> ().SetBool("isRunning",false);
								Debug.Log ("New pair found");
						} else {
								//Reset current and previous grid cells
								gameManager.ResetGridCells (0.7f, 0, new GridCell[] {
										GameManager.previousGridCell,
										gridCell
								});
								//Play wrong sound effect
								gameManager.PlayWrongSFX ();
								Debug.Log ("Not a pair");
						}
						GameManager.previousGridCell = null;
				}
		}

		public void GameNextButtonEvent ()
		{
				GameObject.Find ("GameScene").GetComponent<GameManager> ().NextLevel ();
		}

		public void GameBackButtonEvent ()
		{
				GameObject.Find ("GameScene").GetComponent<GameManager> ().PreviousLevel ();
		}

		public void GameMenuButtonEvent ()
		{
				try {
						if (Mission.selectedMission.levelsManagerComponent.singleLevel) {
								LoadMissionsScene ();
						} else {
								LoadLevelsScene ();
						}
				} catch (Exception ex) {
						///catch the exception
				}
		}

		public void GameRefreshButtonEvent ()
		{
				GameObject.Find ("GameScene").GetComponent<GameManager> ().RefreshGrid ();
		}

		public void WinDialogNextButtonEvent ()
		{
				if (Mission.selectedMission.levelsManagerComponent.singleLevel) {
						LoadMissionsScene ();
						return;
				}

				if (LevelsTable.selectedLevelID == LevelsTable.tableLevels.Count) {
						LoadLevelsScene ();
						return;
				}
				BlackArea.Hide ();
				GameObject.FindObjectOfType<WinDialog> ().Hide ();
				GameObject.Find ("GameScene").GetComponent<GameManager> ().NextLevel ();
		}

		public void LoadMainScene ()
		{
				AudioSources.instance.PlayWaterBubbleSound ();
				StartCoroutine (LoadSceneAsync ("Main"));
		}

		public void LoadHowToPlayScene ()
		{
				AudioSources.instance.PlayWaterBubbleSound ();
				StartCoroutine (LoadSceneAsync ("HowToPlay"));
		}

		public void LoadMissionsScene ()
		{
				AudioSources.instance.PlayWaterBubbleSound ();
				StartCoroutine (LoadSceneAsync ("Missions"));
		}

		public void LoadOptionsScene ()
		{
				AudioSources.instance.PlayWaterBubbleSound ();
				StartCoroutine (LoadSceneAsync ("Options"));
		}

		public void LoadLevelsScene ()
		{
				AudioSources.instance.PlayWaterBubbleSound ();
				StartCoroutine (LoadSceneAsync ("Levels"));
		}

		public void LoadGameScene ()
		{
				AudioSources.instance.PlayWaterBubbleSound ();
				StartCoroutine (LoadSceneAsync ("Game"));
		}

		/// <summary>
		/// Loads the scene Async.
		/// </summary>
		IEnumerator LoadSceneAsync (string sceneName)
		{
				if (!string.IsNullOrEmpty (sceneName)) {
						#if UNITY_PRO_LICENSE
							AsyncOperation async = SceneManager.LoadSceneAsync (sceneName);
							while (!async.isDone) {
							yield return 0;
						}
						#else
						SceneManager.LoadScene (sceneName);
						yield return 0;
						#endif
				}
		}
}