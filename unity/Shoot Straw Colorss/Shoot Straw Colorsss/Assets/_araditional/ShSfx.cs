using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShSfx : MonoBehaviour
{

    public AudioClip sfx1;
    public AudioSource sourceSfx;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void playSfx()
    {
        if (PlayerPrefs.GetString("muteSfx", "false") == "false")
        {
            sourceSfx.clip = sfx1;
            sourceSfx.PlayOneShot(sfx1);

        }
    }
}
