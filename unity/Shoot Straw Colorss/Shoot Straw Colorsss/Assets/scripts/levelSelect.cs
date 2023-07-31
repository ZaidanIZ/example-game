using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class levelSelect : MonoBehaviour
{

    public Button[] allLevels;
    void Start()
    {
        print(PlayerPrefs.GetInt("level"));
        for (int i = 0; i < 300; i++)
        {
            transform.GetChild(i).name = (i + 1).ToString();
            transform.GetChild(i).transform.GetChild(0).GetComponent<Text>().text = (i + 1).ToString();
        }


        for (int i = 0; i < allLevels.Length; i++)
        {
            if (PlayerPrefs.GetInt("level") >= i)
            {
                allLevels[i].interactable = true;
                //allLevels[i].image.sprite = unlocked;
            }
            else
            {
                allLevels[i].interactable = false;
                //allLevels[i].image.sprite = locked;

            }
        }

    }
    public static bool call;
    public static string level;
    public void back()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void buttonClick()
    {
        call = true;
        level = EventSystem.current.currentSelectedGameObject.transform.name;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}