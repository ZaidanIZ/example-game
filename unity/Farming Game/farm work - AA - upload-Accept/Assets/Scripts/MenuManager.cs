using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    public string MoreGamesURL,RateUSURL,ShareText;
    public Text LevelText;
    int Level;

    void Start()
    {
        Level  = PlayerPrefs.GetInt("LEVEL",1);
        LevelText.text = "LEVEL "+Level;
    }

    public void MoreGames(){
        Application.OpenURL(MoreGamesURL);
    }
     public void RateUS(){
        Application.OpenURL(RateUSURL);
    }

    public void StartGame(){
         SceneManager.LoadScene(Level);
    }
}
