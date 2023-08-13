using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

///Developed by Indie Studio
///https://www.assetstore.unity3d.com/en/#!/publisher/9268
///www.indiestd.com
///info@indiestd.com
///copyright © 2016 IGS. All rights reserved.

/// <summary>
/// A level used with LevelsManager Component.
/// When you create a new level using Inspector ,you will create an instace of this class
/// </summary>
[System.Serializable]
public class Level
{
		/// <summary>
		/// Whether the level is visible
		/// </summary>
		public bool showLevel;

		[HideInInspector]
		/// <summary>
		/// The previous number of rows.
		/// </summary>
		public int previousNumberOfRows = -1;

		[HideInInspector]
		/// <summary>
		/// The previous number of cols.
		/// </summary>
		public int previousNumberOfCols = -1;

		/// <summary>
		/// The number of columns.
		/// </summary>
		public int numberOfColumns = 4;
		
		/// <summary>
		/// The number of rows.
		/// </summary>
		public int numberOfRows = 4;


		/// <summary>
		/// The time limit of the level.
		/// </summary>
		public int timeLimit = 60;

		/// <summary>
		/// The three stars time period.
		/// </summary>
		public int threeStarsTimePeriod = 30;

		/// <summary>
		/// The three stars time period.
		/// </summary>
		public int twoStarsTimePeriod = 15;

		/// <summary>
		/// The pairs list.
		/// </summary>
		public List<Pair> pairs = new List<Pair> ();

		/// <summary>
		/// Pair Class.
		/// </summary>
		[System.Serializable]
		public class Pair
		{
				/// <summary>
				/// Whether the pair is visible(used with inspector only).
				/// </summary>
				public bool showPair = true;

				/// <summary>
				/// The background sprite.
				/// </summary>
				public Sprite backgroundSprite;

				/// <summary>
				/// The normal sprite of the pair(default).
				/// </summary>
				public Sprite normalSprite;

				/// <summary>
				/// The sprite of the pair (onclick).
				/// </summary>
				public Sprite onClickSprite;

				/// <summary>
				/// The first element of the pair.
				/// </summary>
				public Element firstElement = new Element ();

				/// <summary>
				/// The second element of the pair.
				/// </summary>
				public Element secondElement = new Element ();
		}

		/// <summary>
		/// Element Class.
		/// </summary>
		[System.Serializable]
		public class Element
		{
				/// <summary>
				/// The index of the element in the Grid.
				/// </summary>
				public int index;
		}
}