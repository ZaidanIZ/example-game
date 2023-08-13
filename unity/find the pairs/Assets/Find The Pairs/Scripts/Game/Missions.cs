using UnityEngine;
using System.Collections;

///Developed by Indie Studio
///https://www.assetstore.unity3d.com/en/#!/publisher/9268
///www.indiestd.com
///info@indiestd.com
///copyright © 2016 IGS. All rights reserved.
[RequireComponent(typeof(MissionCreator))]
[DisallowMultipleComponent]
public class Missions : MonoBehaviour
{
		// Use this for initialization
		void Awake ()
		{
				DataManager.instance.InitGameData ();
		}
}
