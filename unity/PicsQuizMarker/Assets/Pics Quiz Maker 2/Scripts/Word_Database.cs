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
using System.Collections;

public class Word_Database : MonoBehaviour 
{

	public string[,] uiTextsLang = new string[14, 10]
	{
		
		// TRANSLATIONS OF THE UI THEXTS
		// YOU JUST NEED COMPLETE THE LANGUAGES THAT YOU WILL USE
		// FILL THE SPACES OF THE ARRAY (THE QUOTATION MARKS) WITH THE CORRESPONDING TRANSLATION

									/* The \n is for do a newline */
		/* LANGUAGE BY DEFAULT: */ {"LEVEL", "COMPLETED", "PLAY", "Solve puzzle for me", "Show one random\nletter of the answer", "coins", "OK", "NO", "<b>You win!</b>\nWe haven't more\nquiz for you.\n<b>Do you want restart?</b>", "You don't have the\nnecessary coins."},
		/* LANGUAGE # 1:  */ 	   {"NIVEL", "COMPLETADO", "JUGAR", "Resolver puzzle\n por mi", "Mostrar una letra\nde la respuesta", "monedas", "OK", "NO", "<b>¡Ganaste!</b>\nNo tenemos mas\npreguntas para ti.\n<b>¿Quieres reiniciar?</b>", "No tienes las\nmonedas necesarias."},
		/* LANGUAGE # 2:  */       {"", "", "", "", "", "", "", "", "", ""},
		/* LANGUAGE # 3:  */       {"", "", "", "", "", "", "", "", "", ""},
		/* LANGUAGE # 4:  */       {"", "", "", "", "", "", "", "", "", ""},
		/* LANGUAGE # 5:  */       {"", "", "", "", "", "", "", "", "", ""},
		/* LANGUAGE # 6:  */       {"", "", "", "", "", "", "", "", "", ""},
		/* LANGUAGE # 7:  */       {"", "", "", "", "", "", "", "", "", ""},
		/* LANGUAGE # 8:  */       {"", "", "", "", "", "", "", "", "", ""},
		/* LANGUAGE # 9:  */       {"", "", "", "", "", "", "", "", "", ""},
		/* LANGUAGE # 10: */       {"", "", "", "", "", "", "", "", "", ""},
		/* LANGUAGE # 11: */       {"", "", "", "", "", "", "", "", "", ""},
		/* LANGUAGE # 12: */       {"", "", "", "", "", "", "", "", "", ""},
		/* LANGUAGE # 13: */       {"", "", "", "", "", "", "", "", "", ""},
		
	};
	
	public string[] words_List = new string[1];
	public Sprite[] image = new Sprite[1];

	//COINS
	public int coinsWinedByWord = 5;
	public int coinsToShowOneLetter = 2;
	public int coinsToSolveWord = 8;
	public int startWithCoins = 20;
    public bool restartCoinsWhenRestartTheGame = false;
	//LANGUAGES
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
	

	void Awake () 
	{

		if (!PlayerPrefs.HasKey("coinsPlayer")) 
		{

			PlayerPrefs.SetInt ("coinsPlayer", startWithCoins);

		}

	}
	

}