using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

#pragma warning disable 0168 // variable declared but not used.

///Developed by Indie Studio
///https://www.assetstore.unity3d.com/en/#!/publisher/9268
///www.indiestd.com
///info@indiestd.com
///copyright © 2016 IGS. All rights reserved.

[DisallowMultipleComponent]
public class GameManager : MonoBehaviour
{
		/// <summary>
		/// The grid cell prefab.
		/// </summary>
		public GameObject gridCellPrefab;
		[Range(0,1)]
	
		/// <summary>
		/// The cell content prefab.
		/// </summary>
		public GameObject cellContentPrefab;
	
		/// <summary>
		/// The cell content z-position.
		/// </summary>
		[Range(-20,20)]
		public float cellContentZPosition = -5;
	
		/// <summary>
		/// The grid rect transform.
		/// </summary>
		public RectTransform gridRectTransform;
	
		/// <summary>
		/// The level text.
		/// </summary>
		public Text levelText;
	
		/// <summary>
		/// The mission text.
		/// </summary>
		public Text missionText;
	
		/// <summary>
		/// The grid cells in the grid.
		/// </summary>
		public static GridCell[] gridCells;
	
		/// <summary>
		/// The water bubble sound effect.
		/// </summary>
		public AudioClip correctSFX;
	
		/// <summary>
		/// The worng sound effect.
		/// </summary>
		public AudioClip worngSFX;
	
		/// <summary>
		/// The completed sound effect.
		/// </summary>
		public AudioClip completedSFX;
	
		/// <summary>
		/// The level locked sound effect.
		/// </summary>
		public AudioClip levelLockedSFX;
	
		/// <summary>
		/// The next button image.
		/// </summary>
		public Image nextButtonImage;
	
		/// <summary>
		/// The back button image.
		/// </summary>
		public Image backButtonImage;
	
		/// <summary>
		/// The previous grid cell.
		/// </summary>
		public static GridCell previousGridCell;
	
		/// <summary>
		/// The current level.
		/// </summary>
		public static Level currentLevel;
	
		/// <summary>
		/// Whether the GameManager is running.
		/// </summary>
		private bool isRunning;
	
		/// <summary>
		/// Wheter to enable click on cells.
		/// </summary>
		public static bool enableClick = true;
	
		/// <summary>
		/// The timer reference.
		/// </summary>
		private Timer timer;
	
		/// <summary>
		/// The effects audio source.
		/// </summary>
		public AudioSource effectsAudioSource;
	
		void Start ()
		{
				///Setting up the references
				if (timer == null) {
						timer = GameObject.Find ("Time").GetComponent<Timer> ();
				}
		
				if (nextButtonImage == null) {
						nextButtonImage = GameObject.Find ("NextButton").GetComponent<Image> ();
				}
		
				if (backButtonImage == null) {
						backButtonImage = GameObject.Find ("BackButton").GetComponent<Image> ();
				}
		
				if (effectsAudioSource == null) {
						effectsAudioSource = GameObject.Find ("AudioSources").GetComponents<AudioSource> () [1];
				}
		
				if (levelText == null) {
						levelText = GameObject.Find ("Level").GetComponent<Text> ();
				}
		
				if (missionText == null) {
						missionText = GameObject.Find ("MissionTitle").GetComponent<Text> ();
				}
		
				if (gridRectTransform == null) {
						gridRectTransform = GameObject.Find ("Grid").GetComponent<RectTransform> ();
				}
		
				enableClick = true;
		
				///Create New level (the selected level)
				CreateNewLevel ();
		}
	
		/// <summary>
		/// When the GameObject becomes invisible.
		/// </summary>
		void OnDisable ()
		{
				///stop the timer
				if (timer != null)
						timer.Stop ();
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (!isRunning) {
						return;
				}
		}
	
		/// <summary>
		/// Refreshs(Reset) the grid.
		/// </summary>
		public void RefreshGrid ()
		{	
				try {
						timer.Stop ();
						ResetGridCells (0, 0, gridCells);
						previousGridCell = null;
						timer.timeInSeconds = currentLevel.timeLimit;
						timer.StartTimer ();
				} catch (Exception ex) {

				}
		}
	
		/// <summary>
		/// Create a new level.
		/// </summary>
		private void CreateNewLevel ()
		{
				try {
						missionText.text = Mission.selectedMission.missionTitle;
						levelText.text = "Level " + TableLevel.selectedLevel.ID;

						///Set escape event scene name
						if (Mission.selectedMission.levelsManagerComponent.singleLevel) {
								GameObject.FindObjectOfType<EscapeEvent> ().sceneName = "Missions";
								///Hide next,back buttons
								backButtonImage.enabled = nextButtonImage.enabled = false;
						} else {
								GameObject.FindObjectOfType<EscapeEvent> ().sceneName = "Levels";
						}

						currentLevel = Mission.selectedMission.levelsManagerComponent.levels [TableLevel.selectedLevel.ID - 1];
						ResetGameContents ();
						BuildTheGrid ();
						SettingUpPairs ();
						SettingUpNextBackAlpha ();
						timer.Stop ();
						timer.SetTime (currentLevel.timeLimit);
						StartCoroutine (RunTimeAfter (Mission.selectedMission.levelsManagerComponent.initialDelay));
						ResetGridCells (Mission.selectedMission.levelsManagerComponent.initialDelay, Mission.selectedMission.levelsManagerComponent.delayBetweenCells, gridCells);
						isRunning = true;
				} catch (Exception ex) {
						Debug.LogWarning ("Make sure you selected a level");
				}
		}
	
		/// <summary>
		/// Build the grid.
		/// </summary>
		private void BuildTheGrid ()
		{
				LevelsManager levelsManagerComponent = Mission.selectedMission.levelsManagerComponent;
		
				Debug.Log ("Building the Grid " + levelsManagerComponent.levels[TableLevel.selectedLevel.ID-1].numberOfRows + "x" + levelsManagerComponent.levels[TableLevel.selectedLevel.ID-1].numberOfColumns);
		
				Vector2 cellSize = levelsManagerComponent.cellSize, spacing = levelsManagerComponent.spacing;
				if (levelsManagerComponent.matchGrid) {
					cellSize = new Vector2 (gridRectTransform.rect.width / levelsManagerComponent.levels[TableLevel.selectedLevel.ID-1].numberOfColumns, gridRectTransform.rect.height / levelsManagerComponent.levels[TableLevel.selectedLevel.ID-1].numberOfRows);
				}
		
				RectTransform rectTransform;
				int gridCellIndex;
				float x, y;//grid cell x,y offset
			
				gridCells = new GridCell[levelsManagerComponent.levels[TableLevel.selectedLevel.ID-1].numberOfRows * levelsManagerComponent.levels[TableLevel.selectedLevel.ID-1].numberOfColumns];
		
				gridRectTransform.name = "Grid " + levelsManagerComponent.levels[TableLevel.selectedLevel.ID-1].numberOfRows + "x" + levelsManagerComponent.levels[TableLevel.selectedLevel.ID-1].numberOfColumns;
		
				for (int i = 0; i < levelsManagerComponent.levels[TableLevel.selectedLevel.ID-1].numberOfRows; i++) {
					for (int j = 0; j < levelsManagerComponent.levels[TableLevel.selectedLevel.ID-1].numberOfColumns; j++) {
				
								///Calculate grid cell index
								gridCellIndex = i * levelsManagerComponent.levels[TableLevel.selectedLevel.ID-1].numberOfColumns + j;
				
								///Create a new grid cell
								GameObject gridCell = Instantiate (gridCellPrefab) as GameObject;

								gridCell.GetComponent<Button>().interactable = false;
								gridCell.transform.Find("Blured").GetComponent<Image> ().enabled = false;
								gridCell.transform.Find ("Content").GetComponent<Image> ().enabled = false;

								///Name the new grid cell
								gridCell.name = "GridCell-" + gridCellIndex;
								///Set the new grid cell parent
								gridCell.transform.SetParent (gridRectTransform);
								///Set the scale of the new grid cell to one
								gridCell.transform.localScale = Vector3.one;
								///Set the local position of the grid cell
								gridCell.transform.localPosition = Vector3.zero;
				
								//Move and size the new grid cell
								rectTransform = gridCell.GetComponent<RectTransform> ();
								x = -gridRectTransform.rect.width / 2 + cellSize.x * j;
								y = gridRectTransform.rect.height / 2 - cellSize.y * (i + 1);
								rectTransform.offsetMin = new Vector2 (x, y) + spacing / 2;
				
								x = rectTransform.offsetMin.x + cellSize.x;
								y = rectTransform.offsetMin.y + cellSize.y;
								rectTransform.offsetMax = new Vector2 (x, y) - spacing;
				
								///Get GridCell component
								GridCell gridCellComponent = gridCell.GetComponent<GridCell> ();
								///Set the cell index
								gridCellComponent.index = gridCellIndex;
				
								gridCells [gridCellIndex] = gridCellComponent;
						}
				}	
		}
	
		/// <summary>
		/// Setting up the Grid Cells Pairs.
		/// </summary>
		private void SettingUpPairs ()
		{
				Debug.Log ("Setting up the Cells Pairs");
		

				if (currentLevel == null) {
						Debug.Log ("level is undefined");
						return;
				}
		

				if (Mission.selectedMission.levelsManagerComponent.randomShuffleOnBuild) {
						//Random Shuffle on Build
						FPShuffle.RandomPairsShuffle (currentLevel.pairs, Mission.selectedMission.levelsManagerComponent.levels[TableLevel.selectedLevel.ID-1].numberOfRows * Mission.selectedMission.levelsManagerComponent.levels[TableLevel.selectedLevel.ID-1].numberOfColumns);
				}

				Level.Pair elementsPair = null;
				int pairID;
				for (int i = 0; i <currentLevel.pairs.Count; i++) {
			
						pairID = i;
						elementsPair = currentLevel.pairs [i];
			
						//Setting up the First Element of the Pair
						gridCells [elementsPair.firstElement.index].GetComponent<Button>().interactable = true;
						gridCells [elementsPair.firstElement.index].transform.Find("Blured").GetComponent<Image> ().enabled = true;
						gridCells [elementsPair.firstElement.index].transform.Find ("Content").GetComponent<Image> ().enabled = true;
						gridCells [elementsPair.firstElement.index].GetComponent<Image> ().sprite = elementsPair.backgroundSprite;
						gridCells [elementsPair.firstElement.index].transform.Find ("Content").GetComponent<Image> ().sprite = elementsPair.onClickSprite;
						gridCells [elementsPair.firstElement.index].pairID = pairID;
			
						//Setting up the Second Element of the Pair
						gridCells [elementsPair.secondElement.index].GetComponent<Button>().interactable = true;
						gridCells [elementsPair.secondElement.index].transform.Find("Blured").GetComponent<Image> ().enabled = true;
						gridCells [elementsPair.secondElement.index].transform.Find ("Content").GetComponent<Image> ().enabled = true;
						gridCells [elementsPair.secondElement.index].GetComponent<Image> ().sprite = elementsPair.backgroundSprite;
						gridCells [elementsPair.secondElement.index].transform.Find ("Content").GetComponent<Image> ().sprite = elementsPair.onClickSprite;
						gridCells [elementsPair.secondElement.index].pairID = pairID;
				}
		}
	
		/// <summary>
		/// Go to the next level.
		/// </summary>
		public void NextLevel ()
		{
				if (LevelsTable.selectedLevelID >= 1 && LevelsTable.selectedLevelID < LevelsTable.tableLevels.Count) {
						///Get the next level and check if it's locked , then do not load the next level
						DataManager.MissionData currentMissionData = DataManager.FindMissionDataById (Mission.selectedMission.ID, DataManager.instance.filterdMissionsData);//Get the current mission
						if (LevelsTable.selectedLevelID + 1 <= currentMissionData.levelsData.Count) {
								DataManager.LevelData nextLevelData = currentMissionData.FindLevelDataById (LevelsTable.selectedLevelID + 1);///Get the next level
								if (nextLevelData.isLocked) {
										///Play lock sound effectd
										if (levelLockedSFX != null && effectsAudioSource != null) {
												UIExtension.PlayOneShotClipAt (levelLockedSFX, Vector3.zero, effectsAudioSource.volume);
										}
										///Skip the next
										return;
								}
						}
						TableLevel.selectedLevel = LevelsTable.tableLevels [LevelsTable.selectedLevelID];///Set the selected level
						CreateNewLevel ();///Create new level
						LevelsTable.selectedLevelID++;///Increase level ID
			
				} else {
						///Play lock sound effectd
						if (levelLockedSFX != null) {
								UIExtension.PlayOneShotClipAt (levelLockedSFX, Vector3.zero, effectsAudioSource.volume);
						}
				}
		}
	
		//// <summary>
		/// Back to the previous level.
		/// </summary>
		public void PreviousLevel ()
		{
				if (LevelsTable.selectedLevelID > 1 && LevelsTable.selectedLevelID <= LevelsTable.tableLevels.Count) {
						LevelsTable.selectedLevelID--;
						TableLevel.selectedLevel = LevelsTable.tableLevels [LevelsTable.selectedLevelID - 1];
						CreateNewLevel ();
				} else {
						PlayLevelLockedSFX ();
				}
		}
	
		/// <summary>
		/// Setting up alpha value for the next and back buttons.
		/// </summary>
		private void SettingUpNextBackAlpha ()
		{
				Color tempColor;
				if (TableLevel.selectedLevel.ID == 1) {
						tempColor = backButtonImage.color;
						tempColor.a = 0.5f;
						backButtonImage.color = tempColor;
						backButtonImage.GetComponent<Button> ().interactable = false;
			
						tempColor = nextButtonImage.color;
						tempColor.a = 1;
						nextButtonImage.color = tempColor;
						nextButtonImage.GetComponent<Button> ().interactable = true;
				} else if (TableLevel.selectedLevel.ID == LevelsTable.tableLevels.Count) {
						tempColor = backButtonImage.color;
						tempColor.a = 1;
						backButtonImage.color = tempColor;
						backButtonImage.GetComponent<Button> ().interactable = true;
			
						tempColor = nextButtonImage.color;
						tempColor.a = 0.5f;
						nextButtonImage.color = tempColor;
						nextButtonImage.GetComponent<Button> ().interactable = false;
				} else {
						tempColor = backButtonImage.color;
						tempColor.a = 1;
						backButtonImage.color = tempColor;
						backButtonImage.GetComponent<Button> ().interactable = true;
			
						tempColor = nextButtonImage.color;
						tempColor.a = 1;
						nextButtonImage.color = tempColor;
						nextButtonImage.GetComponent<Button> ().interactable = true;
				}
		}
	
		/// <summary>
		/// Resets the game contents.
		/// </summary>
		private void ResetGameContents ()
		{
				GameObject [] gridCells = GameObject.FindGameObjectsWithTag ("GridCell");
				foreach (GameObject gridCell in gridCells) {
						Destroy (gridCell);
				}
		}
	
		/// <summary>
		/// Checks Wheter the level is completed.
		/// </summary>
		public bool CheckLevelComplete ()
		{
				bool isLevelComplete = true;
		
				foreach (GridCell gridCell in gridCells) {
						if(!gridCell.GetComponent<Button>().interactable){
								continue;
						}
						if (!gridCell.alreadyUsed) {
								isLevelComplete = false;
								break;
						}
				}
		
				if (isLevelComplete) {
						timer.Stop ();
						isRunning = false;
			
						try {
								///Save the stars level
								DataManager.MissionData currentMissionData = DataManager.FindMissionDataById (Mission.selectedMission.ID, DataManager.instance.filterdMissionsData);
								DataManager.LevelData currentLevelData = currentMissionData.FindLevelDataById (TableLevel.selectedLevel.ID);

								//Calculate the stars rating
								if (timer.GetTimeInSeconds () >= currentLevel.threeStarsTimePeriod && timer.GetTimeInSeconds () <= currentLevel.timeLimit) {
										currentLevelData.starsNumber = TableLevel.StarsNumber.THREE;
								} else if (timer.GetTimeInSeconds () >= currentLevel.twoStarsTimePeriod && timer.GetTimeInSeconds () < currentLevel.threeStarsTimePeriod) {
										currentLevelData.starsNumber = TableLevel.StarsNumber.TWO;
								} else {
										currentLevelData.starsNumber = TableLevel.StarsNumber.ONE;
								}

								if (currentLevelData .ID + 1 <= currentMissionData.levelsData.Count) {
										///Unlock the next level
										DataManager.LevelData nextLevelData = currentMissionData.FindLevelDataById (TableLevel.selectedLevel.ID + 1);
										nextLevelData.isLocked = false;
								}
								DataManager.instance.SaveMissionsToFile (DataManager.instance.filterdMissionsData);

								///Show the black area
								BlackArea.Show ();
								///Show the win dialog
								GameObject.FindObjectOfType<WinDialog> ().Show (currentLevelData.starsNumber);
								Debug.Log ("You completed level " + TableLevel.selectedLevel.ID);
						} catch (Exception ex) {
								Debug.Log (ex.Message);
						}
			

				}
				return isLevelComplete;
		}
	
		public void ResetGridCells (float initialDelay, float delayBetweenCells, GridCell[] gcs)
		{
				if (gcs != null) {
						StartCoroutine (ResetCellsCoroutine (initialDelay, delayBetweenCells, gcs));
				}
		}
	
		private IEnumerator ResetCellsCoroutine (float initialDelay, float delayBetweenCells, GridCell[] gcs)
		{
				enableClick = false;
				yield return new WaitForSeconds (initialDelay);
				if (gcs != null) {
						foreach (GridCell gc in gcs) {
								if (gc != null) {
										gc.transform.Find ("Content").GetComponent<Image> ().sprite = currentLevel.pairs [gc.pairID].normalSprite;
										gc.transform.Find ("Blured").GetComponent<Animator> ().SetBool ("isRunning", false);
										gc.alreadyUsed = false;
								}
								yield return new WaitForSeconds (delayBetweenCells);
						}
				}
				enableClick = true;
		}

		private IEnumerator RunTimeAfter (float delayTime)
		{
				yield return new WaitForSeconds (delayTime);
				if (timer != null) {
						timer.StartTimer ();
				}
		}

		public void OnTimeOut ()
		{
				Debug.Log ("Time is out");
				///Show the black area
				BlackArea.Show ();
				///Show the time out dialog
				GameObject.FindObjectOfType<TimeOutDialog> ().Show ();
		}

		public void PlayWrongSFX ()
		{
				if (worngSFX != null && effectsAudioSource != null) {
						UIExtension.PlayOneShotClipAt (worngSFX, Vector3.zero, effectsAudioSource.volume);
				}
		}
	
		public void PlayCorrectSFX ()
		{
				if (correctSFX != null && effectsAudioSource != null) {
						UIExtension.PlayOneShotClipAt (correctSFX, Vector3.zero, effectsAudioSource.volume);
				}
		}
	
		public void PlayCompletedSFX ()
		{
				if (completedSFX != null && effectsAudioSource != null) {
						UIExtension.PlayOneShotClipAt (completedSFX, Vector3.zero, effectsAudioSource.volume);
				}
		}
	
		public void PlayLevelLockedSFX ()
		{
				if (levelLockedSFX != null && effectsAudioSource != null) {
						UIExtension.PlayOneShotClipAt (levelLockedSFX, Vector3.zero, effectsAudioSource.volume);
				}
		}
}