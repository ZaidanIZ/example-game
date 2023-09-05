using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrickshotArena
{
	public class MusicPlayer : MonoBehaviour
	{
		/// <summary>
		/// This class plays a starting music file and then loops another one when the first one is over.
		/// </summary>

		public AudioClip main;
		public AudioClip loop;

		void Awake()
		{
			DontDestroyOnLoad(this);
		}

		void Start()
		{
			playSfx(main);
		}

		void Update()
		{
			if (!GetComponent<AudioSource>().isPlaying)
			{
				playSfx(loop);
			}
		}

		void playSfx(AudioClip _clip)
		{
			GetComponent<AudioSource>().clip = _clip;
			GetComponent<AudioSource>().Play();
		}

	}
}