using UnityEngine;
using System.Collections;

///Developed by Indie Studio
///https://www.assetstore.unity3d.com/en/#!/publisher/9268
///www.indiestd.com
///info@indiestd.com
///copyright © 2016 IGS. All rights reserved.
public class GameObjectUtil
{
		/// <summary>
		/// Finds the child by tag.
		/// </summary>
		/// <returns>The child by tag.</returns>
		/// <param name="p">parent.</param>
		/// <param name="childTag">Child tag.</param>
		public static Transform FindChildByTag (Transform theParent, string childTag)
		{
				if (string.IsNullOrEmpty (childTag) || theParent == null) {
						return null;
				}

				foreach (Transform child in theParent) {
						if (child.tag == childTag) {
							return child;
						}
				}

				return null;
		}
}
