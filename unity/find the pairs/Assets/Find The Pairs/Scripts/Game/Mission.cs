using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

///Developed by Indie Studio
///https://www.assetstore.unity3d.com/en/#!/publisher/9268
///www.indiestd.com
///info@indiestd.com
///copyright © 2016 IGS. All rights reserved.
#pragma warning disable 0168 // variable declared but not used.

[DisallowMultipleComponent]
[RequireComponent(typeof(LevelsManager))]
[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]

/// <summary>
/// A Mission Component used with the LevelsManager Component.
/// To create an new mission you need to add this Component.
/// </summary>
[ExecuteInEditMode]
public class Mission : MonoBehaviour
{
		/// <summary>
		/// Mission ID.
		/// </summary>
		public int ID = -1;

		/// <summary>
		/// The selected Mission.
		/// </summary>
		public static Mission selectedMission;

		/// <summary>
		/// The title of the mission .
		/// </summary>
		public string missionTitle = "New Mission";

		/// <summary>
		/// The LevelsManager Component.
		/// </summary>
		[HideInInspector]
		public LevelsManager levelsManagerComponent;
			
		void Awake ()
		{
				InitMission ();
		}

		void Start ()
		{
				if (Application.isPlaying) {
						SetStarsScore ();
				}
		}

		void Update ()
		{
				#if UNITY_EDITOR
				if (!Application.isPlaying) {
						InitMission ();
				}
				#endif
		}
		
		/// <summary>
		/// Inits the mission.
		/// </summary>
		private void InitMission ()
		{
				///Setting up the ID of the Mission
				if (Application.isPlaying) {

						//Add onClick listener
						GetComponent<Button> ().onClick.AddListener (() => GameObject.FindObjectOfType<UIEvents> ().MissionButtonEvent (this));

						bool validName = int.TryParse (name.Split ('-') [1], out ID);
						if (!validName) {
								Debug.LogError ("Invalid Mission Name");
						}
				}

				//Setting up the LevelsManager Component
				levelsManagerComponent = GetComponent<LevelsManager> ();
		
				if (levelsManagerComponent != null) {
						if (string.IsNullOrEmpty (missionTitle) || missionTitle == "New Mission") {
								//Define the Title of the Mission
								missionTitle = "Pack " + ID;
						}
				}
		
				if (Application.isPlaying) {
						Debug.Log ("Setting up Mission <b>" + missionTitle + "</b> of ID " + ID);
				}

				Transform missionTitleTransform = transform.Find ("Title");
		
				///Setting up the Title of the Mission
				if (missionTitleTransform != null) {
						Text uiText = missionTitleTransform.GetComponent<Text> ();
						if (uiText != null)
								uiText.text = missionTitle;
				}
		}

		/// <summary>
		/// Set the stars score of the mission.
		/// </summary>
		public void SetStarsScore ()
		{
				Transform score = transform.Find ("Score");	
				///Set the mission score
				if (score != null) {
						Transform starsCount = score.Find ("StarsCount");
						if (starsCount != null) {
								starsCount.GetComponent<Text> ().text = GetStarsCount ();
						}
				}
		}
	
		/// <summary>
		/// Get the score count.
		/// </summary>
		/// <returns>The score count.</returns>
		public string GetStarsCount ()
		{
				string result = "";
				try {
						int totalStarsCount = 3;
						int currentStarsCount = 0;

						DataManager.MissionData missionData = DataManager.FindMissionDataById (ID, DataManager.instance.filterdMissionsData);

						if (!levelsManagerComponent.singleLevel) {
								totalStarsCount = levelsManagerComponent.levels.Count * 3;
								foreach (DataManager.LevelData lvl in missionData.levelsData) {
										currentStarsCount += StarsEnumToInt (lvl.starsNumber);
								}
						} else {
								if (missionData.levelsData.Count != 0)
										currentStarsCount += StarsEnumToInt (missionData.levelsData [0].starsNumber);
						}
						
						result = currentStarsCount + "/" + totalStarsCount;
				} catch (Exception ex) {
						//Exception
				}
				return result;
		}

		/// <summary>
		/// Convert StarsNumber enum to Int value.
		/// </summary>
		/// <returns>The StarsNumber enum.</returns>
		/// <param name="starsNumber">Stars number as integer value.</param>
		private int StarsEnumToInt (TableLevel.StarsNumber starsNumber)
		{
				if (starsNumber == TableLevel.StarsNumber.ONE) {
						return 1;
				} else if (starsNumber == TableLevel.StarsNumber.TWO) {
						return 2;
				} else if (starsNumber == TableLevel.StarsNumber.THREE) {
						return 3;
				}
				return 0;
		}
}