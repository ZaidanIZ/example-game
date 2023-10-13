using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    public GameObject
        backgroundBlur,
        background,
        foreground;

    [Space(20)]
    public MeshRenderer
    bgMeshRenderer,
    frMeshRenderer;

    [Space(20)]
    public Texture backgroundDay;
    public Texture
        backgroundNight,
        foregroundDay,
        foregroundNight;

    [Space(20)]
    public Transform zombieHolder;

    public GameObject zombie1Prefab;
    public GameObject
        zombie2Prefab,
        zombie3Prefab;

    [Space(20)]
    public Transform itemHolder;

    public GameObject[] items;

    [Space(20)]
    public float objectMaxYPos;
    public float objectMinYPos;

    [Space(20)]
    public float nextTimeToSpawn = 2.0f;
    public float
        streetSpeed,
        backgroundSpeed;

    public float[] zombieSpeed;

    [Space(20)]
    public float distanceFactor = 1.0f;

    private GameObject[]
        zombieList,
        itemList;

    private float score;

    // Temporary variables
    private float
        tmp1,
        tmp2,
        tmp3,
        tmp4,
        tmp5,
        tmp6,
        tmp7;

    #region define some hard code

    private float
        distanceFactor_state1 = 13.0f,
        distanceFactor_state2 = 15.0f,
        distanceFactor_state3 = 18.0f,
        distanceFactor_state4 = 21.0f,
        distanceFactor_state5 = 30.0f,
        nextTimeToSpawn_state1 = 0.5f,
        nextTimeToSpawn_state2 = 0.35f,
        nextTimeToSpawn_state3 = 0.27f,
        nextTimeToSpawn_state4 = 0.19f,
        nextTimeToSpawn_state5 = 0.17f,
        streetSpeed_state1 = 0.006f,
        streetSpeed_state2 = 0.007f,
        streetSpeed_state3 = 0.009f,
        streetSpeed_state4 = 0.012f,
        streetSpeed_state5 = 0.017f,
        bgSpeed_state1 = 0.003f,
        bgSpeed_state2 = 0.0035f,
        bgSpeed_state3 = 0.004f,
        bgSpeed_state4 = 0.005f,
        bgSpeed_state5 = 0.007f;

    private float[]
        zombieSpeed_state1 = new float[] { 0.12f, 0.14f, 0.16f },
        zombieSpeed_state2 = new float[] { 0.14f, 0.16f, 0.22f },
        zombieSpeed_state3 = new float[] { 0.18f, 0.20f, 0.26f },
        zombieSpeed_state4 = new float[] { 0.25f, 0.27f, 0.33f },
        zombieSpeed_state5 = new float[] { 0.35f, 0.37f, 0.43f };

    #endregion

    private int selectedCharacter;

    private bool timeEffect = false;

    public float Coin { get; set; }
    public float Diamond { get; set; }

    public bool GameOver { get; set; }
    public bool Saved { get; set; }
    public bool Paused { get; set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(this.gameObject);
        }
		Application.targetFrameRate = 60;
    }

    void Start()
    {
        InitZombie();
        InitItem();

        ResetGame();
    }

    private void InitZombie()
    {
        zombieList = new GameObject[15];

        for (var i = 0; i < zombieList.Length; i++)
        {
            GameObject clone = null;
            if (i >= 0 && i <= 4)
            {
                clone = Instantiate(zombie1Prefab, Vector3.zero, Quaternion.identity);
            }
            else if (i >= 5 && i <= 9)
            {
                clone = Instantiate(zombie2Prefab, Vector3.zero, Quaternion.identity);
            }
            else if (i >= 10 && i <= 14)
            {
                clone = Instantiate(zombie3Prefab, Vector3.zero, Quaternion.identity);
            }

            clone.transform.SetParent(zombieHolder);
            clone.GetComponent<ObjectsMove>().SetstartPosAndEndPos();
            clone.SetActive(false);
            zombieList[i] = clone;
        }
    }

    private void InitItem()
    {
        itemList = new GameObject[18];

        var j = 0;
        for (var i = 0; i < itemList.Length; i++)
        {
            if (i == 3 || i == 6 || i == 9 || i == 12 || i == 15)
            {
                j++;
            }

            GameObject clone = null;
            clone = Instantiate(items[j], Vector3.zero, Quaternion.identity);

            clone.transform.SetParent(itemHolder);
            clone.GetComponent<ObjectsMove>().SetstartPosAndEndPos();
            clone.SetActive(false);
            itemList[i] = clone;
        }
    }

    private void SetTemporaryVariables()
    {
        tmp1 = streetSpeed_state1; tmp2 = bgSpeed_state1; tmp3 = zombieSpeed_state1[0];
        tmp4 = zombieSpeed_state1[1]; tmp5 = zombieSpeed_state1[2]; tmp6 = nextTimeToSpawn_state1;
        tmp7 = distanceFactor_state1;
    }

    public void ResetGame()
    {
        if (PlayerPrefs.GetInt(Constants.SOUND, 1) == 1)
        {
            if (SoundManager.Instance.timer.isPlaying)
            {
                SoundManager.Instance.timer.mute = false;
                SoundManager.Instance.timer.Stop();
            }
        }

        score = 0.0f;
        Coin = PlayerPrefs.GetFloat(Constants.COIN, 0.0f);
        Diamond = PlayerPrefs.GetFloat(Constants.DIAMOND, 0.0f);

        GameOver = false;
        Saved = false;

        background.SetActive(false);
        foreground.SetActive(false);
        backgroundBlur.SetActive(true);

        selectedCharacter = PlayerPrefs.GetInt(Constants.SELECTED_CHARACTER, 1);
        PlayerController.Instance.characters[selectedCharacter - 1].SetActive(false);
        PlayerController.Instance.HP = 3;

        UIManager.Instance.hearts[0].SetActive(true);
        UIManager.Instance.hearts[1].SetActive(true);
        UIManager.Instance.hearts[2].SetActive(true);

        SetTemporaryVariables();
        ChangeSpeed(tmp1, tmp2, tmp3, tmp4, tmp5, tmp6, tmp7);
    }

    public void StartGame()
    {
        backgroundBlur.SetActive(false);
        background.SetActive(true);
        foreground.SetActive(true);

        selectedCharacter = PlayerPrefs.GetInt(Constants.SELECTED_CHARACTER, 1);
        PlayerController.Instance.characters[selectedCharacter - 1].SetActive(true);
        PlayerController.Instance.transform.position = Vector3.zero;
        PlayerController.Instance.GetSpriteMainChar();
        PlayerController.Instance.SetAxisX_Boundary();

        StartSpawnZombie();
        StartSpawnItems();

        // Switch day and night
        StartCoroutine("SwitchDayAndNight");
    }

    private void StartSpawnZombie()
    {
        StartCoroutine("SpawnZombie");
    }

    private IEnumerator SpawnZombie()
    {
        var index = 0;

        while (!GameOver)
        {
            yield return new WaitForSeconds(nextTimeToSpawn);

            do
            {
                index = Random.Range(0, zombieList.Length - 1);

            } while (zombieList[index].activeInHierarchy);

            zombieList[index].SetActive(true);
        }
    }

    private void StartSpawnItems()
    {
        StartCoroutine("SpawnItem");
    }

    private IEnumerator SpawnItem()
    {
        var index = 0;

        while (!GameOver)
        {
            yield return new WaitForSeconds(Random.Range(2.0f, 3.5f));

            do
            {
                index = Random.Range(0, itemList.Length - 1);

            } while (itemList[index].activeInHierarchy);

            itemList[index].SetActive(true);
        }
    }

    private IEnumerator SwitchDayAndNight()
    {
        while (!GameOver)
        {
            StartCoroutine("ZombieVoice");

            yield return new WaitForSeconds(15.0f);

            if (bgMeshRenderer.material.mainTexture == backgroundDay)
            {
                bgMeshRenderer.material.mainTexture = backgroundNight;
                frMeshRenderer.material.mainTexture = foregroundNight;

                if (PlayerPrefs.GetInt(Constants.SOUND, 1) == 1)
                {
                    SoundManager.Instance.bgNight.Play();
                }
            }
            else
            {
                bgMeshRenderer.material.mainTexture = backgroundDay;
                frMeshRenderer.material.mainTexture = foregroundDay;

                if (PlayerPrefs.GetInt(Constants.SOUND, 1) == 1)
                {
                    SoundManager.Instance.bgDay.Play();
                }
            }
        }
    }

    private IEnumerator ZombieVoice()
    {
        while (!GameOver)
        {
            yield return new WaitForSeconds(Random.Range(2.5f, 4.0f));

            if (PlayerPrefs.GetInt(Constants.SOUND, 1) == 1)
            {
                if (Random.value < 0.25f)
                {
                    if (!SoundManager.Instance.bgVar1.isPlaying)
                        SoundManager.Instance.bgVar1.Play();
                }
                else if (0.25f <= Random.value && Random.value < 0.5f)
                {
                    if (!SoundManager.Instance.bgVar2.isPlaying)
                        SoundManager.Instance.bgVar2.Play();
                }
                else if (0.5f <= Random.value && Random.value < 0.75f)
                {
                    if (!SoundManager.Instance.bgVar3.isPlaying)
                        SoundManager.Instance.bgVar3.Play();
                }
                else
                {
                    if (!SoundManager.Instance.bgVar4.isPlaying)
                        SoundManager.Instance.bgVar4.Play();
                }
            }
        }
    }

    public void UpdateDistance()
    {
        score += Time.deltaTime * distanceFactor;

        UIManager.Instance.UpdateDistance(Mathf.Round(score));
        UIManager.Instance.UpdateScore(Mathf.Round(score));

        if (!timeEffect)
        {
            if (Mathf.RoundToInt(score) >= 200 && Mathf.RoundToInt(score) < 400)
            {
                ChangeSpeed(streetSpeed_state2, bgSpeed_state2, zombieSpeed_state2[0], zombieSpeed_state2[1], zombieSpeed_state2[2], nextTimeToSpawn_state2, distanceFactor_state2);
            }
            else if (Mathf.RoundToInt(score) >= 400 && Mathf.RoundToInt(score) < 700)
            {
                ChangeSpeed(streetSpeed_state3, bgSpeed_state3, zombieSpeed_state3[0], zombieSpeed_state3[1], zombieSpeed_state3[2], nextTimeToSpawn_state3, distanceFactor_state3);
            }
            else if (Mathf.RoundToInt(score) >= 700)
            {
                ChangeSpeed(streetSpeed_state4, bgSpeed_state4, zombieSpeed_state4[0], zombieSpeed_state4[1], zombieSpeed_state4[2], nextTimeToSpawn_state4, distanceFactor_state4);
            }
        }
    }

    public void SpeedUp()
    {
        if (PlayerPrefs.GetInt(Constants.SOUND, 1) == 1)
        {
            SoundManager.Instance.timer.Stop();
        }

        UIManager.Instance.dsItemTimer.gameObject.SetActive(false);
        UIManager.Instance.dsItemTimer.fillAmount = 1.0f;
        UIManager.Instance.DSItemCoolingDown = false;

        StopCoroutine("SpeedDownDuration");
        ChangeSpeed(tmp1, tmp2, tmp3, tmp4, tmp5, tmp6, tmp7);

        StopCoroutine("SpeedUpDuration");
        StartCoroutine("SpeedUpDuration");
    }

    private IEnumerator SpeedUpDuration()
    {
        timeEffect = true;

        tmp1 = streetSpeed; tmp2 = backgroundSpeed; tmp3 = zombieSpeed[0];
        tmp4 = zombieSpeed[1]; tmp5 = zombieSpeed[2]; tmp6 = nextTimeToSpawn;
        tmp7 = distanceFactor;

        if (Mathf.RoundToInt(score) < 200)
        {
            ChangeSpeed(streetSpeed_state2, bgSpeed_state2, zombieSpeed_state2[0], zombieSpeed_state2[1], zombieSpeed_state2[2], nextTimeToSpawn_state2, distanceFactor_state2);
        }
        else if (22 <= Mathf.RoundToInt(score) && Mathf.RoundToInt(score) < 400)
        {
            ChangeSpeed(streetSpeed_state3, bgSpeed_state3, zombieSpeed_state3[0], zombieSpeed_state3[1], zombieSpeed_state3[2], nextTimeToSpawn_state3, distanceFactor_state3);
        }
        else if (400 <= Mathf.RoundToInt(score) && Mathf.RoundToInt(score) < 700)
        {
            ChangeSpeed(streetSpeed_state4, bgSpeed_state4, zombieSpeed_state4[0], zombieSpeed_state4[1], zombieSpeed_state4[2], nextTimeToSpawn_state4, distanceFactor_state4);
        }
        else if (700 <= Mathf.RoundToInt(score))
        {
            ChangeSpeed(streetSpeed_state5, bgSpeed_state5, zombieSpeed_state5[0], zombieSpeed_state5[1], zombieSpeed_state5[2], nextTimeToSpawn_state5, distanceFactor_state5);
        }

        yield return new WaitForSeconds(3.0f);

        timeEffect = false;
        ChangeSpeed(tmp1, tmp2, tmp3, tmp4, tmp5, tmp6, tmp7);
    }

    private void ChangeSpeed(float num1, float num2, float num3, float num4, float num5, float num6, float num7)
    {
        streetSpeed = num1;
        backgroundSpeed = num2;
        zombieSpeed[0] = num3;
        zombieSpeed[1] = num4;
        zombieSpeed[2] = num5;

        nextTimeToSpawn = num6;
        distanceFactor = num7;
    }

    public void SpeedDown()
    {
        StopCoroutine("SpeedUpDuration");
        ChangeSpeed(tmp1, tmp2, tmp3, tmp4, tmp5, tmp6, tmp7);

        if (PlayerPrefs.GetInt(Constants.SOUND, 1) == 1)
        {
            SoundManager.Instance.timer.Stop();
        }

        StopCoroutine("SpeedDownDuration");
        StartCoroutine("SpeedDownDuration");
    }

    private IEnumerator SpeedDownDuration()
    {
        if (PlayerPrefs.GetInt(Constants.SOUND, 1) == 1)
        {
            SoundManager.Instance.timer.Play();
        }

        timeEffect = true;

        tmp1 = streetSpeed; tmp2 = backgroundSpeed; tmp3 = zombieSpeed[0];
        tmp4 = zombieSpeed[1]; tmp5 = zombieSpeed[2]; tmp6 = nextTimeToSpawn;
        tmp7 = distanceFactor;

        ChangeSpeed(0.003f, 0.001f, 0.065f, 0.07f, 0.08f, 0.7f, 8.0f);

        var timeWait = 2.0f + PlayerPrefs.GetInt(Constants.STATE_DECREASE_TIME, 1);

        // Show HUD
        UIManager.Instance.dsItemTimer.gameObject.SetActive(true);
        UIManager.Instance.dsItemTimer.fillAmount = 1.0f;
        UIManager.Instance.DSItemCoolingDown = true;
        UIManager.Instance.DSItemTimeDuration = timeWait;

        yield return new WaitForSeconds(timeWait);

        timeEffect = false;
        ChangeSpeed(tmp1, tmp2, tmp3, tmp4, tmp5, tmp6, tmp7);

        if (PlayerPrefs.GetInt(Constants.SOUND, 1) == 1)
        {
            SoundManager.Instance.timer.Stop();
        }
    }

    public void Over()
    {
        if (PlayerPrefs.GetInt(Constants.SOUND, 1) == 1)
        {
            if (SoundManager.Instance.timer.isPlaying)
            {
                SoundManager.Instance.timer.Stop();
            }
        }

        StopAllCoroutines();

        var bestScore = PlayerPrefs.GetFloat(Constants.BEST_SCORE, 0.0f);
        var currentScore = Mathf.Round(score);

        if (currentScore > bestScore)
        {
            PlayerPrefs.SetFloat(Constants.BEST_SCORE, currentScore);
        }

        UIManager.Instance.bestScoreInOverText.text = PlayerPrefs.GetFloat(Constants.BEST_SCORE, 0.0f) + "";

        // Set total coin
        PlayerPrefs.SetFloat(Constants.COIN, Coin);
        UIManager.Instance.coinInOverText.text = Coin + "";

        // Set total diamond
        PlayerPrefs.SetFloat(Constants.DIAMOND, Diamond);
        UIManager.Instance.diamondInHelpText.text = Diamond + "";
        UIManager.Instance.diamondInOverText.text = Diamond + "";

        StartCoroutine("ShowUpGameOver");
    }

    private IEnumerator ShowUpGameOver()
    {
		//AdsControl.Instance.showAds ();
        yield return new WaitForSeconds(1.0f);

        if (!Saved)
        {
            UIManager.Instance.helpMenu.SetActive(true);
            UIManager.Instance.backDelegate = UIManager.Instance.CloseHelp;
        }
        else
        {
            UIManager.Instance.overMenu.SetActive(true);
            UIManager.Instance.playDelegate = UIManager.Instance.PlayAgain;
        }
    }

    public void ContinueGame()
    {
        GameOver = false;
        timeEffect = false;

        PlayerController.Instance.characters[selectedCharacter - 1].SetActive(false);
        PlayerController.Instance.characters[selectedCharacter - 1].SetActive(true);
        PlayerController.Instance.HP = 1;
        UIManager.Instance.hearts[0].SetActive(true);

        ChangeSpeed(tmp1, tmp2, tmp3, tmp4, tmp5, tmp6, tmp7);

        StartSpawnZombie();
        StartSpawnItems();

        // Switch day and night
        StartCoroutine("SwitchDayAndNight");
    }

    public void ClearZombies()
    {
        Hide(ref zombieList);
    }

    public void ClearItems()
    {
        Hide(ref itemList);
    }

    private void Hide(ref GameObject[] list)
    {
        for (var i = list.Length - 1; i >= 0; i--)
        {
            if (list[i].activeInHierarchy)
            {
                list[i].SetActive(false);
            }
        }
    }
}
