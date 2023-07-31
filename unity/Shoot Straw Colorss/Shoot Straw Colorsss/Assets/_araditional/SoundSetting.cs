using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSetting : MonoBehaviour
{


  public   bool muteBgm, muteSfx,lah;
    public string mih;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void doMuteSound(bool b) {
        muteBgm = b;
        muteSfx = b;

        PlayerPrefs.SetString("muteBgm", muteBgm.ToString());
        PlayerPrefs.SetString("muteSfx", muteSfx.ToString());
        print("s"+muteSfx.ToString()+ bool.Parse("True"));
        mih = PlayerPrefs.GetString("muteBgm", "False");
        lah = bool.Parse(mih);
    }
}
