using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ui : MonoBehaviour
{
    
    public void back()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    }
    public void privacy()
    {

    }
    public void home()
    {
        SceneManager.LoadScene("main");
    }
    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        
    }
    public void mute()
    {
        //if (!PlayerPrefs.HasKey("mute"))
        if (PlayerPrefs.GetInt("mute") > 0)
        {
            PlayerPrefs.SetInt("mute", 0);
        }
        else if (PlayerPrefs.GetInt("mute") < 1)
        { 
            PlayerPrefs.SetInt("mute", 1);
        
        }

    }
}