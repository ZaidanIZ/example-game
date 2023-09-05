#pragma warning disable 0414

using System.Collections;
using System.Collections.Generic;
using TrickshotArena;
using UnityEngine;

namespace TrickshotArena
{
	public class GoalLightFx : MonoBehaviour
	{
		/// <summary>
		/// We need to use this object to highlight the active gate , so player can get to know the gate he needs to
		/// shoot the ball and score a goal.
		/// </summary>

		private Vector3 startingScale;
		private Vector3 targetScale;
		private Color startingColor;
		private Color targetColor;
		private bool canGlow;

		void Start()
		{
			canGlow = true;
			GetComponent<Renderer>().enabled = false;
			startingColor = new Color(1, 1, 1, 0);
			targetColor = new Color(1, 1, 1, 1);
			GetComponent<Renderer>().material.color = startingColor;
			startingScale = new Vector3(4.5f, 7f, 0.001f);
			targetScale = startingScale * 1.2f;
			transform.localScale = startingScale;
		}


		void Update()
		{
			//Only show the gate helper if the game is started & player is able to shoot
			if (GlobalGameManager.gameIsStarted && playerController.canShoot)
			{
				GetComponent<Renderer>().enabled = true;
			}
			else
				GetComponent<Renderer>().enabled = false;

			if (canGlow)
				StartCoroutine(glow());
		}


		/// <summary>
		/// We change the opacity of the helper object to make it look like a glowing helper!
		/// </summary>
		/// <returns></returns>
		IEnumerator glow()
		{
			canGlow = false;

			float t = 0;
			float t2 = 0;

			while (t < 1)
			{
				t += Time.deltaTime * 1.1f;
				GetComponent<Renderer>().material.color = new Color(1, 1, 1, Mathf.SmoothStep(startingColor.a, targetColor.a, t));
				yield return 0;
			}

			if (t >= 1)
			{
				while (t2 < 1)
				{
					t2 += Time.deltaTime * 1.1f;
					GetComponent<Renderer>().material.color = new Color(1, 1, 1, Mathf.SmoothStep(targetColor.a, startingColor.a, t2));
					yield return 0;
				}
			}

			if (t2 >= 1)
				canGlow = true;
		}

	}
}