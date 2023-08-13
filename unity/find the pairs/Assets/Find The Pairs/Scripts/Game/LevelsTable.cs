using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.

///Developed by Indie Studio
///https://www.assetstore.unity3d.com/en/#!/publisher/9268
///www.indiestd.com
///info@indiestd.com
///copyright © 2016 IGS. All rights reserved.
[DisallowMultipleComponent]
public class LevelsTable : MonoBehaviour
{
		/// <summary>
		/// The table levels.
		/// </summary>
		public static List<TableLevel> tableLevels;

		/// <summary>
		/// The selected level ID.
		/// </summary>
		public static int selectedLevelID;

		/// <summary>
		/// The levels parent.
		/// </summary>
		public Transform levelsParent;

		/// <summary>
		/// The star on sprite.
		/// </summary>
		public Sprite starOn;

		/// <summary>
		/// The star off sprite.
		/// </summary>
		public Sprite starOff;

		/// <summary>
		/// The level prefab.
		/// </summary>
		public GameObject levelPrefab;

		/// <summary>
		/// temporary transform.
		/// </summary>
		private Transform tempTransform;

		/// <summary>
		/// temporary mission data.
		/// </summary>
		private DataManager.MissionData tempMissionData;

		/// <summary>
		/// temporary level data.
		/// </summary>
		private DataManager.LevelData tempLevelData;

		void Awake ()
		{
				///define table levels
				tableLevels = new List<TableLevel> ();

				try {
						///Create new levels
						CreateLevels ();

						if (Mission.selectedMission.levelsManagerComponent.singleLevel) {
								if (tableLevels.Count != 0) {
										TableLevel.selectedLevel = tableLevels [0];		
										GameObject.FindObjectOfType<UIEvents>().LoadGameScene();
										return;
								}
						}

						///Setting up the top title
						if (string.IsNullOrEmpty (Mission.selectedMission.missionTitle)) {
								GameObject.Find ("TopTitle").GetComponent<Text> ().text = "Mission Title";
						} else {
								GameObject.Find ("TopTitle").GetComponent<Text> ().text = Mission.selectedMission.missionTitle;
						}
				} catch (Exception ex) {
						Debug.Log ("Make sure you selected a mission");
				}
		}



		/// <summary>
		/// Creates the levels.
		/// </summary>
		private void CreateLevels ()
		{
				///Clear current tableLevels list
				tableLevels.Clear ();
				///Get LevelsManager Component from the wanted (selected) Mission
				LevelsManager levelsManagerComponent = Mission.selectedMission.levelsManagerComponent;

				TableLevel tableLevelComponent = null;
				GameObject tableLevelGameObject = null;
		 
				///The ID of the level
				int ID = 0;

				///The count of levels
				int levelsCount = 1;
				
				if (!Mission.selectedMission.levelsManagerComponent.singleLevel) {
					levelsCount = levelsManagerComponent.levels.Count;
				}
				///Create Levels
				for (int i = 0; i < levelsCount ; i++) {

						ID = (i + 1);//the id of the level

						tableLevelGameObject = Instantiate (levelPrefab, Vector3.zero, Quaternion.identity) as GameObject;
						tableLevelGameObject.transform.SetParent (levelsParent);//setting up level parent
						tableLevelComponent = tableLevelGameObject.GetComponent<TableLevel> ();//get TableLevel Component
						tableLevelComponent.ID = ID;//setting up level ID
						tableLevelGameObject.name = "Level-" + ID;//level name
						tableLevelGameObject.transform.localScale = Vector3.one;

						SettingUpLevel (tableLevelComponent, ID);//setting up the level contents (stars number ,islocked,...)
						tableLevels.Add (tableLevelComponent);//add table level component to the list
				}

				if (levelsManagerComponent.levels.Count == 0) {
						Debug.Log ("There are no Levels in this Mission");
				} else {
						Debug.Log ("New levels have beeen created");
				}
		}

		/// <summary>
		/// Settings up the level contents in the table.
		/// </summary>
		/// <param name="tableLevel">Table level.</param>
		/// <param name="ID">ID of the level.</param>
		private void SettingUpLevel (TableLevel tableLevel, int ID)
		{
				if (tableLevel == null) {
						return;
				}

				///Get Mission Data of the current Mission
				tempMissionData = DataManager.FindMissionDataById (Mission.selectedMission.ID, DataManager.instance.filterdMissionsData);
				if (tempMissionData == null) {
						Debug.Log ("Null MissionData");
						return;
				}

				///Get Level Data of the wanted (selected) Level
				tempLevelData = tempMissionData.FindLevelDataById (tableLevel.ID);
				if (tempLevelData == null) {
						Debug.Log ("Null LevelData");
						return;
				}

				//If the level is locked then , skip the next
				if (tempLevelData.isLocked) {
						return;
				}
             
				///Make the button interactable
				tableLevel.GetComponent<Button> ().interactable = true;

				///Show the stars of the level
				tableLevel.transform.Find ("Stars").gameObject.SetActive (true);

				///Hide the lock
				tableLevel.transform.Find ("Lock").gameObject.SetActive (false);

				///Show the title of the level
				tableLevel.transform.Find ("LevelTitle").gameObject.SetActive (true);

				///Setting up the level title
				tableLevel.transform.Find ("LevelTitle").GetComponent<Text> ().text = ID.ToString ();

				///Get stars Number from current Level Data
				tableLevel.starsNumber = tempLevelData.starsNumber;
				tempTransform = tableLevel.transform.Find ("Stars");

				///Apply the current Stars Rating 
				if (tempLevelData.starsNumber == TableLevel.StarsNumber.ONE) {//One Star
						tempTransform.Find ("FirstStar").GetComponent<Image> ().sprite = starOn;
						tempTransform.Find ("SecondStar").GetComponent<Image> ().sprite = starOff;
						tempTransform.Find ("ThirdStar").GetComponent<Image> ().sprite = starOff;
				} else if (tempLevelData.starsNumber == TableLevel.StarsNumber.TWO) {//Two Stars
						tempTransform.Find ("FirstStar").GetComponent<Image> ().sprite = starOn;
						tempTransform.Find ("SecondStar").GetComponent<Image> ().sprite = starOn;
						tempTransform.Find ("ThirdStar").GetComponent<Image> ().sprite = starOff;
				} else if (tempLevelData.starsNumber == TableLevel.StarsNumber.THREE) {//Three Stars
						tempTransform.Find ("FirstStar").GetComponent<Image> ().sprite = starOn;
						tempTransform.Find ("SecondStar").GetComponent<Image> ().sprite = starOn;
						tempTransform.Find ("ThirdStar").GetComponent<Image> ().sprite = starOn;
				} else {//Zero Stars
						tempTransform.Find ("FirstStar").GetComponent<Image> ().sprite = starOff;
						tempTransform.Find ("SecondStar").GetComponent<Image> ().sprite = starOff;
						tempTransform.Find ("ThirdStar").GetComponent<Image> ().sprite = starOff;
				}
		}
}