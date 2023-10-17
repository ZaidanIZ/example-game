using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; set; }

    public GameObject
        inGame,
        menu,
        tutorialMenu,
        optionsMenu,
        nextBtn,
        preBtn,
        playGameBtn,
        helpMenu,
        overMenu,
        coinAndDiamondShop,
        customizationShop,
        charactersShop,
        upgradeShop,
        pauseMenu;

    [Space(20)]
    public Sprite[] tutorials;

    public Image contentTut;

    [Space(20)]
    public GameObject[] hearts;

    public Text
        scoreText,
        bestScoreText,
        bestScoreInOverText,
        diamondInHelpText,
        diamondInShopText,
        diamondInCustomText,
        diamondInOverText,
        coinInShopText,
        coinInCustomText,
        coinInOverText;

    [Space]
    public RectTransform coinAndDiamondShopContent;

    [Space(20)]
    public Image[] characterBtn;

    public Sprite[]
        normalCharacters,
        highLightCharacters;
    public Sprite
        useSprite,
        equipSprite;

    public Image[] purchaseCharacters;

    public Sprite[] stateBar;

    public Image
        itemTimeBar,
        itemZombieBar;

    [Space(20)]
    public Image musicCheckBox;
    public Image
        soundCheckBox,
        notifyCheckBox;

    public Sprite
        checkSprite,
        uncheckSprite;

    [Space(20)]
    public Image dsItemTimer;
    public Image zItemTimer;

    private float bestScore;

    private int lastSelectedIndex;

    public float DSItemTimeDuration { get; set; }
    public float ZItemTimeDuration { get; set; }

    public bool DSItemCoolingDown { get; set; }
    public bool ZItemCoolingDown { get; set; }

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
    }

    void Start()
    {
        //PlayerPrefs.SetFloat(Constants.COIN, 750.0f);
        //PlayerPrefs.SetFloat(Constants.DIAMOND, 13.0f);

        // Default character 1 can use
        PlayerPrefs.SetInt("statusC1", 1);

        CheckCharacters();
        CheckStateBars();
        CheckSetting();

        bestScore = PlayerPrefs.GetFloat(Constants.BEST_SCORE, 0.0f);
        bestScoreText.text = "BEST: " + bestScore;

        playDelegate = PrepareToPlayGame;
        backDelegate = BackToMenu;
    }

    public void OpenLink(string urlnya)
    {
        Application.OpenURL(urlnya);
    }

    private void CheckCharacters()
    {
        var selectedChar = PlayerPrefs.GetInt(Constants.SELECTED_CHARACTER, 1);

        for (var i = 0; i < characterBtn.Length; i++)
        {
            string status = "statusC" + (i + 1);
            if (PlayerPrefs.GetInt(status, 0) == 1)
            {
                characterBtn[i].color = Color.white;

                if ((i + 1) == selectedChar)
                {
                    characterBtn[i].sprite = highLightCharacters[i];
                    purchaseCharacters[i].sprite = equipSprite;

                    lastSelectedIndex = i;
                }
                else
                {
                    characterBtn[i].sprite = normalCharacters[i];
                    purchaseCharacters[i].sprite = useSprite;
                }
            }
        }
    }

    private void CheckStateBars()
    {
        itemTimeBar.sprite = stateBar[PlayerPrefs.GetInt(Constants.STATE_DECREASE_TIME, 1) - 1];
        itemZombieBar.sprite = stateBar[PlayerPrefs.GetInt(Constants.STATE_ZOMBIE_TIME, 1) - 1];
    }

    private void CheckSetting()
    {
        // Music
        if (PlayerPrefs.GetInt(Constants.MUSIC, 1) == 0)
        {
            musicCheckBox.sprite = uncheckSprite;
        }
        else
        {
            musicCheckBox.sprite = checkSprite;
            SoundManager.Instance.bgMusic.Play();
        }

        // Sound
        if (PlayerPrefs.GetInt(Constants.SOUND, 1) == 0)
        {
            soundCheckBox.sprite = uncheckSprite;
        }
        else
        {
            soundCheckBox.sprite = checkSprite;
        }

        // Notify
        if (PlayerPrefs.GetInt(Constants.NOTIFY, 1) == 0)
        {
            notifyCheckBox.sprite = uncheckSprite;
        }
        else
        {
            notifyCheckBox.sprite = checkSprite;
        }
    }

    void Update()
    {
        if (DSItemCoolingDown)
        {
            dsItemTimer.fillAmount -= 1.0f / DSItemTimeDuration * Time.deltaTime;

            if (dsItemTimer.fillAmount <= 0.0f)
            {
                DSItemCoolingDown = false;
                dsItemTimer.gameObject.SetActive(false);
            }
        }

        if (ZItemCoolingDown)
        {
            zItemTimer.fillAmount -= 1.0f / ZItemTimeDuration * Time.deltaTime;

            if (zItemTimer.fillAmount <= 0.0f)
            {
                ZItemCoolingDown = false;
                zItemTimer.gameObject.SetActive(false);
            }
        }
    }

    public void UpdateDistance(float score)
    {
        scoreText.text = score + "";
    }

    public void UpdateScore(float score)
    {
        if (score > bestScore)
        {
            bestScoreText.text = "BEST: " + score;
        }
    }

    public delegate void PlayDelegate();
    public PlayDelegate playDelegate;
    public void Play()
    {
        playDelegate();

        if (PlayerPrefs.GetInt(Constants.SOUND, 1) == 1)
        {
            SoundManager.Instance.genericBtn.Play();
        }
    }

    private void PrepareToPlayGame()
    {
        menu.SetActive(false);

        if (PlayerPrefs.GetInt(Constants.FIRST_TIME_PLAY, 1) == 1)
        {
            PlayerPrefs.SetInt(Constants.FIRST_TIME_PLAY, 0);
            tutorialMenu.SetActive(true);
        }
        else
        {
            coinInCustomText.text = GameController.Instance.Coin + "";
            diamondInCustomText.text = GameController.Instance.Diamond + "";
            customizationShop.SetActive(true);
            upgradeShop.SetActive(false);
            charactersShop.SetActive(true);

            playDelegate = PlayGame;
            backDelegate = BackToMenu;
        }
    }

    private void PlayGame()
    {
        if (tutorialMenu.activeInHierarchy)
        {
            tutorialMenu.SetActive(false);
        }

        if (customizationShop.activeInHierarchy)
        {
            customizationShop.SetActive(false);
        }

        inGame.SetActive(true);

        GameController.Instance.StartGame();
    }

    private void Resume()
    {
        if (SoundManager.Instance.timer.isPlaying)
        {
            SoundManager.Instance.timer.mute = false;
        }

        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        GameController.Instance.Paused = false;
    }

    private void CloseTutorialInGame()
    {
        tutorialMenu.SetActive(false);

        playDelegate = Resume;
    }

    public void PlayAgain()
    {
        overMenu.SetActive(false);
        inGame.SetActive(false);

        GameController.Instance.ResetGame();
        PrepareToPlayGame();
    }

    public delegate void BackDelegate();
    public PlayDelegate backDelegate;
    public void Back()
    {
        backDelegate();


        ADsummoner.adSummoner.LoadInterstitial();
        if (PlayerPrefs.GetInt(Constants.SOUND, 1) == 1)
        {
            SoundManager.Instance.back.Play();
        }
    }

    private void BackToMenu()
    {
        customizationShop.SetActive(false);
        menu.SetActive(true);

        playDelegate = PrepareToPlayGame;
    }

    private void LeaveShop()
    {
        coinAndDiamondShop.SetActive(false);
        menu.SetActive(true);

        playDelegate = PrepareToPlayGame;
    }

    private void BackToHelp()
    {
        coinAndDiamondShop.SetActive(false);
        helpMenu.SetActive(true);

        backDelegate = CloseHelp;
    }

    public void CloseHelp()
    {
        helpMenu.SetActive(false);
        overMenu.SetActive(true);

        playDelegate = PlayAgain;
    }

    private void BackToCustom()
    {
        coinAndDiamondShop.SetActive(false);
        customizationShop.SetActive(true);

        backDelegate = BackToMenu;
    }

    private void LeaveTutorial()
    {
        tutorialMenu.SetActive(false);
        menu.SetActive(true);

        playDelegate = PrepareToPlayGame;
    }

    private void LeaveOption()
    {
        optionsMenu.SetActive(false);
        menu.SetActive(true);

        playDelegate = PrepareToPlayGame;
    }

    // Next Btn is clicked
    public void NextBtn_Onclick()
    {
        if (PlayerPrefs.GetInt(Constants.SOUND, 1) == 1)
        {
            SoundManager.Instance.genericBtn.Play();
        }

        preBtn.SetActive(true);

        for (var i = 0; i < tutorials.Length; i++)
        {
            if (tutorials[i] == contentTut.sprite)
            {
                contentTut.sprite = tutorials[i + 1];

                if (i + 1 == tutorials.Length - 1)
                {
                    nextBtn.SetActive(false);
                    playGameBtn.SetActive(true);

                    if (!GameController.Instance.Paused)
                    {
                        playDelegate = PlayGame;
                    }
                }
                break;
            }
        }
    }

    // Previous Btn is clicked
    public void PreBtn_Onclick()
    {
        if (PlayerPrefs.GetInt(Constants.SOUND, 1) == 1)
        {
            SoundManager.Instance.genericBtn.Play();
        }

        if (playGameBtn.activeInHierarchy)
        {
            playGameBtn.SetActive(false);
            nextBtn.SetActive(true);
        }

        for (var i = 0; i < tutorials.Length; i++)
        {
            if (tutorials[i] == contentTut.sprite)
            {
                contentTut.sprite = tutorials[i - 1];

                if (i - 1 == 0)
                {
                    preBtn.SetActive(false);
                }
                break;
            }
        }
    }

    // Purchase diamond btn is clicked
    public void PurchaseBtn_Onclick()
    {
        if (PlayerPrefs.GetInt(Constants.SOUND, 1) == 1)
        {
            SoundManager.Instance.genericBtn.Play();
        }

        helpMenu.SetActive(false);
        ShowShop(-600.0f);

        backDelegate = BackToHelp;
    }

    // Buy back free
    public void BuyBackFreeBtn_Onclick()
    {
        if (PlayerPrefs.GetInt(Constants.SOUND, 1) == 1)
        {
            SoundManager.Instance.genericBtn.Play();
        }
        
        helpMenu.SetActive(false);
        GameController.Instance.Saved = true;
        GameController.Instance.ContinueGame();
    }

	public void ShowRewardVideo()
	{
        //AdsControl.Instance.showAds ();
        ADsummoner.adSummoner.ShowReward(afterReward);
       
        
	}

    public void afterReward()
    {
        GameController.Instance.ContinueGame();
        helpMenu.SetActive(false);
        ADsummoner.adSummoner.LoadReward();

    }



    public void ShowInterstitial()
    {
        ADsummoner.adSummoner.ShowInterstitial();
        ADsummoner.adSummoner.LoadInterstitial();
    }

    public void aferinter()
    {
        ADsummoner.adSummoner.LoadInterstitial();
    }

    // Buy back by diamond
    public void BuyBackBtn_Onclick()
    {
        if (PlayerPrefs.GetInt(Constants.SOUND, 1) == 1)
        {
            SoundManager.Instance.genericBtn.Play();
        }

        if (GameController.Instance.Diamond >= 3.0f)
        {
            GameController.Instance.Diamond -= 3.0f;
            PlayerPrefs.SetFloat(Constants.DIAMOND, GameController.Instance.Diamond);
            diamondInHelpText.text = GameController.Instance.Diamond + "";
            diamondInOverText.text = diamondInHelpText.text;

            helpMenu.SetActive(false);
            GameController.Instance.Saved = true;
            GameController.Instance.ContinueGame();
        }
    }

    // Home btn is clicked
    public void HomeBtn_Onclick()
    {
        if (PlayerPrefs.GetInt(Constants.SOUND, 1) == 1)
        {
            SoundManager.Instance.genericBtn.Play();
        }

        overMenu.SetActive(false);
        inGame.SetActive(false);
        menu.SetActive(true);

        GameController.Instance.ClearZombies();
        GameController.Instance.ResetGame();

        playDelegate = PrepareToPlayGame;
    }

    // Options btn is clicked
    public void OptionBtn_Onclick()
    {
        menu.SetActive(false);
        optionsMenu.SetActive(true);

        backDelegate = LeaveOption;

        if (PlayerPrefs.GetInt(Constants.SOUND, 1) == 1)
        {
            SoundManager.Instance.genericBtn.Play();
        }
    }

    // buttons in options
    public void Setting(string type)
    {
        if (PlayerPrefs.GetInt(Constants.SOUND, 1) == 1)
        {
            SoundManager.Instance.genericBtn.Play();
        }

        if (type == "music")
        {
            if (musicCheckBox.sprite == checkSprite)
            {
                musicCheckBox.sprite = uncheckSprite;
                PlayerPrefs.SetInt(Constants.MUSIC, 0);

                SoundManager.Instance.bgMusic.Stop();
            }
            else
            {
                musicCheckBox.sprite = checkSprite;
                PlayerPrefs.SetInt(Constants.MUSIC, 1);

                SoundManager.Instance.bgMusic.Play();
            }
        }
        else if (type == "sound")
        {
            if (soundCheckBox.sprite == checkSprite)
            {
                soundCheckBox.sprite = uncheckSprite;
                PlayerPrefs.SetInt(Constants.SOUND, 0);
            }
            else
            {
                soundCheckBox.sprite = checkSprite;
                PlayerPrefs.SetInt(Constants.SOUND, 1);
            }
        }
        else if (type == "notify")
        {
            if (notifyCheckBox.sprite == checkSprite)
            {
                notifyCheckBox.sprite = uncheckSprite;
                PlayerPrefs.SetInt(Constants.NOTIFY, 0);
            }
            else
            {
                notifyCheckBox.sprite = checkSprite;
                PlayerPrefs.SetInt(Constants.NOTIFY, 1);
            }
        }
    }

    // Tutorial btn is clicked
    public void TutorialBtn_Onclick()
    {
        menu.SetActive(false);

        contentTut.sprite = tutorials[0];
        preBtn.SetActive(false);
        playGameBtn.SetActive(false);
        nextBtn.SetActive(true);
        tutorialMenu.SetActive(true);

        backDelegate = LeaveTutorial;

        if (PlayerPrefs.GetInt(Constants.SOUND, 1) == 1)
        {
            SoundManager.Instance.genericBtn.Play();
        }
    }

    

    // Tutorial btn in game over is clicked
    public void TutorialBtn2_Onclick()
    {
        if (PlayerPrefs.GetInt(Constants.SOUND, 1) == 1)
        {
            SoundManager.Instance.genericBtn.Play();
        }

        overMenu.SetActive(false);
        inGame.SetActive(false);

        contentTut.sprite = tutorials[0];
        preBtn.SetActive(false);
        playGameBtn.SetActive(false);
        nextBtn.SetActive(true);
        tutorialMenu.SetActive(true);

        GameController.Instance.ResetGame();

        backDelegate = LeaveTutorial;
    }

    // Shop btn is clicked
    public void OpenShop()
    {
        ShowShop(966.0f);

        backDelegate = LeaveShop;

        if (PlayerPrefs.GetInt(Constants.SOUND, 1) == 1)
        {
            SoundManager.Instance.genericBtn.Play();
        }
    }

    // Upgrade btn is clicked
    public void UpgradeBtn_Onclick()
    {
        charactersShop.SetActive(false);
        upgradeShop.SetActive(true);

        if (PlayerPrefs.GetInt(Constants.SOUND, 1) == 1)
        {
            SoundManager.Instance.genericBtn.Play();
        }
    }

    private void ShowShop(float pos)
    {
        coinAndDiamondShop.SetActive(true);
        coinAndDiamondShopContent.anchoredPosition = new Vector2(pos, coinAndDiamondShopContent.anchoredPosition.y);
        coinInShopText.text = GameController.Instance.Coin + "";
        diamondInShopText.text = GameController.Instance.Diamond + "";
    }

    private void DeductionCoin(float price)
    {
        GameController.Instance.Coin -= price;
        PlayerPrefs.SetFloat(Constants.COIN, GameController.Instance.Coin);
        coinInCustomText.text = GameController.Instance.Coin + "";
    }

    // Select character
    public void SelectChar(int order)
    {
        string status = "statusC" + order;
        if (PlayerPrefs.GetInt(status, 0) == 0)
        {
            float price = float.Parse(purchaseCharacters[order - 1].name);

            if (GameController.Instance.Coin < price)
            {
                characterBtn[order - 1].GetComponent<RectTransform>().localScale = new Vector3(0.95f, 0.95f, 1.0f);
                customizationShop.SetActive(false);
                ShowShop(966.0f);

                backDelegate = BackToCustom;

                if (PlayerPrefs.GetInt(Constants.SOUND, 1) == 1)
                {
                    SoundManager.Instance.genericBtn.Play();
                }
            }
            else
            {
                PlayerPrefs.SetInt(status, 1);

                DeductionCoin(price);

                characterBtn[order - 1].color = Color.white;
                purchaseCharacters[order - 1].sprite = useSprite;

                if (PlayerPrefs.GetInt(Constants.SOUND, 1) == 1)
                {
                    SoundManager.Instance.buy.Play();
                }
            }
        }
        else
        {
            PlayerPrefs.SetInt(Constants.SELECTED_CHARACTER, order);

            characterBtn[lastSelectedIndex].sprite = normalCharacters[lastSelectedIndex];
            purchaseCharacters[lastSelectedIndex].sprite = useSprite;

            characterBtn[order - 1].sprite = highLightCharacters[order - 1];
            purchaseCharacters[order - 1].sprite = equipSprite;

            lastSelectedIndex = (order - 1);

            if (PlayerPrefs.GetInt(Constants.SOUND, 1) == 1)
            {
                SoundManager.Instance.genericBtn.Play();
            }
        }
    }

    // Characters btn is clicked
    public void CharactersBtn_Onclick()
    {
        upgradeShop.SetActive(false);
        charactersShop.SetActive(true);

        if (PlayerPrefs.GetInt(Constants.SOUND, 1) == 1)
        {
            SoundManager.Instance.genericBtn.Play();
        }
    }

    private void PurchaseTime(string type, string key, ref Image itemBar)
    {
        int state = PlayerPrefs.GetInt(key, 1);

        if (state < 5)
        {
            if (type == "coin")
            {
                if (GameController.Instance.Coin < 350.0f)
                {
                    customizationShop.SetActive(false);
                    ShowShop(966.0f);

                    if (PlayerPrefs.GetInt(Constants.SOUND, 1) == 1)
                    {
                        SoundManager.Instance.genericBtn.Play();
                    }
                }
                else
                {
                    DeductionCoin(350.0f);
                    state++;
                    itemBar.sprite = stateBar[state - 1];
                    PlayerPrefs.SetInt(key, state);

                    if (PlayerPrefs.GetInt(Constants.SOUND, 1) == 1)
                    {
                        SoundManager.Instance.buy.Play();
                    }
                }
            }
            else
            {
                if (GameController.Instance.Diamond < 10.0f)
                {
                    customizationShop.SetActive(false);
                    ShowShop(-600.0f);

                    if (PlayerPrefs.GetInt(Constants.SOUND, 1) == 1)
                    {
                        SoundManager.Instance.genericBtn.Play();
                    }
                }
                else
                {
                    GameController.Instance.Diamond -= 10.0f;
                    PlayerPrefs.SetFloat(Constants.DIAMOND, GameController.Instance.Diamond);
                    diamondInCustomText.text = GameController.Instance.Diamond + "";

                    state++;
                    itemBar.sprite = stateBar[state - 1];
                    PlayerPrefs.SetInt(key, state);

                    if (PlayerPrefs.GetInt(Constants.SOUND, 1) == 1)
                    {
                        SoundManager.Instance.buy.Play();
                    }
                }
            }

            backDelegate = BackToCustom;
        }
    }

    // Purchase decrease time
    public void PurchaseDecreaseTime(string type)
    {
        PurchaseTime(type, Constants.STATE_DECREASE_TIME, ref itemTimeBar);
    }

    // Purchase decrease time
    public void PurchaseZombie(string type)
    {
        PurchaseTime(type, Constants.STATE_ZOMBIE_TIME, ref itemZombieBar);
    }

    // Pause btn is clicked()
    public void PauseBtn_Onclick()
    {
        if (PlayerPrefs.GetInt(Constants.SOUND, 1) == 1)
        {
            SoundManager.Instance.genericBtn.Play();

            if (SoundManager.Instance.timer.isPlaying)
            {
                SoundManager.Instance.timer.mute = true;
            }
        }

        GameController.Instance.Paused = true;
        Time.timeScale = 0.0f;
        pauseMenu.SetActive(true);

        playDelegate = Resume;
    }

    // Tutorial btn in game pause
    public void TutorialBtn3_Onclick()
    {
        if (PlayerPrefs.GetInt(Constants.SOUND, 1) == 1)
        {
            SoundManager.Instance.genericBtn.Play();
        }

        contentTut.sprite = tutorials[0];
        preBtn.SetActive(false);
        playGameBtn.SetActive(false);
        nextBtn.SetActive(true);
        tutorialMenu.SetActive(true);

        playDelegate = CloseTutorialInGame;
        backDelegate = CloseTutorialInGame;
    }

    // Home btn in game pause
    public void HomeBtn2_Onclick()
    {
        if (PlayerPrefs.GetInt(Constants.SOUND, 1) == 1)
        {
            SoundManager.Instance.genericBtn.Play();
        }

        DSItemCoolingDown = false;
        ZItemCoolingDown = false;
        dsItemTimer.gameObject.SetActive(false);
        zItemTimer.gameObject.SetActive(false);

        Time.timeScale = 1.0f;
        GameController.Instance.Paused = false;

        pauseMenu.SetActive(false);
        inGame.SetActive(false);
        menu.SetActive(true);

        GameController.Instance.GameOver = true;
        GameController.Instance.StopAllCoroutines();
        GameController.Instance.ResetGame();
        GameController.Instance.ClearZombies();
        GameController.Instance.ClearItems();

        playDelegate = PrepareToPlayGame;
        ADsummoner.adSummoner.LoadInterstitial();
    }

    // Purchase coin and diamond
    public void PurchaseCoinAndDiamond(string str)
    {
        string type = System.Text.RegularExpressions.Regex.Match(str, @"\D+").Value;
        float bonus = float.Parse(System.Text.RegularExpressions.Regex.Match(str, @"\d+").Value);

        if (type == "C")
        {
            GameController.Instance.Coin += bonus ;
            PlayerPrefs.SetFloat(Constants.COIN, GameController.Instance.Coin);
            coinInShopText.text = GameController.Instance.Coin + "";
        }
        else if (type == "D")
        {
            GameController.Instance.Diamond += bonus;
            PlayerPrefs.SetFloat(Constants.DIAMOND, GameController.Instance.Diamond);
            diamondInShopText.text = GameController.Instance.Diamond + "";
        }
    }
}
