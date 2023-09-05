using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrickshotArena
{
    public class MasterLevelManager : MonoBehaviour
    {
        /// <summary>
        /// We use this class to let player create new levels and add it to the available LevelStructure arrays,
        /// So he can later select these levels from within the "GlobalGameManager" class.
        /// This class is also able to generate random levels for you. You can simply call on of its already available 
        /// "RandomLevelGeneration" methods to face a new random level everytime.
        /// </summary>

        public static MasterLevelManager instance;
        public static int alignmentFactor;  //1 or -1           //the game can dynamically mirror all elements from ltr & rtl

        //reference to prefabs and scene gameobjects
        public GameObject coneHolder;
        public GameObject goalLightHelper;
        public GameObject ball;
        public GameObject player;
        public GameObject opponent;
        public GameObject oneUp;

        public LevelStructure[] selectedLevelStructure;
        [Space(20)]
        public LevelStructure[] EasyLevelStructure;
        [Space(10)]
        public LevelStructure[] NormalLevelStructure;
        [Space(10)]
        public LevelStructure[] HardLevelStructure;

        //Do not touch/edit.
        [Space(10)]
        internal int[] easyLevelsID;
        internal int[] mediumLevelsID;
        internal int[] hardLevelsID;


        void Awake()
        {
            instance = this;
        }


        void Start()
        {
            //we ignore the very first easy level
            easyLevelsID = new int[EasyLevelStructure.Length - 1];
            mediumLevelsID = new int[NormalLevelStructure.Length];
            hardLevelsID = new int[HardLevelStructure.Length];

            //for easy levels we start from 1, instead of 0.
            for (int i = 1; i < EasyLevelStructure.Length; i++)
                easyLevelsID[i - 1] = i;

            for (int j = 0; j < NormalLevelStructure.Length; j++)
                mediumLevelsID[j] = j;

            for (int k = 0; k < HardLevelStructure.Length; k++)
                hardLevelsID[k] = k;

            Shuffle(easyLevelsID);
            Shuffle(mediumLevelsID);
            Shuffle(hardLevelsID);

            //Debug
            print("Number of easy levels: " + (EasyLevelStructure.Length - 1));
            print("Number of medium levels: " + NormalLevelStructure.Length);
            print("Number of hard levels: " + HardLevelStructure.Length);
        }


        /// <summary>
        /// Shuffle the given array
        /// </summary>
        /// <param name="arr"></param>
        void Shuffle(int[] arr)
        {
            for (int t = 0; t < arr.Length; t++)
            {
                int tmp = arr[t];
                int r = Random.Range(t, arr.Length);
                arr[t] = arr[r];
                arr[r] = tmp;
            }
        }


        public static MasterLevelManager GetInstance()
        {
            return instance;
        }


        /// <summary>
        /// Create the exact given level
        /// </summary>
        /// <param name="levelID"></param>
        public void CreateLevel(int levelType, int levelID)
        {
            //Resolve selectedLevelStructure
            switch (levelType)
            {
                case 0:
                    selectedLevelStructure = EasyLevelStructure;
                    break;
                case 1:
                    selectedLevelStructure = NormalLevelStructure;
                    break;
                case 2:
                    selectedLevelStructure = HardLevelStructure;
                    break;
            }
            print("selectedLevelStructure: " + selectedLevelStructure);


            //Set correct alignment
            if (selectedLevelStructure[levelID].LevelAlignment == LevelStructure.Alignment.Right)
            {
                print("Using Right alignement");
                alignmentFactor = 1;

            }
            else if (selectedLevelStructure[levelID].LevelAlignment == LevelStructure.Alignment.Left)
            {
                print("Using Left alignement");
                alignmentFactor = -1;

            }
            else if (selectedLevelStructure[levelID].LevelAlignment == LevelStructure.Alignment.Random)
            {
                print("Using Random alignement");
                if (Random.value > 0.5f)
                    alignmentFactor = 1;
                else
                    alignmentFactor = -1;
            }
            //Set final values on level objects
            coneHolder.transform.localPosition = new Vector3(-14 * alignmentFactor, 0, -0.5f);
            goalLightHelper.transform.localPosition = new Vector3(14 * alignmentFactor, 0, -0.824f);
            goalLightHelper.transform.localScale = new Vector3(4.5f * alignmentFactor, 7, 0.001f);


            //activate coneHolder
            coneHolder.SetActive(true);

            //Ball
            Vector3 bPos = new Vector3(selectedLevelStructure[levelID].ballPosition.x * alignmentFactor, selectedLevelStructure[levelID].ballPosition.y, -0.5f);
            GameObject b = Instantiate(ball, bPos, Quaternion.Euler(0, 0, 0)) as GameObject;
            b.name = "Ball";

            //Player(s)
            for (int i = 0; i < selectedLevelStructure[levelID].playerUnits; i++)
            {
                GameObject pu = Instantiate(
                    player,
                    new Vector3(
                         selectedLevelStructure[levelID].playerUnitsPosition[i].x * alignmentFactor,
                         selectedLevelStructure[levelID].playerUnitsPosition[i].y,
                         -0.5f),
                    Quaternion.Euler(90, 0, 0)) as GameObject;
                pu.name = "PlayerUnit_" + (i + 1).ToString();
            }

            //opponent(s)
            for (int j = 0; j < selectedLevelStructure[levelID].OpponentUnits; j++)
            {
                GameObject ou = Instantiate(
                    opponent,
                    new Vector3(
                         selectedLevelStructure[levelID].OpponentUnitsPosition[j].x * alignmentFactor,
                         selectedLevelStructure[levelID].OpponentUnitsPosition[j].y,
                         -0.5f),
                    Quaternion.Euler(90, 0, 0)) as GameObject;
                ou.name = "OpponentUnit_" + (j + 1).ToString();
            }

            //1Up
            if (selectedLevelStructure[levelID].oneUp > 0)
            {
                Vector3 oneUpPos = new Vector3(selectedLevelStructure[levelID].oneUpPosition.x * alignmentFactor, selectedLevelStructure[levelID].oneUpPosition.y, -0.5f);
                GameObject oneUpGo = Instantiate(oneUp, oneUpPos, Quaternion.Euler(-90, 90, 90)) as GameObject;
                oneUpGo.name = "OneUp";
            }

            return;
        }


        /// <summary>
        /// CreateRandomEasyLevel
        /// </summary>
        public void CreateRandomEasyLevel()
        {
            //Random Direction
            int rndDir;
            if (Random.value > 0.5f)
                rndDir = 1;
            else
                rndDir = -1;

            //Set final values on level objects
            coneHolder.transform.localPosition = new Vector3(-14, 0, -0.5f);
            goalLightHelper.transform.localPosition = new Vector3(14, 0, -0.824f);
            goalLightHelper.transform.localScale = new Vector3(4.5f, 7, 0.001f);
            //activate coneHolder
            coneHolder.SetActive(true);

            //Player Position
            Vector3 pPos = new Vector3(Random.Range(-10f, -3f), Random.Range(-3f, 3f), -0.5f);
            //Ball Position
            Vector3 bPos = new Vector3(Random.Range(2.0f, 7f), Random.Range(-1f, 1f), -0.5f);
            //Opponent Position
            Vector3 oPos = new Vector3(Random.Range(2.0f, 12.0f), Random.Range(-3f, -5f) * rndDir, -0.5f);

            GameObject pu = Instantiate(player, pPos, Quaternion.Euler(90, 0, 0)) as GameObject;
            pu.name = "PlayerUnit";
            GameObject b = Instantiate(ball, bPos, Quaternion.Euler(0, 0, 0)) as GameObject;
            b.name = "Ball";
            GameObject o = Instantiate(opponent, oPos, Quaternion.Euler(90, 90, 90)) as GameObject;
            o.name = "OpponentUnit";

            return;
        }


        /// <summary>
        /// CreateRandomMediumLevel
        /// </summary>
        public void CreateRandomMediumLevel()
        {
            //Random Direction
            int rndDir;
            if (Random.value > 0.5f)
                rndDir = 1;
            else
                rndDir = -1;

            //Set final values on level objects
            coneHolder.transform.localPosition = new Vector3(-14, 0, -0.5f);
            goalLightHelper.transform.localPosition = new Vector3(14, 0, -0.824f);
            goalLightHelper.transform.localScale = new Vector3(4.5f, 7, 0.001f);
            //activate coneHolder
            coneHolder.SetActive(true);

            //Player Position
            Vector3 pPos = new Vector3(Random.Range(-10f, -5f), Random.Range(-3.5f, 3.5f), -0.5f);
            //Ball Position
            Vector3 bPos = new Vector3(Random.Range(3.0f, 8.0f), Random.Range(-2f, 2f), -0.5f);
            //Opponent Position
            Vector3 oPos = new Vector3(13f, Random.Range(-2f, -3.5f) * rndDir, -0.5f);

            GameObject pu = Instantiate(player, pPos, Quaternion.Euler(90, 0, 0)) as GameObject;
            pu.name = "PlayerUnit";
            GameObject b = Instantiate(ball, bPos, Quaternion.Euler(0, 0, 0)) as GameObject;
            b.name = "Ball";
            GameObject o = Instantiate(opponent, oPos, Quaternion.Euler(90, 90, 90)) as GameObject;
            o.name = "OpponentUnit";

            return;
        }


        /// <summary>
        /// CreateRandomSemiHardLevel
        /// </summary>
        public void CreateRandomSemiHardLevel()
        {
            //Random Direction
            int rndDir;
            if (Random.value > 0.5f)
                rndDir = 1;
            else
                rndDir = -1;

            //Set final values on level objects
            coneHolder.transform.localPosition = new Vector3(-14, 0, -0.5f);
            goalLightHelper.transform.localPosition = new Vector3(14, 0, -0.824f);
            goalLightHelper.transform.localScale = new Vector3(4.5f, 7, 0.001f);
            //activate coneHolder
            coneHolder.SetActive(true);

            //Player Position
            Vector3 pPos = new Vector3(Random.Range(-4.5f, -1.0f), Random.Range(-1.75f, 1.75f), -0.5f);
            //Ball Position
            Vector3 bPos = new Vector3(Random.Range(3.5f, 6.5f), Random.Range(-0.75f, 0.75f), -0.5f);
            //Opponent Position
            Vector3 oPos = new Vector3(Random.Range(9.0f, 13.0f), Random.Range(-4.5f, -3.5f) * rndDir, -0.5f);

            GameObject pu = Instantiate(player, pPos, Quaternion.Euler(90, 0, 0)) as GameObject;
            pu.name = "PlayerUnit";
            GameObject b = Instantiate(ball, bPos, Quaternion.Euler(90, 90, 90)) as GameObject;
            b.name = "Ball";
            GameObject o = Instantiate(opponent, oPos, Quaternion.Euler(90, 90, 90)) as GameObject;
            o.name = "OpponentUnit";
            GameObject o2 = Instantiate(opponent, oPos + new Vector3(0, 2.5f * rndDir, 0), Quaternion.Euler(90, 90, 90)) as GameObject;
            o2.name = "OpponentUnit-2";
        }


        /// <summary>
        /// CreateRandomHardLevel
        /// </summary>
        public void CreateRandomHardLevel()
        {
            //Random Direction
            int rndDir;
            if (Random.value > 0.5f)
                rndDir = 1;
            else
                rndDir = -1;

            //Set final values on level objects
            coneHolder.transform.localPosition = new Vector3(-14, 0, -0.5f);
            goalLightHelper.transform.localPosition = new Vector3(14, 0, -0.824f);
            goalLightHelper.transform.localScale = new Vector3(4.5f, 7, 0.001f);
            //activate coneHolder
            coneHolder.SetActive(true);

            //Player Position
            Vector3 pPos = new Vector3(Random.Range(-11.5f, -4.0f), Random.Range(-5f, 5f), -0.5f);
            //Ball Position
            Vector3 bPos = new Vector3(Random.Range(3.5f, 5.5f), Random.Range(-0.75f, 0.75f), -0.5f);
            //Opponent Position
            Vector3 oPos = new Vector3(Random.Range(8.0f, 13.0f), Random.Range(-5.0f, -4.5f) * rndDir, -0.5f);
            Vector3 oPos2 = new Vector3(Random.Range(7.0f, 13.0f), Random.Range(-3.0f, -2.0f) * rndDir * -1, -0.5f);

            GameObject pu = Instantiate(player, pPos, Quaternion.Euler(90, 0, 0)) as GameObject;
            pu.name = "PlayerUnit";
            GameObject b = Instantiate(ball, bPos, Quaternion.Euler(90, 90, 90)) as GameObject;
            b.name = "Ball";
            GameObject o = Instantiate(opponent, oPos, Quaternion.Euler(90, 90, 90)) as GameObject;
            o.name = "OpponentUnit";
            GameObject o2 = Instantiate(opponent, oPos + new Vector3(0, 2.5f * rndDir, 0), Quaternion.Euler(90, 90, 90)) as GameObject;
            o2.name = "OpponentUnit-2";
            GameObject o3 = Instantiate(opponent, oPos2, Quaternion.Euler(90, 90, 90)) as GameObject;
            o3.name = "OpponentUnit-3";
        }


        /// <summary>
        /// CreateRandomSuperHardLevel
        /// </summary>
        public void CreateRandomSuperHardLevel()
        {
            //Random Direction
            int rndDir;
            if (Random.value > 0.5f)
                rndDir = 1;
            else
                rndDir = -1;

            //Set final values on level objects
            coneHolder.transform.localPosition = new Vector3(-14, 0, -0.5f);
            goalLightHelper.transform.localPosition = new Vector3(14, 0, -0.824f);
            goalLightHelper.transform.localScale = new Vector3(4.5f, 7, 0.001f);
            //activate coneHolder
            coneHolder.SetActive(true);

            //Player Position
            Vector3 pPos = new Vector3(Random.Range(-11.5f, -8.0f), Random.Range(-4.0f, -1f), -0.5f);
            Vector3 pPos2 = new Vector3(Random.Range(-6.0f, -4.0f), Random.Range(1f, 4.0f), -0.5f);
            //Ball Position
            Vector3 bPos = new Vector3(Random.Range(3.0f, 5.5f), Random.Range(-1.25f, 1.25f), -0.5f);
            //Opponent Position
            Vector3 oPos = new Vector3(Random.Range(8.0f, 12.0f), Random.Range(-5.0f, -4.5f) * rndDir, -0.5f);
            Vector3 oPos2 = new Vector3(Random.Range(7.0f, 13.0f), Random.Range(-3.0f, -2.0f) * rndDir * -1, -0.5f);
            Vector3 oPos3 = new Vector3(Random.Range(-2.0f, 0.0f), Random.Range(-4.5f, -3.0f) * rndDir, -0.5f);

            GameObject pu = Instantiate(player, pPos, Quaternion.Euler(90, 0, 0)) as GameObject;
            pu.name = "PlayerUnit";

            //create second player shooter
            if (Random.value >= 0.4f)
            {
                GameObject pu2 = Instantiate(player, pPos2, Quaternion.Euler(90, 0, 0)) as GameObject;
                pu2.name = "PlayerUnit";
            }

            GameObject b = Instantiate(ball, bPos, Quaternion.Euler(90, 90, 90)) as GameObject;
            b.name = "Ball";
            GameObject o = Instantiate(opponent, oPos, Quaternion.Euler(90, 90, 90)) as GameObject;
            o.name = "OpponentUnit";
            GameObject o2 = Instantiate(opponent, oPos + new Vector3(1 * rndDir, 3.0f * rndDir, 0), Quaternion.Euler(90, 90, 90)) as GameObject;
            o2.name = "OpponentUnit-2";
            GameObject o3 = Instantiate(opponent, oPos2, Quaternion.Euler(90, 90, 90)) as GameObject;
            o3.name = "OpponentUnit-3";

            //create 4th opponent
            if (Random.value >= 0.5f)
            {
                GameObject o4 = Instantiate(opponent, oPos3, Quaternion.Euler(90, 90, 90)) as GameObject;
                o4.name = "OpponentUnit-4";
            }
        }


        /// <summary>
        /// CreateRandomInsaneLevel
        /// </summary>
        public void CreateRandomInsaneLevel()
        {
            //Random Direction
            int rndDir;
            if (Random.value > 0.5f)
                rndDir = 1;
            else
                rndDir = -1;

            //Set final values on level objects
            coneHolder.transform.localPosition = new Vector3(-14, 0, -0.5f);
            goalLightHelper.transform.localPosition = new Vector3(14, 0, -0.824f);
            goalLightHelper.transform.localScale = new Vector3(4.5f, 7, 0.001f);
            //activate coneHolder
            coneHolder.SetActive(true);

            //Player Position
            Vector3 pPos = new Vector3(Random.Range(-12.0f, -6.0f), Random.Range(-5.0f, -1f), -0.5f);
            Vector3 pPos2 = new Vector3(Random.Range(-12.0f, -6.0f), Random.Range(1f, 5.0f), -0.5f);

            //Ball Position
            Vector3 bPos = new Vector3(Random.Range(4.0f, 5.5f), Random.Range(-2.25f, 2.25f), -0.5f);
            //Opponent Position
            Vector3 oPos = new Vector3(Random.Range(9.0f, 12.0f), Random.Range(-5.0f, -4.5f) * rndDir, -0.5f);
            Vector3 oPos2 = new Vector3(Random.Range(7.0f, 9.0f), Random.Range(-3.0f, -2.0f) * rndDir * -1, -0.5f);
            Vector3 oPos3 = new Vector3(Random.Range(-4.0f, 2.0f), Random.Range(-4.5f, -3.0f) * rndDir, -0.5f);
            Vector3 oPos4 = new Vector3(Random.Range(-4.0f, 2.0f), Random.Range(-4.5f, -3.0f) * rndDir * -1, -0.5f);

            GameObject pu = Instantiate(player, pPos, Quaternion.Euler(90, 0, 0)) as GameObject;
            pu.name = "PlayerUnit";

            //create second player shooter
            if (Random.value >= 0.15f)
            {
                GameObject pu2 = Instantiate(player, pPos2, Quaternion.Euler(90, 0, 0)) as GameObject;
                pu2.name = "PlayerUnit";
            }

            GameObject b = Instantiate(ball, bPos, Quaternion.Euler(90, 90, 90)) as GameObject;
            b.name = "Ball";
            GameObject o = Instantiate(opponent, oPos, Quaternion.Euler(90, 90, 90)) as GameObject;
            o.name = "OpponentUnit";
            GameObject o2 = Instantiate(opponent, oPos + new Vector3(1 * rndDir, 3.0f * rndDir, 0), Quaternion.Euler(90, 90, 90)) as GameObject;
            o2.name = "OpponentUnit-2";
            GameObject o3 = Instantiate(opponent, oPos2, Quaternion.Euler(90, 90, 90)) as GameObject;
            o3.name = "OpponentUnit-3";

            //create 4th opponent
            if (Random.value >= 0.2f)
            {
                GameObject o4 = Instantiate(opponent, oPos3, Quaternion.Euler(90, 90, 90)) as GameObject;
                o4.name = "OpponentUnit-4";

            }

            //create 5th opponent
            if (Random.value >= 0.3f)
            {
                GameObject o5 = Instantiate(opponent, oPos4 + new Vector3(3 * rndDir, 2 * rndDir, 0), Quaternion.Euler(90, 90, 90)) as GameObject;
                o5.name = "OpponentUnit-5";
            }
        }

    }

}