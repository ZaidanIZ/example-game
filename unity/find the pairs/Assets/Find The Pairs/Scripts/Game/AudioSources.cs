using UnityEngine;
using System.Collections;

///Developed by Indie Studio
///https://www.assetstore.unity3d.com/en/#!/publisher/9268
///www.indiestd.com
///info@indiestd.com
///copyright © 2016 IGS. All rights reserved.
public class AudioSources : MonoBehaviour {

	/// <summary>
	/// The wanter bubble sound.
	/// </summary>
	public AudioClip waterBubbleSound;

	[HideInInspector]
	public AudioSource[] audioSources;

	/// <summary>
	/// The audio sources instance.
	/// </summary>
	public static AudioSources instance;
	
	// Use this for initialization
	void Awake ()
	{
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad(gameObject);
		} else {
			Destroy (gameObject);
		}
		audioSources = GetComponents<AudioSource> ();
	}

	public void PlayWaterBubbleSound ()
	{
		audioSources [1].clip = waterBubbleSound;
		audioSources [1].Play ();
	}
}
