using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
public class AudioPemain : MonoBehaviour
{


    public string[] namaScene;
    public AudioClip[] musicScene;
    public AudioSource musicSource;
    public bool isGantiScene;
    public string namaSceneBefore;
    public static AudioPemain misal;
    private void Awake()
    {

        if (misal == null)
        {
            misal = this;
        }
        else
        {

            if (misal != this)
            {
                Destroy(this.gameObject);
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        namaSceneBefore = SceneManager.GetActiveScene().name;
        ChangeBGM();
    }
    public bool isMute;
    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetString("muteBgm", "false") == "true")
        {
            musicSource.mute = true;
        }
        else {
            musicSource.mute = false;
        }
        isMute = bool.Parse(PlayerPrefs.GetString("muteBgm", "False"));
        musicSource.mute = isMute;
        if (namaSceneBefore != SceneManager.GetActiveScene().name) {
            ChangeBGM();
            namaSceneBefore = SceneManager.GetActiveScene().name;

        }
    }

    void ChangeBGM() {
        musicSource.Stop();
        for (int x = 0; x < musicScene.Length; x++) {
            if (SceneManager.GetActiveScene().name == namaScene[x]) {
                musicSource.clip = musicScene[x];
                musicSource.Play();
                break;

            }
        }

    }
}
