using UnityEngine;
using System.Collections;

namespace TrickshotArena
{
	public class BallManager : MonoBehaviour
	{
		/// <summary>
		/// Main Ball Manager.
		/// This class controls ball collision with Goal triggers and gatePoles, 
		/// and also stops the ball when the speed smaller than a minimum.
		/// </summary>

		public int rotationSpeed = 12;      //rotation speed after each shoot/hit
		public GameObject trail;            //trail object
		public GameObject shadow;           //shadow object
		internal bool isUntouched;          //flag to make the ball as fresh (has not been shooted yet)

		private GameObject gameController;  //Reference to main game controller
		public AudioClip ballHitPost;       //Sfx for hitting the poles
		public AudioClip ballHit1Up;

		private Vector3 shootCollisionPoint;       //the exact position on ball where the shoot happened. used for the curved shot.

		void Awake()
		{
			shootCollisionPoint = new Vector3(0, 0, 0);
			gameController = GameObject.FindGameObjectWithTag("GameController");
		}

		void Start()
		{
			//bounce unit scale from 0 to 1
			StartCoroutine(scaleAnimator(1));

			isUntouched = true;

			GetComponent<Rigidbody>().drag = 0.7f;
			GetComponent<Rigidbody>().mass = 0.5f;
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
				startingScale = new Vector3(0.1f, 0.1f, 0.1f);
				targetScale = transform.localScale;
			}
			else if (dir == -1)
			{
				startingScale = transform.localScale;
				targetScale = new Vector3(0.1f, 0.1f, 0.1f);

				//hide trail
				if (trail)
					trail.GetComponent<TrailRenderer>().enabled = false;
				if (GetComponent<TrailRenderer>())
					GetComponent<TrailRenderer>().enabled = false;
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
				{
					this.gameObject.tag = "Untagged";
					Destroy(gameObject, 3);
					this.gameObject.GetComponent<SphereCollider>().enabled = false;
					GetComponent<Renderer>().enabled = false;
					shadow.GetComponent<Renderer>().enabled = false;
				}
			}
		}


		void FixedUpdate()
		{
			manageBallFriction();
		}

		void LateUpdate()
		{
			transform.position = new Vector3(transform.position.x, transform.position.y, -0.5f);
			shadow.transform.eulerAngles = new Vector3(0, 180, 0);
			shadow.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.4f);
		}

		/// <summary>
		/// Monitor ball's speed at all times.
		/// </summary>
		private float ballSpeed;
		void manageBallFriction()
		{
			ballSpeed = GetComponent<Rigidbody>().velocity.magnitude;
			//print("Ball Speed: " + ballSpeed);

			if (ballSpeed < 2.5f && !isUntouched)
			{
				//forcestop the ball
				GetComponent<Rigidbody>().drag = 2;
			}

			if (GlobalGameManager.goalHappened && !isUntouched)
			{
				GetComponent<Rigidbody>().drag = 10;
				GetComponent<Rigidbody>().mass = 10;
			}
		}


		/// <summary>
		/// Manage ball's collision with the world
		/// </summary>
		/// <param name="other"></param>
		void OnCollisionEnter(Collision other)
		{
			switch (other.gameObject.tag)
			{
				case "gatePost":
					playSfx(ballHitPost);
					break;
				case "Player":
				case "Opponent":
					//set the touch flag on ball
					isUntouched = false;
					//save collision point
					shootCollisionPoint = other.contacts[0].point;
					print("shootCollisionPoint: " + shootCollisionPoint);
					//give fake rotation to ball
					StartCoroutine(fakeRotation());
					break;
			}
		}


		/// <summary>
		/// if ball hits the gate...
		/// </summary>
		/// <param name="other"></param>
		void OnTriggerEnter(Collider other)
		{
			switch (other.gameObject.tag)
			{
				case "opponentGoalTrigger":
                    StartCoroutine(gameController.GetComponent<GlobalGameManager>().managePostGoal("Player"));
                    break;
			}
			Randomize.randomizee.Randomz();
		}


		/// <summary>
		/// Create a non physical fake rotation for the ball everytime it gets shot shot by the player
		/// </summary>
		bool frFlag = false;
		IEnumerator fakeRotation()
		{
			if (frFlag)
				yield break;

			frFlag = true;

			float t = 0;
			while (t < 1)
			{
				t += Time.deltaTime * 0.2f;
				transform.Rotate(new Vector3(0, rotationSpeed - (t * rotationSpeed), 0));
				yield return 0;
			}

			if (t >= 1)
			{
				frFlag = false;
			}
		}


		/// <summary>
		/// Play sound clips
		/// </summary>
		/// <param name="_clip"></param>
		void playSfx(AudioClip _clip)
		{
			GetComponent<AudioSource>().clip = _clip;
			if (!GetComponent<AudioSource>().isPlaying)
			{
				GetComponent<AudioSource>().Play();
			}
		}

	}
}