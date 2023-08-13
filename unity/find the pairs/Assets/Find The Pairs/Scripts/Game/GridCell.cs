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
public class GridCell : MonoBehaviour
{       
		/// <summary>
		/// The index of the GridCell in the Grid.
		/// </summary>
		public int index;

		/// <summary>
		/// The pair ID.
		/// </summary>
		[HideInInspector]
		public int pairID;

		/// <summary>
		/// Whether the Grid Cell already used or not.
		/// </summary>
		public bool alreadyUsed;

		void Start ()
		{
			GetComponent<Button>().onClick.AddListener(() => GameObject.FindObjectOfType<UIEvents>().GridCellButtonEvent(this));
		}
}