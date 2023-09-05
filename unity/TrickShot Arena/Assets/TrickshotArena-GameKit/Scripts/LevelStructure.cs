using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrickshotArena
{
    [System.Serializable]
    public class LevelStructure
    {
        /// <summary>
        /// We need to use this class to be able to create new level setups for the game.
        /// Each instance of this class will let us define a new level setup via "MasterLevelManager" object.
        /// </summary>

        public enum Alignment { Random, Right, Left }
        public Alignment LevelAlignment = Alignment.Random;
        public int playerUnits;
        public int OpponentUnits;
        public int oneUp;
        [Space(5)]
        public Vector2 ballPosition;
        public Vector2 oneUpPosition;
        public Vector2[] playerUnitsPosition;
        public Vector2[] OpponentUnitsPosition;
        // ^^ We just need the X-Y. Z is always fixed on -0.5f
    }
}