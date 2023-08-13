using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

///Developed by Indie Studio
///https://www.assetstore.unity3d.com/en/#!/publisher/9268
///www.indiestd.com
///info@indiestd.com
///copyright © 2016 IGS. All rights reserved.
[DisallowMultipleComponent]
public class LevelsManager : MonoBehaviour
{
		/// <summary>
		/// The initial delay before play.
		/// </summary>
		public float initialDelay = 3;

		/// <summary>
		/// The delay between cells.
		/// </summary>
		public float delayBetweenCells = 0.1f;

		/// <summary>
		/// The size of the cell in the grid.
		/// </summary>
		public Vector2 cellSize = new Vector2 (150, 150);

		/// <summary>
		/// The spacing between cells.
		/// </summary>
		public Vector2 spacing = new Vector2 (5, 5);

		/// <summary>
		/// Whether to make random shuffle on grid build or not.
		/// </summary>
		public bool randomShuffleOnBuild;

		/// <summary>
		/// Whether to match the grid or not
		/// </summary>
		public bool matchGrid = true;

		/// <summary>
		/// Whether to make the mission as one level
		/// Doing that will skip the levels scene and go directly into Game scene with one level (first level)
		/// </summary>
		public bool singleLevel;

		/// <summary>
		/// The default normal sprite for the pair.
		/// </summary>
		public Sprite defaultNormalSprite;

		/// <summary>
		/// The default onclick sprite for the pair.
		/// </summary>
		public Sprite defaultOnClickSprite;

		/// <summary>
		/// The default background sprite for the pair.
		/// </summary>
		public Sprite defaultBackgroundSprite;

		/// <summary>
		/// The rows limit.
		/// </summary>
		public readonly static int rowsLimit = 12;

		/// <summary>
		/// The cols limit.
		/// </summary>
		public readonly static int colsLimit = 12;

		/// <summary>
		/// The levels list.
		/// </summary>
		public List<Level> levels = new List<Level> ();
}