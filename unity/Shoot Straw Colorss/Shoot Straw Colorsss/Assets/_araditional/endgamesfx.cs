using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endgamesfx : MonoBehaviour
{
   public AudioSource thisSfx;
    // Start is called before the first frame update
    void Start()
    {


        if (PlayerPrefs.GetString("muteSfx", "false") == "false")
        {
            thisSfx.Play();
        }
    }
    private void OnEnable()
    {
        if (PlayerPrefs.GetString("muteSfx", "false") == "false")
        {
            thisSfx.Play();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
