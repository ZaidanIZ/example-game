using System.Collections;
using System.Collections.Generic;

///Developed by Indie Studio
///https://www.assetstore.unity3d.com/en/#!/publisher/9268
///www.indiestd.com
///info@indiestd.com
///copyright © 2016 IGS. All rights reserved.
#if !(UNITY_WP8 || UNITY_WP8_1)
using System.Runtime.Serialization.Formatters.Binary;
#endif
using System.Xml.Serialization;
using System.IO;
using System;
using UnityEngine;

[DisallowMultipleComponent]
/// <summary>
/// Data manager.
/// (Manages saving(writing)/loading(reading) the data of the game)
/// </summary>
public class DataManager : MonoBehaviour
{
	/// <summary>
	/// The name of the file without the extension.
	/// </summary>
	public string fileName = "findthepairs";	

	/// <summary>
	/// The serilization method for reading and writing.
	/// </summary>
	public SerilizationMethod serilizationMethod;
	
	/// <summary>
	/// The scene missions data.
	/// (will be loaded from the Missions scene)
	/// </summary>
	private List<MissionData> sceneMissionsData;
	
	/// <summary>
	/// The file missions data.
	/// (will be loaded from the file)
	/// </summary>
	private List<MissionData> fileMissionsData;
	
	/// <summary>
	/// The filterd missions data.
	/// (The final missions data after filtering)
	/// </summary>
	public List<MissionData> filterdMissionsData;
	
	/// <summary>
	/// The path of the file.
	/// </summary>
	private string filePath;
	
	/// <summary>
	/// Whether the Missions data is empty or null.
	/// </summary>
	private bool isNullOrEmpty;
	
	/// <summary>
	/// Whether it's need to save new data.
	/// </summary>
	private bool needsToSaveNewData;
	
	/// <summary>
	/// The public DataManager instance.
	/// </summary>
	public static DataManager instance;
	
	void Awake ()
	{
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy(gameObject);
			return;
		}
		
		#if UNITY_IPHONE
		//Enable the MONO_REFLECTION_SERIALIZER(For IOS Platform Only)
		System.Environment.SetEnvironmentVariable ("MONO_REFLECTION_SERIALIZER", "yes");
		#endif
		
	}
	
	/// <summary>
	/// Inits the game data.
	/// </summary>
	public void InitGameData ()
	{
		///Reset flags
		isNullOrEmpty = false;
		needsToSaveNewData = false;
		
		///Load Missions data from the Missions Scene
		sceneMissionsData = LoadMissionsDataFromScene ();
		if (sceneMissionsData == null) {
			return;
		}
		
		if (sceneMissionsData.Count == 0) {
			return;
		}
		
		///Load Missions data from the file
		fileMissionsData = LoadMissionsFromFile ();
		
		if (fileMissionsData == null) {
			isNullOrEmpty = true;
		} else {
			if (fileMissionsData.Count == 0) {
				isNullOrEmpty = true;
			}
		}
		
		///If the Missions data in the file is null or empty,then save the scene Missions data to the file
		if (isNullOrEmpty) {
			SaveMissionsToFile (sceneMissionsData);
			filterdMissionsData = sceneMissionsData;
		} else {
			///Otherwise get the filtered Missions Data
			filterdMissionsData = GetFilterdMissionsData ();
			
			///If it's need to save a new Missions data to the file ,then save it
			if (needsToSaveNewData) {
				SaveMissionsToFile (filterdMissionsData);
			}
		}
	}
	
	///MissionData is class used for persistent loading and saving
	[System.Serializable]
	public class MissionData
	{
		public int ID;//the ID of the Mission
		public bool isLocked = true;
		public List<LevelData> levelsData = new List<LevelData> ();//the levels of the mission
		
		/// <summary>
		/// Find the level data by ID.
		/// </summary>
		/// <returns>The level data.</returns>
		/// <param name="ID">the ID of the level.</param>
		public LevelData FindLevelDataById (int ID)
		{
			foreach (LevelData levelData in levelsData) {
				if (levelData.ID == ID) {
					return levelData;
				}
			}
			return null;
		}
	}
	
	///LevelData is class used for persistent loading and saving
	[System.Serializable]
	public class LevelData
	{
		public int ID;//The id of the level
		public bool isLocked = true;
		public TableLevel.StarsNumber starsNumber = TableLevel.StarsNumber.ZERO;//number of stars (stars rating)
	}
	
	/// <summary>
	/// Reset the game data.
	/// </summary>
	public void ResetGameData ()
	{
		try {
			fileMissionsData = LoadMissionsFromFile ();
			
			if (fileMissionsData == null) {
				return;
			}
			
			///Reset the levels of each mission
			foreach (MissionData missionData in fileMissionsData) {
				if (missionData == null) {
					continue;
				}

				if(missionData.ID == 1){
					missionData.isLocked = false;
				}else{
					missionData.isLocked = true;
				}

				foreach (LevelData levelData in missionData.levelsData) {
					if (levelData == null) {
						continue;
					}
					
					///UnLock the level of ID equals 1(first level) ,otherwise lock the others
					if (levelData.ID == 1) {
						levelData.isLocked = false;
					} else {
						levelData.isLocked = true;
					}
					
					///Set stars number to zero for each level
					levelData.starsNumber = TableLevel.StarsNumber.ZERO;
				}
			}
			
			SaveMissionsToFile (fileMissionsData);
		} catch (Exception ex) {
			Debug.Log (ex.Message);
			return;
		}
		Debug.Log ("Game Data has been reset successfully");
	}
	
	/// <summary>
	/// Load the missions data from the scene.
	/// </summary>
	/// <returns>The missions data from the scene.</returns>
	private List<MissionData> LoadMissionsDataFromScene ()
	{
		Debug.Log ("Loading Missions Data from Scene");
		
		GameObject [] missions = UIExtension.FindGameObjectsWithTag ("Mission");;
		
		if (missions == null) {
			Debug.Log ("No Mission with 'Mission' tag found");
			return null;
		}
		
		Mission tempMission = null;
		LevelsManager tempLevelManager = null;
		
		List<MissionData> tempMissionsData = new List<MissionData> ();
		MissionData tempMissionData = null;
		for (int i = 0 ; i < missions.Length ;i++) {

			tempMission = missions[i].GetComponent<Mission> ();
			tempLevelManager =  missions[i].GetComponent<LevelsManager> ();
			tempMissionData = new MissionData ();
			if(i == 0){
				tempMissionData.isLocked = false;
			}
			tempMissionData.ID = tempMission.ID;
			tempMissionData.levelsData = GetLevelData (tempLevelManager.levels);
			
			tempMissionsData.Add (tempMissionData);
		}
		
		return tempMissionsData;
	}
	
	
	/// <summary>
	/// Get the level data.
	/// </summary>
	/// <returns>The new levels data.</returns>
	/// <param name="levels">The current levels data.</param>
	private List<LevelData> GetLevelData (List<Level> levels)
	{
		if (levels == null) {
			return null;
		}
		
		LevelData tempLevelData = null;
		List<LevelData> tempLevelsData = new List<LevelData> ();
		int ID = 1;
		for (int i = 0; i <levels.Count; i++) {
			tempLevelData = new LevelData ();
			tempLevelData.ID = ID;
			ID++;
			if (i == 0) {
				///First level must be unlocked
				tempLevelData.isLocked = false;
			}
			tempLevelsData.Add (tempLevelData);
		}
		
		return tempLevelsData;
	}
	
	/// <summary>
	/// Get the filterd missions data.
	/// (Compare the Missions data in the file with the Missions data in the scene)
	/// Scenario :
	/// -you may have a set of missions saved into file
	/// -if you add/delete a mission using inspector
	/// -then it's need to determine and save the new data
	/// </summary>
	/// <returns>The filterd missions data.</returns>
	private List<MissionData> GetFilterdMissionsData ()
	{
		if (fileMissionsData == null || sceneMissionsData == null) {
			return null;
		}
		
		MissionData tempMissionData = null;
		List<MissionData> tempFilteredMissionsData = new List<MissionData> ();
		
		foreach (MissionData missionData in sceneMissionsData) {
			
			///Get the mission data from the file
			tempMissionData = FindMissionDataById (missionData.ID, fileMissionsData);
			if (tempMissionData != null) {
				///If the number of levels in the scene mission Equals to the number of levels in the file mission
				if (missionData.levelsData.Count == tempMissionData.levelsData.Count) {
					///Add tempMissionData(file mission data) to the filtered list
					tempFilteredMissionsData.Add (tempMissionData);
				} else {//Otherwise,its need to save new data
					//TODO:levels data will be lost,when a level is added or removed
					needsToSaveNewData = true;
					///Add the  missionData(scene mission data) to the filtered list 
					tempFilteredMissionsData.Add (missionData);
				}
			} else {//Otherwise,its need to save new data
				needsToSaveNewData = true;
				///Add the missionData(scene mission data) to the filtered list 
				tempFilteredMissionsData.Add (missionData);
			}
		}
		return tempFilteredMissionsData;
	}
	
	/// <summary>
	/// Save the missions to file.
	/// </summary>
	/// <param name="missionsData">Missions data.</param>
	public void SaveMissionsToFile (List<MissionData> missionsData)
	{
		#if UNITY_ANDROID || UNITY_IPHONE
		if (serilizationMethod == SerilizationMethod.BINARY) {
			SaveDataToBinaryFile (missionsData);
		} else if (serilizationMethod == SerilizationMethod.XML) {
			SaveDataToXMLFile (missionsData);
		}
		#elif UNITY_WP8
		if (serilizationMethod == SerilizationMethod.XML) {
			SaveDataToXMLFile (missionsData);
		}
		#else
		if (serilizationMethod == SerilizationMethod.BINARY) {
			SaveDataToBinaryFile (missionsData);
		} else if (serilizationMethod == SerilizationMethod.XML) {
			SaveDataToXMLFile (missionsData);
		}
		#endif
	}
	
	/// <summary>
	/// Load the missions from file.
	/// </summary>
	/// <returns>The missions from file.</returns>
	public List<MissionData> LoadMissionsFromFile ()
	{
		#if UNITY_ANDROID || UNITY_IPHONE
		if (serilizationMethod == SerilizationMethod.BINARY) {
			return	LoadDataFromBinaryFile<List<MissionData>> ();
		} else if (serilizationMethod == SerilizationMethod.XML) {
			return	LoadDataFromXMLFile<List<MissionData>> ();
		}
		#elif UNITY_WP8 || UNITY_WP8_1
		if (serilizationMethod == SerilizationMethod.XML) {
			return	LoadDataFromXMLFile<List<MissionData>> ();
		}
		#else
		if (serilizationMethod == SerilizationMethod.BINARY) {
			return	LoadDataFromBinaryFile<List<MissionData>> ();
		} else if (serilizationMethod == SerilizationMethod.XML) {
			return	LoadDataFromXMLFile<List<MissionData>> ();
		}
		#endif
		
		return null;
	}
	
	/// <summary>
	/// Save the data to XML file.
	/// </summary>
	/// <param name="data">Data.</param>
	public void SaveDataToXMLFile<Type> (Type data)
	{
		///Setting up file path
		SettingUpFilePath ();
		if (string.IsNullOrEmpty (filePath)) {
			Debug.Log ("Null or Empty path");
			return;
		}
		
		if (data == null) {
			Debug.Log ("Data is Null");
			return;
		}
		
		Debug.Log ("Saving Data to XML File");
		
		XmlSerializer serializer = new XmlSerializer (typeof(Type));
		TextWriter textWriter = new StreamWriter (filePath);
		serializer.Serialize (textWriter, data);
		textWriter.Close ();
	}
	
	/// <summary>
	/// Load the data from XML file.
	/// </summary>
	/// <returns>The data from XML file.</returns>
	public Type LoadDataFromXMLFile<Type> ()
	{
		Type data = default(Type);
		
		///Setting up file path
		SettingUpFilePath ();
		
		if (string.IsNullOrEmpty (filePath)) {
			Debug.Log ("Null or Empty file path");
			return data;
		}
		
		if (!File.Exists (filePath)) {
			Debug.Log (filePath + " is not exists");
			return data;
		}
		
		Debug.Log ("Loading Data from XML File");
		
		XmlSerializer deserializer = new XmlSerializer (typeof(Type));
		TextReader textReader = new StreamReader (filePath);
		data = (Type)deserializer.Deserialize (textReader);
		textReader.Close ();
		
		return data;
	}
	
	/// <summary>
	/// Save data to the binary file.
	/// </summary>
	/// <param name="data"> The data.</param>
	public void SaveDataToBinaryFile<Type> (Type data)
	{
		#if !(UNITY_WP8 || UNITY_WP8_1)
		///Setting up file path
		SettingUpFilePath ();
		if (string.IsNullOrEmpty (filePath)) {
			Debug.Log ("Null or Empty file path");
			return;
		}
		
		if (data == null) {
			Debug.Log ("Data is Null");
			return;
		}
		
		Debug.Log ("Saving Data to Binary File");
		
		FileStream file = null;
		try {
			BinaryFormatter bf = new BinaryFormatter ();
			file = File.Open (filePath, FileMode.Create);
			bf.Serialize (file, data);
			file.Close ();
		} catch (Exception ex) {
			file.Close ();
			Debug.LogError ("Exception : " + ex.Message);
		}
		#endif
	}
	
	/// <summary>
	/// Load data from the binary file.
	/// </summary>
	public Type LoadDataFromBinaryFile<Type> ()
	{
		Type data = default(Type);
		
		#if !(UNITY_WP8 || UNITY_WP8_1)
		///Setting up file path
		SettingUpFilePath ();
		if (string.IsNullOrEmpty (filePath)) {
			Debug.Log ("Null or Empty file path");
			return data;
		}
		
		if (!File.Exists (filePath)) {
			Debug.Log (filePath + " is not exists");
			return data;
		}
		
		Debug.Log ("Loading Data from Binary File");
		
		
		FileStream file = null;
		try {
			BinaryFormatter bf = new BinaryFormatter ();
			file = File.Open (filePath, FileMode.Open);
			data = (Type)bf.Deserialize (file);
			file.Close ();
		} catch (Exception ex) {
			file.Close ();
			Debug.LogError ("Exception : " + ex.Message);
		}
		#endif
		return data;
	}
	
	/// <summary>
	/// Finds the mission data by id.
	/// </summary>
	/// <returns>The mission data by ID.</returns>
	/// <param name="ID">the ID of the mission.</param>
	/// <param name="missionsData">Missions data list to search in.</param>
	public static MissionData FindMissionDataById (int ID, List<MissionData> missionsData)
	{
		if (missionsData == null) {
			return null;
		}
		
		foreach (MissionData missionData in missionsData) {
			if (missionData.ID == ID) {
				return missionData;
			}
			
		}
		
		return null;
	}
	
	/// <summary>
	/// Settings up the path of the file ,relative to the current platform.
	/// </summary>
	private void SettingUpFilePath ()
	{
		string fileExtension = "";
		
		#if UNITY_ANDROID
		//Get Android Path
		filePath = GetAndroidFileFolder();
		if (serilizationMethod == SerilizationMethod.BINARY) {
			fileExtension = ".bin";
		} else if (serilizationMethod == SerilizationMethod.XML) {
			fileExtension = ".xml";
		}
		#elif UNITY_IPHONE
		//Get iPhone Documents Path
		filePath = GetIPhoneFileFolder();
		if (serilizationMethod == SerilizationMethod.BINARY) {
			fileExtension = ".bin";
		} else if (serilizationMethod == SerilizationMethod.XML) {
			fileExtension = ".xml";
		}
		#elif UNITY_WP8 || UNITY_WP8_1
		//Get Windows Phone 8 Path
		filePath = GetWP8FileFolder();
		if (serilizationMethod == SerilizationMethod.XML) {
			fileExtension = ".xml";
		}
		#else
		//Others
		filePath = GetOthersFileFolder ();
		if (serilizationMethod == SerilizationMethod.BINARY) {
			fileExtension = ".bin";
		} else if (serilizationMethod == SerilizationMethod.XML) {
			fileExtension = ".xml";
		}
		#endif
		filePath += "/" + fileName + fileExtension;
	}
	
	public static string GetAndroidFileFolder ()
	{
		return Application.persistentDataPath;
	}
	
	public static string GetIPhoneFileFolder ()
	{
		return Application.persistentDataPath;
	}
	
	public static string GetWP8FileFolder ()
	{
		return Application.dataPath;
	}
	
	public static string GetOthersFileFolder ()
	{
		return Application.dataPath;
	}
	
	public enum SerilizationMethod
	{
		#if UNITY_WP8 || UNITY_WP8_1
		XML
		#else
		BINARY,
		XML
		#endif
	}
	;	
}