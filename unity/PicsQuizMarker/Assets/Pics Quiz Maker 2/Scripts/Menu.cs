/// <summary>
///
///----------- ESPAÑOL -----------
/// 
/// Este script controla los botones de lenguage del menu y el boton de PLAY.
/// Los botones de sonido son controlados por el script "Sounds" que esta agregado al objeto "Main Camera".
/// 
///----------- ENGLISH -----------
/// 
/// This script controls the language buttons of the menu and the "PLAY" button.
/// The buttons of sounds are controlled by the "Sounds" script, that it's added to the "Main Camera" object.
/// 
/// </summary>

using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour
{

	public GameObject languageButton;
	public GameObject obscuredBackGround;
	Vector3 positionButton = new Vector3 (-9.37f, -3.783f, -1.72f);
	GameObject[] instantiatedButtons;
	bool showLanguageButtons = false;
	string actualLanguage;

	void Awake () 
	{

		if (!PlayerPrefs.HasKey("language"))
		{

			PlayerPrefs.SetString("language", GameObject.Find("Words_Database").GetComponent<Word_Database>().languagesName[0]);
			actualLanguage = GameObject.Find("Words_Database").GetComponent<Word_Database>().languagesName[0];

		}
		else 
		{

			actualLanguage = PlayerPrefs.GetString("language");

		}

		GameObject.Find("MENU").transform.Find("LanguagesContainer").transform.Find("ActualLanguage").GetComponent<TextMesh>().text = actualLanguage;


	}

	public void Start () 
	{

		instantiatedButtons = new GameObject[GameObject.Find("Words_Database").GetComponent<Word_Database>().languagesName.Length];

	}
	

	void Update () 
	{

		ClicDetections ();

	}

	void ClicDetections () 
	{

		if (Input.GetMouseButtonUp (0))
		{
			
			RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 10)), Vector2.zero);
			
			if (hit.collider != null) 
			{
				
				GameObject objectHit = hit.collider.gameObject;
				
				if(objectHit.name == "buttonPlay" || objectHit.name == "CircleLevel") 
				{
					
					GameObject.Find("Main Camera").GetComponent<Animation>().Play("nextQuiz");
					
				} 
				else if (objectHit.name == "LanguagesContainer")
				{
					
					if (!showLanguageButtons)
					{
						
						//INSTANTIATE OBSCURE BACKGROUND
						Instantiate(obscuredBackGround, new Vector3 (-8, 0, -1.08f), Quaternion.identity);
						
						//INSTANTIATE BUTTONS
						for (int i = 0; i < instantiatedButtons.Length; i++)
						{
							if (GameObject.Find("Words_Database").GetComponent<Word_Database>().languagesName[i] != actualLanguage) 
							{
								
								instantiatedButtons[i] = Instantiate(languageButton, positionButton, Quaternion.identity) as GameObject;
								instantiatedButtons[i].transform.parent = GameObject.Find("MENU").transform.Find("LanguagesContainer").transform;
								instantiatedButtons[i].name = "buttonLanguage"+GameObject.Find("Words_Database").GetComponent<Word_Database>().languagesName[i];
								instantiatedButtons[i].transform.Find("LanguageText").GetComponent<TextMesh>().text = GameObject.Find("Words_Database").GetComponent<Word_Database>().languagesName[i];
								positionButton = new Vector3(positionButton.x, positionButton.y + 0.7f, positionButton.z);
								
							}
						}
						
						showLanguageButtons = true;
						
					}
					else 
					{
						
						CloseLanguages ();
						
					}
				} 
				else if (objectHit.name == "ObscureBackground(Clone)") 
				{
					
					CloseLanguages ();
					
				}
				
				for (int i = 0; i < instantiatedButtons.Length; i++) 
				{
					
					if (instantiatedButtons[i] != null)
					{
						
						if (objectHit.name == instantiatedButtons[i].name) 
						{
							PlayerPrefs.SetString("language", GameObject.Find("Words_Database").GetComponent<Word_Database>().languagesName[i]);
							PlayerPrefs.SetInt("numberLanguae", i);
							Awake();
							CloseLanguages();
						}
						
					}
				}
			}
		}

	}

	void CloseLanguages () 
	{

		positionButton = new Vector3 (-9.37f, -3.783f, -1.72f);
		Destroy(GameObject.Find("ObscureBackground(Clone)").gameObject);
		
		for (int i = 0; i < instantiatedButtons.Length; i++) 
		{
			if (instantiatedButtons[i] != null)
			{
				Destroy(instantiatedButtons[i].gameObject);
			}
		}
		
		showLanguageButtons = false;
		Texts_Changer.Refresh_Language();

	}
}
