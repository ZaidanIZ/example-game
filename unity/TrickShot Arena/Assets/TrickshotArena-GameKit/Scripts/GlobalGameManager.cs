using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using TrickshotArena;

namespace TrickshotArena
{
    public class GlobalGameManager : MonoBehaviour
    {
        /// <summary>
        /// This master class controls everything within the game, including receiving player inputs,
        /// creating new levels, managing score and bestScore, handling ingame events (start, end, win, lose)
        /// updating UI data, and playing sound fx.
        /// </summary>

        public static GlobalGameManager instance;

        //UI
        public GameObject startMenuUI;
        public GameObject LogoHolderUI;
        public GameObject ingameHUD;

        public static int level;
        public static bool initLevel;
        public static int playerHealth;

        //Global flags
        public static bool goalHappened;
        public static bool shootHappened;
        public static bool gameIsFinished;
        public static bool gameIsStarted;
        public static bool canTapOnUI;
        private bool canStart;

        private int bestRecord;

        //sound btn states
        public static int soundStatus;
        public Button soundButton;
        public Sprite[] soundButtonIcons;

        //reference to public prefab (level objects)
        public GameObject levelStructure;

        //mamixmu distance that players can drag away from selected unit to shoot the ball (is in direct relation with shoot power)
        public static float maxDistance = 3.0f;

        //After player shot the ball and did not scored a goal, we need to wait for a few second and then the game is over
        public static float shootTime;
        internal float shootCheckWait = 5;

        //gameObject references
        public GameObject goalPlane;
        public GameObject tryAgainPlane;
        public GameObject helpPanel;
        public Text uiGameLevel;
        //public GameObject uiBestRecord;
        public GameObject debuggerUI;

        //health
        public Image healthBarUI;
        public Sprite[] availableHealthIcons;

        //AudioClips
        public AudioClip generalTap;
        public AudioClip startWistle;
        public AudioClip finishWistle;
        public AudioClip newRecord;
        public AudioClip healthPlus;
        public AudioClip healthMinus;
        public AudioClip[] goalSfx;
        public AudioClip[] goalHappenedSfx;
        public AudioClip[] crowdChants;
        private bool canPlayCrowdChants;

        //Public references
        public GameObject gameEndPanel;
        public Text gameEndRecord;
        public Text gameEndBestRecord;
        public GameObject newRecordRay;
        private GameObject adManager;
        public Randomize randommmm;


        void Awake()
        {
            instance = this;
            //debuggerUI.GetComponent<TextMesh>().text += "Game Awaked." + "\r\n";

            //debug
            //PlayerPrefs.DeleteAll();

            //Set First level
            level = 1;
            //cheat/debug - You can quickly test a new level by uncommenting/changing the below variable.
            //level = 4;

            playerHealth = 3;   //player always starts with 3 health

            //init
            goalHappened = false;
            initLevel = false;
            shootHappened = false;
            gameIsFinished = false;
            gameIsStarted = false;
            canTapOnUI = true;
            shootTime = 0;
            canPlayCrowdChants = true;
            canStart = true;

            //first run sets to enable sound
            soundStatus = PlayerPrefs.GetInt("IsSoundEnabled", 1);
            soundButton.image.sprite = soundButtonIcons[soundStatus];
            if (soundStatus == 0)
                AudioListener.pause = true;

            //get best record
            bestRecord = PlayerPrefs.GetInt("BestRecord");
        }


        void Start()
        {
            
        }


        void Update()
        {
            if (!gameIsStarted)
            {
                return;
            }

            if (!initLevel)
            {
                initLevel = true;
                spawnLevelObjects();
            }

            //every now and then, play some crowd chants
            StartCoroutine(playCrowdChants());

            //show game level on UI
            uiGameLevel.text = level.ToString();

            //update remaining health on UI
            ManageHealthUI();


            //Update - manual win/lose
            if (Application.isEditor)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                   StartCoroutine(managePostGoal("Player"));
                    
                }

                if (Input.GetKeyDown(KeyCode.L))
                {
                    gameIsFinished = true;
                    StartCoroutine(manageGameFinishState());
                }
            }
        }


        /// <summary>
        /// Show player lives on UI
        /// </summary>
        public void ManageHealthUI()
        {
            healthBarUI.sprite = availableHealthIcons[playerHealth];
        }


        public void StartGame()
        {
            if (!canStart)
                return;
            canStart = false;

            playSfx(generalTap);
            StartCoroutine(DisableAndReactiveTap(1f));

            //if this is the first time we are playing this game, we need to show help panel
            if (PlayerPrefs.GetInt("IsFirstPlay") == 0)
            {
                PlayerPrefs.SetInt("IsFirstPlay", 1);
                //StartCoroutine(HelpPanelManager.GetInstance().MoveIn());
                helpPanel.GetComponent<Animator>().Play("HelpPanelOut");
            }
            else
                StartCoroutine(startTheGame());
        }


        /// <summary>
        /// Alternative start, by animating all UI elements
        /// </summary>
        public IEnumerator startTheGame()
        {
            //Move UI elements
            startMenuUI.GetComponent<Animator>().Play("MoveOut");
            LogoHolderUI.GetComponent<Animator>().Play("MoveLogoOut");
            ingameHUD.GetComponent<Animator>().Play("MoveHudIn");

            yield return new WaitForSeconds(1.5f);
            gameIsStarted = true;
        }


        /// <summary>
        /// Let player click/touch again after a short delay
        /// </summary>
        /// <param name="delay"></param>
        /// <returns></returns>
        public IEnumerator DisableAndReactiveTap(float delay)
        {
            canTapOnUI = false;
            yield return new WaitForSeconds(delay);
            canTapOnUI = true;
            print("Tap is enabled...");
        }


        /// <summary>
        /// When player eats a 1up, we need to animate the UI and add 1 unit to playerHealth.
        /// </summary>
        public void Eat1Up()
        {
            playSfx(healthPlus);
            StartCoroutine(AnimateHealthBar());

            if (playerHealth >= 3)
                return;

            playerHealth++;
        }


        /// <summary>
        /// When a goal is happened...
        /// </summary>
        public IEnumerator managePostGoal(string _goalBy)
        {
            //avoid counting a goal as two or more
            if (goalHappened)
                yield break;

            //soft pause the game
            goalHappened = true;
            shootHappened = false;

            //ads
            //Randomize.randomizee.Randomz();
            randommmm.Randomz();
            



            //wait a few seconds to show the effects , and physics cooldown
            if (Random.value > 0.9f)
                playSfx(goalSfx[Random.Range(0, goalSfx.Length)]);

            //play goal sfx
            GetComponent<AudioSource>().PlayOneShot(goalHappenedSfx[Random.Range(0, goalHappenedSfx.Length)], 1);

            //wait
            yield return new WaitForSeconds(0.5f);

            //hide current level objects (ball, player unit, opponents)
            HideCurrentLevelObjects();

            //shake the camera
            if (Random.value > 0.5f || level == 1)
                StartCoroutine(CameraShake.GetInstance().Shake(0.75f, 0.5f));

            //activate the goal event plane
            GameObject gp = null;
            gp = Instantiate(goalPlane, new Vector3(40, 0, -2), Quaternion.Euler(0, 0, 0)) as GameObject;
            float t = 0;
            float speed = 1.0f;
            while (t < 1)
            {
                t += Time.deltaTime * speed;
                gp.transform.position = new Vector3(Mathf.SmoothStep(40, 0, t), 0, -2);
                yield return 0;
            }
            yield return new WaitForSeconds(0.95f);
            float t2 = 0;
            while (t2 < 1)
            {
                t2 += Time.deltaTime * speed * 2;
                gp.transform.position = new Vector3(Mathf.SmoothStep(0, -40, t2), 0, -2);
                yield return 0;
            }
            Destroy(gp, 1.5f);

            //Cooldown
            print("...");
            yield return new WaitForSeconds(0.1f);

            //save the current bestRecord
            if (level > bestRecord)
            {
                PlayerPrefs.SetInt("BestRecord", level);
                //update UI
                //uiBestRecord.GetComponent<TextMesh>().text = level.ToString();
            }

            //Cooldown
            print("...");
            yield return new WaitForSeconds(0.15f);

            //increase level
            level++;

            //create the next level
            spawnLevelObjects();

            //Cooldown
            yield return new WaitForSeconds(0.5f);

            //only active this line if you want the sharing system to capture a shot at the beginning of each level.
            //This is a heavy task and might cause lag in the game.
            //SharingSystem.canCapture = true;

            goalHappened = false;
            playerController.canShoot = true;
        }


        /// <summary>
        /// hide and destroy current game objects available in the scene
        /// </summary>
        public void HideCurrentLevelObjects()
        {
            GameObject[] ps = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject p in ps)
            {
                StartCoroutine(p.GetComponent<playerController>().scaleAnimator(-1));
            }

            GameObject b = GameObject.FindGameObjectWithTag("ball");
            if (b)
                StartCoroutine(b.GetComponent<BallManager>().scaleAnimator(-1));

            GameObject[] oppos = GameObject.FindGameObjectsWithTag("Opponent");
            foreach (GameObject op in oppos)
            {
                StartCoroutine(op.GetComponent<OpponentUnitController>().scaleAnimator(-1));
            }

            GameObject ou = GameObject.FindGameObjectWithTag("1Up");
            if (ou)
                StartCoroutine(ou.GetComponent<OneUpManager>().scaleAnimator(-1));
        }


        /// <summary>
        /// After the game is finished, this function handles the events.
        /// </summary>
        /// <returns></returns>
        public IEnumerator manageGameFinishState()
        {
            print("GAME IS FINISHED.");

            //Temporary hide
            gameEndPanel.SetActive(false);
            newRecordRay.SetActive(false);

            //Play gameFinish wistle
            if (level <= bestRecord)
                playSfx(finishWistle);
            else
                playSfx(newRecord);

            //set UI records
            gameEndRecord.text = level.ToString();
            gameEndBestRecord.text = bestRecord.ToString();

            yield return new WaitForSeconds(0.75f);

            gameEndPanel.SetActive(true);
            gameEndPanel.GetComponent<Animator>().Play("PopIn");


            if (level > bestRecord)
                newRecordRay.SetActive(true);
        }


        /// <summary>
        /// Play a random crowd sfx every now and then to spice up the game
        /// </summary>
        /// <returns></returns>
        IEnumerator playCrowdChants()
        {
            if (canPlayCrowdChants)
            {
                canPlayCrowdChants = false;
                GetComponent<AudioSource>().PlayOneShot(crowdChants[Random.Range(0, crowdChants.Length)], 1);
                yield return new WaitForSeconds(Random.Range(15, 35));
                canPlayCrowdChants = true;
            }
        }


        /// <summary>
        /// Play the given sound clips
        /// </summary>
        /// <param name="_clip"></param>
        void playSfx(AudioClip _clip)
        {
            GetComponent<AudioSource>().clip = _clip;
            if (!GetComponent<AudioSource>().isPlaying)
                GetComponent<AudioSource>().Play();
        }


        /// <summary>
        /// Play the given sound clips with a delay
        /// </summary>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        IEnumerator playSfxDelayed(AudioClip c, float d)
        {
            yield return new WaitForSeconds(d);
            GetComponent<AudioSource>().clip = c;
            if (!GetComponent<AudioSource>().isPlaying)
                GetComponent<AudioSource>().Play();
        }


        /// <summary>
        /// Once a shoot is performed, we need to wait to see if the shoot has been led to a goal or not.
        /// Incase the shoot was not goaled, we need to decrease 1 life from player and recreate the level to let him try again.
        /// </summary>
        /// <returns></returns>
        public IEnumerator checkGameover()
        {
            float t = 0;
            while (t < shootCheckWait)
            {
                if (goalHappened)
                    yield break;

                t += Time.deltaTime;
                //print ("Time remained to score a goal: " + (shootCheckWait - t).ToString() );

                if (t >= shootCheckWait)
                {
                    //we lose a health here. if we still have health, we can continue. otherwise its gameover.
                    playerHealth--;

                    //play sfx
                    playSfx(healthMinus);

                    //create gfx on healthbar
                    StartCoroutine(AnimateHealthBar());

                    if (playerHealth > 0)
                    {
                        //recreate the current level
                        HideCurrentLevelObjects();

                        //yield return new WaitForSeconds(1.0f);
                        //spawnLevelObjects();
                        StartCoroutine(spawnLevelObjectsCo(1f));

                        //show tryagain panel
                        StartCoroutine(RunTryAgainPanel());
                        //randommmm.Randomz();
                    }
                    else
                    {
                        //game over
                        gameIsFinished = true;
                        StartCoroutine(manageGameFinishState());
                    }
                }

                yield return 0;
            }
        }


        /// <summary>
        /// Animate the healthBar by changing its scales
        /// </summary>
        /// <returns></returns>
        public IEnumerator AnimateHealthBar()
        {
            Vector3 normalScale = healthBarUI.transform.localScale;
            Vector3 targetScale = normalScale * 1.15f;

            for (int i = 0; i < 20; i++)
            {
                if (i % 2 == 0)
                    healthBarUI.transform.localScale = normalScale;
                else
                    healthBarUI.transform.localScale = targetScale;

                yield return new WaitForSeconds(0.07f);
            }

            //back to normal
            healthBarUI.transform.localScale = normalScale;
        }


        /// <summary>
        /// Show a TryAgain panel to player
        /// </summary>
        /// <returns></returns>
        public IEnumerator RunTryAgainPanel()
        {
            GameObject tap = null;
            tap = Instantiate(tryAgainPlane, new Vector3(40, 0, -2), Quaternion.Euler(0, 0, 0)) as GameObject;

            //move in
            float t = 0;
            float speed = 1.25f;
            while (t < 1)
            {
                t += Time.deltaTime * speed * 2;
                tap.transform.position = new Vector3(Mathf.SmoothStep(40, 0, t), 0, -2);
                yield return 0;
            }

            yield return new WaitForSeconds(0.6f);

            //move out
            float t2 = 0;
            while (t2 < 1)
            {
                t2 += Time.deltaTime * speed * 3;
                tap.transform.position = new Vector3(Mathf.SmoothStep(0, -40, t2), 0, -2);
                yield return 0;
            }

            print("end. here 3");
            //Destroy(tap, 1.5f);
            Destroy(tap);
        }


        /// <summary>
        /// Enable player to shoot again after a short delay
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        IEnumerator allowPlayerShoot(float d)
        {
            yield return new WaitForSeconds(d);
            playerController.canShoot = true;
        }


        public IEnumerator spawnLevelObjectsCo(float delay = 0.1f)
        {
            yield return new WaitForSeconds(delay);
            spawnLevelObjects();
        }


        /// <summary>
        /// Create a brand new level.
        /// You can create new levels in two ways:
        /// 
        /// 1. You can define new level structure via "MasterLevelManager" object and call the index of that level here like this:
        /// MasterLevelManager.GetInstance().CreateLevel(A , B);
        /// A = LevelType (0=easy, 1=medium, 2=hard)
        /// B = LevelID
        /// 
        /// 2. You can ask "MasterLevelManager" to create a random level for you. We have a few premade setup for easy to hard levels like this:
        /// MasterLevelManager.GetInstance().CreateRandomEasyLevel();
        /// MasterLevelManager.GetInstance().CreateRandomMediumLevel();
        /// MasterLevelManager.GetInstance().CreateRandomSemiHardLevel();
        /// MasterLevelManager.GetInstance().CreateRandomHardLevel();
        /// MasterLevelManager.GetInstance().CreateRandomSuperHardLevel();
        /// MasterLevelManager.GetInstance().CreateRandomInsaneLevel();
        /// 
        /// You can use these randomly generated levels anywhere. 
        /// </summary>
        void spawnLevelObjects()
        {
            print("level: " + level);
            StartCoroutine(playSfxDelayed(startWistle, 1.0f));
            StartCoroutine(allowPlayerShoot(1.0f));

            if (level == 1)
            {
                MasterLevelManager.GetInstance().CreateLevel(0, 0);
                //MasterLevelManager.GetInstance().CreateRandomEasyLevel();
            }

            if (level == 2)
            {
                MasterLevelManager.GetInstance().CreateLevel(0, 1);
            }

            if (level == 3)
            {
                MasterLevelManager.GetInstance().CreateLevel(0, 5);
            }

            if (level == 4)
            {
                MasterLevelManager.GetInstance().CreateLevel(0, 2);
            }

            if (level == 5)
            {
                MasterLevelManager.GetInstance().CreateLevel(0, 3);
            }

            if (level == 6)
            {
                MasterLevelManager.GetInstance().CreateLevel(0, 6);
            }

            if (level == 7)
            {
                MasterLevelManager.GetInstance().CreateLevel(0, 4);
            }

            if (level == 8)
            {
                MasterLevelManager.GetInstance().CreateLevel(0, 7);
            }

            if (level == 9)
            {
                MasterLevelManager.GetInstance().CreateRandomMediumLevel();
            }

            if (level == 10)
            {
                MasterLevelManager.GetInstance().CreateLevel(1, 1);
            }

            if (level == 11)
            {
                MasterLevelManager.GetInstance().CreateLevel(1, 0);
            }

            if (level == 12)
            {
                MasterLevelManager.GetInstance().CreateLevel(1, 7);
            }

            if (level == 13)
            {
                MasterLevelManager.GetInstance().CreateRandomMediumLevel();
            }

            if (level == 14)
            {
                MasterLevelManager.GetInstance().CreateRandomMediumLevel();
            }

            if (level == 15)
            {
                MasterLevelManager.GetInstance().CreateLevel(1, 2);
            }

            if (level == 16)
            {
                MasterLevelManager.GetInstance().CreateLevel(1, 3);
            }

            if (level == 17)
            {
                MasterLevelManager.GetInstance().CreateLevel(1, 4);
            }

            if (level == 18)
            {
                MasterLevelManager.GetInstance().CreateLevel(1, 5);
            }

            if (level == 19)
            {
                MasterLevelManager.GetInstance().CreateLevel(1, 6);
            }

            if (level == 20)
            {
                MasterLevelManager.GetInstance().CreateRandomSemiHardLevel();
            }

            if (level == 21)
            {
                MasterLevelManager.GetInstance().CreateLevel(2, 0);
            }

            if (level == 22)
            {
                MasterLevelManager.GetInstance().CreateRandomSemiHardLevel();
            }

            if (level == 23)
            {
                MasterLevelManager.GetInstance().CreateRandomHardLevel();
            }

            if (level == 24)
            {
                MasterLevelManager.GetInstance().CreateLevel(2, 1);
            }

            if (level == 25)
            {
                MasterLevelManager.GetInstance().CreateRandomHardLevel();
            }

            if (level == 26)
            {
                MasterLevelManager.GetInstance().CreateLevel(2, 2);
            }

            if (level == 27)
            {
                MasterLevelManager.GetInstance().CreateLevel(2, 3);
            }

            if (level == 28)
            {
                MasterLevelManager.GetInstance().CreateRandomHardLevel();
            }

            if (level == 29)
            {
                MasterLevelManager.GetInstance().CreateRandomHardLevel();
            }

            if (level == 30)
            {
                MasterLevelManager.GetInstance().CreateRandomHardLevel();
            }

            if (level == 31)
            {
                MasterLevelManager.GetInstance().CreateLevel(2, 4);
            }

            if (level >= 32 && level <= 34)
            {
                MasterLevelManager.GetInstance().CreateRandomHardLevel();
            }

            if (level == 35)
            {
                MasterLevelManager.GetInstance().CreateLevel(2, 5);
            }

            //Levels 36 ~ 50
            if (level >= 36 && level <= 50)
            {
                MasterLevelManager.GetInstance().CreateRandomSuperHardLevel();
            }

            //Levels > 50
            if (level >= 51)
            {
                MasterLevelManager.GetInstance().CreateRandomInsaneLevel();
            }
        }


        public void ClickOnPlayButton()
        {
            StartGame();
        }

        public void ClickOnSoundButton()
        {
            playSfx(generalTap);
            StartCoroutine(DisableAndReactiveTap(0.25f));
            //set correct texture
            if (soundStatus == 1)
            {
                soundStatus = 0;
                soundButton.image.sprite = soundButtonIcons[soundStatus];
                AudioListener.pause = true;
                PlayerPrefs.SetInt("IsSoundEnabled", 0);
            }
            else
            {
                soundStatus = 1;
                soundButton.image.sprite = soundButtonIcons[soundStatus];
                AudioListener.pause = false;
                PlayerPrefs.SetInt("IsSoundEnabled", 1);
            }
        }

        public void ClickOnHelpButton()
        {
            playSfx(generalTap);
            StartCoroutine(DisableAndReactiveTap(1f));
            //move helpPanel in
            //StartCoroutine(HelpPanelManager.GetInstance().MoveIn());
            helpPanel.GetComponent<Animator>().Play("HelpPanelIn");
            //Hide demo units
            HideCurrentLevelObjects();
        }

        public void ClickOnRestartButton()
        {
            playSfx(generalTap);
            StartCoroutine(DisableAndReactiveTap(1f));
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void ClickOnRateButton()
        {
            playSfx(generalTap);
            StartCoroutine(DisableAndReactiveTap(1f));
            print("Ready to Rate!");
            Application.OpenURL("market://details?id=" + "com.finalboss.trickshotarena");
        }

        public void ClickOnShareButton()
        {
            playSfx(generalTap);
            StartCoroutine(DisableAndReactiveTap(1f));
        }

        public void ClickOnHelpStartButton()
        {
            playSfx(generalTap);
            StartCoroutine(DisableAndReactiveTap(1f));
            //move helpPanel out
            //StartCoroutine(HelpPanelManager.GetInstance().MoveOut());
            helpPanel.GetComponent<Animator>().Play("HelpPanelOut");
            //start the game
            StartCoroutine(startTheGame());
        }

    }
}