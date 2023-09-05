using UnityEngine;
using System.Collections;

namespace TrickshotArena
{
	public class playerColliderManager : MonoBehaviour
	{
		/// <summary>
		/// Controller for collision of player units vs other items in the scene like ball or opponent units
		/// </summary>

		public AudioClip unitsBallHit;      //units hits the ball sfx
		public AudioClip unitsGeneralHit;   //units general hit sfx (Not used)
		public AudioClip unitsBorderHit;    //units hits the border sfx

		void OnCollisionEnter(Collision other)
		{
			switch (other.gameObject.tag)
			{
				case "Opponent":
					PlaySfx(unitsGeneralHit);
					break;
				case "Player":
					PlaySfx(unitsGeneralHit);
					break;
				case "ball":
					PlaySfx(unitsBallHit);
					StartCoroutine(fixColliderSize());
					break;
				case "Border":
					PlaySfx(unitsBorderHit);
					break;
			}
		}

		IEnumerator fixColliderSize()
		{
			yield return new WaitForSeconds(0.05f);
			//GetComponent<SphereCollider>().radius = 0.5f;
		}

		/// <summary>
		/// Play the given audioclip
		/// </summary>
		/// <param name="_clip"></param>
		void PlaySfx(AudioClip _clip)
		{
			GetComponent<AudioSource>().clip = _clip;
			if (!GetComponent<AudioSource>().isPlaying)
			{
				GetComponent<AudioSource>().Play();
			}
		}
	}
}