using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mute : MonoBehaviour {

    public Sprite Mute, unMute;
    public Image muteImg;
	// Update is called once per frame
	void Update () {

    }
    public void click()
    {
        if (PlayerPrefs.GetInt("mute") > 0)
        {
            muteImg.sprite = unMute;
        }
        else if (PlayerPrefs.GetInt("mute") < 1)
        {
            muteImg.sprite = Mute;
        }
    }
}
