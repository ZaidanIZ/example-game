using UnityEngine;
using System.Collections;

///Developed by Indie Studio
///https://www.assetstore.unity3d.com/en/#!/publisher/9268
///www.indiestd.com
///info@indiestd.com
///copyright © 2016 IGS. All rights reserved.
[DisallowMultipleComponent]
public class StarsEffect : MonoBehaviour
{
		/// <summary>
		/// The position of Stars Effect in the World Space.
		/// </summary>
		private Vector3 tempPosition;

		/// <summary>
		/// The stars effect prefab.
		/// </summary>
		public GameObject starsEffectPrefab;

		/// <summary>
		/// The star effect Z position.
		/// </summary>
		[Range(-50,50)]
		public float starEffectZPosition = -5;

		/// <summary>
		/// The angle of stars effect.
		/// </summary>
		private Vector3 angle = new Vector3 (0, 180, 0);

		/// <summary>
		/// Create the stars effect.
		/// </summary>
		public void CreateStarsEffect ()
		{
				tempPosition = transform.position;
				tempPosition.z = starEffectZPosition;
				GameObject starsEffect = Instantiate (starsEffectPrefab, tempPosition, Quaternion.Euler(angle)) as GameObject;
				starsEffect.transform.parent = transform;//setting up Stars Effect Parent
		}
}