/// <summary>
///
///----------- ESPAÑOL -----------
/// 
/// Este script sirve para refrescar los textos de la UI cuando se cambia el lenguage.
/// Con la funcion Awake hace que se ejecute la funcion Refresh_Language para que se cargue el lenguage guardado.
/// 
///----------- ENGLISH -----------
/// 
/// This script seves to refresh the UI texts when the user change the language.
/// The "Awake" function call the "Refresh_Language" function to load the saved language by the user.
/// 
/// </summary>


using UnityEngine;
using System.Collections;

public class Texts_Changer : MonoBehaviour 
{

	static Word_Database wordsDB;

	void Awake () 
	{

		wordsDB = GameObject.Find ("Words_Database").GetComponent<Word_Database>();
		Refresh_Language ();

	}

	public static void Refresh_Language () 
	{

		//BUTTON PLAY
		GameObject.Find ("MENU").transform.Find ("buttonPlay").transform.Find ("PlayText").GetComponent<TextMesh>().text = wordsDB.uiTextsLang[PlayerPrefs.GetInt("numberLanguae"), 2];
		GameObject.Find ("MENU").transform.Find ("buttonPlay").transform.Find ("PlayTextShadow").GetComponent<TextMesh>().text = wordsDB.uiTextsLang[PlayerPrefs.GetInt("numberLanguae"), 2];


		//LEVEL INDICATOR OF MENU
		if (PlayerPrefs.HasKey ("levelWord"))
		{
			
			GameObject.Find("MENU").transform.Find("CircleLevel").transform.Find("LevelText").GetComponent<TextMesh>().text = wordsDB.uiTextsLang[PlayerPrefs.GetInt("numberLanguae"), 0];
			GameObject.Find("MENU").transform.Find("CircleLevel").transform.Find("LevelNum").GetComponent<TextMesh>().text = (PlayerPrefs.GetInt ("levelWord")+1).ToString();

		}
		else 
		{
			
			PlayerPrefs.SetInt ("levelWord", 0);
			GameObject.Find("MENU").transform.Find("CircleLevel").transform.Find("LevelText").GetComponent<TextMesh>().text = wordsDB.uiTextsLang[PlayerPrefs.GetInt("numberLanguae"), 0];
			GameObject.Find("MENU").transform.Find("CircleLevel").transform.Find("LevelNum").GetComponent<TextMesh>().text = (PlayerPrefs.GetInt ("levelWord")+1).ToString();

		}

		// x/x COMPLETED
		GameObject.Find("MENU").transform.Find("BoxQuizCompleted").transform.Find("QuizCompleted").GetComponent<TextMesh>().text = (PlayerPrefs.GetInt ("levelWord")+1) +"/" + GameObject.Find("Words_Database").GetComponent<Word_Database>().words_List.Length + " " + wordsDB.uiTextsLang[PlayerPrefs.GetInt("numberLanguae"), 1];

		//LEVEL INDICATOR IN GAME
		GameObject.Find("LevelIndicator").transform.Find("Level").GetComponent<TextMesh>().text = wordsDB.uiTextsLang[PlayerPrefs.GetInt("numberLanguae"), 0] + " " + (PlayerPrefs.GetInt ("levelWord")+1);


		//CHANGE THE TEXTS OF THE OK AND NO BUTTONS
		GameObject.Find("Game_Controller").GetComponent<Game_Controller>().AreYouSureWindow.transform.Find ("TextOk").GetComponent<TextMesh>().text = wordsDB.uiTextsLang[PlayerPrefs.GetInt("numberLanguae"), 6];
		GameObject.Find("Game_Controller").GetComponent<Game_Controller>().AreYouSureWindow.transform.Find ("TextNo").GetComponent<TextMesh>().text = wordsDB.uiTextsLang[PlayerPrefs.GetInt("numberLanguae"), 7];

	}



}
