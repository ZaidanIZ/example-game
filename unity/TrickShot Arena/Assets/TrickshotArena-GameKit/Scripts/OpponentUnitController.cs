using UnityEngine;
using System.Collections;

namespace TrickshotArena
{
	public class OpponentUnitController : MonoBehaviour
	{
		/// <summary>
		/// Opponents do nothing much on their own. They are just in game to make the game (shooting) harder for the player
		/// by blocking the direct path towards the gate.
		/// </summary>

		public AudioClip unitsBallHit;          //units hits the ball sfx
		public AudioClip unitsBorderHit;        //units hits the borders sfx

		IEnumerator Start()
		{
			//bounce unit scale from 0 to 1
			StartCoroutine(scaleAnimator(1));
			yield return new WaitForSeconds(2f);
		}

		/// <summary>
		/// We use this method to change the unit size once the level is about to begin/end.
		/// </summary>
		/// <param name="dir"></param>
		/// <returns></returns>
		public IEnumerator scaleAnimator(int dir)
		{

			Vector3 startingScale = new Vector3();
			Vector3 targetScale = new Vector3();

			if (dir == 1)
			{
				startingScale = new Vector3(0.1f, 0.5f, 0.1f);
				targetScale = transform.localScale;
			}
			else if (dir == -1)
			{
				startingScale = transform.localScale;
				targetScale = new Vector3(0.1f, 0.5f, 0.1f);
			}

			transform.localScale = startingScale;
			float t = 0;
			while (t < 1)
			{
				t += Time.deltaTime * 1.0f;
				transform.localScale = new Vector3(Mathf.SmoothStep(startingScale.x, targetScale.x, t),
												   Mathf.SmoothStep(startingScale.y, targetScale.y, t),
												   Mathf.SmoothStep(startingScale.z, targetScale.z, t));
				yield return 0;
			}

			if (t >= 1)
			{
				if (dir == -1)
					Destroy(gameObject);
			}
		}

		/// <summary>
		/// Collision of opponents with ball & borders
		/// </summary>
		/// <param name="other"></param>
		void OnCollisionEnter(Collision other)
		{
			switch (other.gameObject.tag)
			{
				case "ball":
					PlaySfx(unitsBallHit);
					break;
				case "Border":
					PlaySfx(unitsBorderHit);
					break;
			}
		}

		/// <summary>
		/// Play sound clips
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