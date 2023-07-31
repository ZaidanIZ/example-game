/// <summary>
///
///----------- ESPAÑOL -----------
/// 
/// Las siguientes variables son las que almacenan los lenguajes, las palabras (o niveles) del juego y sus traducciones y las variables de coins.
/// Estos arrays, a excepción de uiTextsLang se llenan desde el inspector, controlandose con el script DB_EDITOR ubicado en la carpeta "editor".
/// 
///----------- ENGLISH -----------
/// 
/// The next variables store the languages, the words (or levels) of the game and the correspondig translations, and the variables of the coins.
/// This arrays, except uiTextsLang is fill since the inspectos, this is controlled with the "BD_EDITOR" script, this script is in the "editor" folder.
/// 
/// </summary>



using UnityEngine;

public class Word_Database : MonoBehaviour 
{

	public string[,] uiTextsLang = new string[14, 25]
	{
		
		// TRANSLATIONS OF THE UI THEXTS
		// YOU JUST NEED COMPLETE THE LANGUAGES THAT YOU WILL USE
		// FILL THE SPACES OF THE ARRAY (THE QUOTATION MARKS) WITH THE CORRESPONDING TRANSLATION

									/* The \n is for do a newline */
		/* LANGUAGE BY DEFAULT: */ {"LEVEL", "COMPLETED", "PLAY", "Solve puzzle for me", "Show one random\nletter of the answer", "coins", "OK", "NO", "", "You don't have\nenough coins.", "Category Selection", "Total solved words", "Total porcent", "", "Solved words", "Porcent solved", "MENU", "Solved questions:", "You need have ", " solved words to unlock.", "Continue", "Rate us 5 stars and get ", " coins", "Rate now!", "Later"},
		/* LANGUAGE # 1:  */ 	   {"NIVEL", "COMPLETADO", "JUGAR", "Resolver puzzle\n por mi", "Mostrar una letra\nde la respuesta", "monedas", "OK", "NO", "", "No tienes las\nmonedas necesarias.", "Selección de categoría", "Total de palabras resueltas", "Porcengaje total", "", "Palabras resueltas", "Porcentaje resuelto", "MENU", "Preguntas resueltas:", "Necesitas tener ", " palabras para desbloquear.", "Continuar", "Calificanos con 5 estrellas y obten ", " monedas", "Califica ahora!", "Después"},
		/* LANGUAGE # 2:  */       {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""},
		/* LANGUAGE # 3:  */       {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""},
		/* LANGUAGE # 4:  */       {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""},
		/* LANGUAGE # 5:  */       {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""},
		/* LANGUAGE # 6:  */       {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""},
		/* LANGUAGE # 7:  */       {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""},
		/* LANGUAGE # 8:  */       {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""},
		/* LANGUAGE # 9:  */       {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""},
		/* LANGUAGE # 10: */       {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""},
		/* LANGUAGE # 11: */       {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""},
		/* LANGUAGE # 12: */       {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""},
		/* LANGUAGE # 13: */       {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""},

    };

    public int howManyQuizSolvedNeedToUnlock = 0;
    //The lenght is 13 because here save the differents names of this category in the differents languages.
    public string[] nameOfCategory = new string[14];

    public string[] words_List = new string[1];
	public Sprite[] image = new Sprite[1];
    public bool[] solvedWords = new bool[1];

	//CONFIGS
	public int coinsWinedByWord = 5;
	public int coinsToShowOneLetter = 2;
	public int coinsToSolveWord = 8;
	public int startWithCoins = 20;
    public bool showInfoLaterOfSolveWord;

	//LANGUAGES (Words translations)
	public string[] languagesName = new string[1];
	public string[] language1 = new string[1];
	public string[] language2 = new string[1];
	public string[] language3 = new string[1];
	public string[] language4 = new string[1];
	public string[] language5 = new string[1];
	public string[] language6 = new string[1];
	public string[] language7 = new string[1];
	public string[] language8 = new string[1];
	public string[] language9 = new string[1];
	public string[] language10 = new string[1];
	public string[] language11 = new string[1];
	public string[] language12 = new string[1];
	public string[] language13 = new string[1];

    //LANGUAGES (Information translations)
    public string[] languageInfoWord0 = new string[1];
    public string[] languageInfoWord1 = new string[1];
    public string[] languageInfoWord2 = new string[1];
    public string[] languageInfoWord3 = new string[1];
    public string[] languageInfoWord4 = new string[1];
    public string[] languageInfoWord5 = new string[1];
    public string[] languageInfoWord6 = new string[1];
    public string[] languageInfoWord7 = new string[1];
    public string[] languageInfoWord8 = new string[1];
    public string[] languageInfoWord9 = new string[1];
    public string[] languageInfoWord10 = new string[1];
    public string[] languageInfoWord11 = new string[1];
    public string[] languageInfoWord12 = new string[1];
    public string[] languageInfoWord13 = new string[1];

    void Awake () 
	{

        if(PlayerPrefs.HasKey("levelWord"))
        {
            PlayerPrefs.SetInt("levelWord", 1);
        }

		if (!PlayerPrefs.HasKey("coinsPlayer")) 
		{

			PlayerPrefs.SetInt ("coinsPlayer", startWithCoins);

		}

		if (PlayerPrefs.HasKey("clickedFacebookButton")) 
		{
			Destroy(GameObject.Find("MENU").transform.Find("LikeFacebookButton").gameObject);
		}
      
        if (!PlayerPrefs.HasKey("firstTimeToPlay" + gameObject.name))
        {
            System.Array.Resize(ref solvedWords, words_List.Length);
            PlayerPrefsArray.SetBoolArray("completedWords" + gameObject.name, solvedWords);
            PlayerPrefs.SetInt("firstTimeToPlay" + gameObject.name, 1);

        }
        else
        {
            System.Array.Resize(ref solvedWords, words_List.Length);
            solvedWords = PlayerPrefsArray.GetBoolArray("completedWords" + gameObject.name);

            if (PlayerPrefsArray.GetBoolArray("completedWords" + gameObject.name).Length != words_List.Length)
            {
                System.Array.Resize(ref solvedWords, words_List.Length);
                PlayerPrefsArray.SetBoolArray("completedWords" + gameObject.name, solvedWords);
            }


        }

    }


}