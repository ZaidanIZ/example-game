#pragma warning disable 0219

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TrickshotArena
{
	public class playerController : MonoBehaviour
	{
		/// <summary>
		/// This is the main player controller class.
		/// We use this to let the player select/unselect & drag a unit, shoot, and see the player debug informations in the editor.
		/// </summary>

		//Public Variables
		public GameObject selectionCircle;  //Reference to gameObject

		//Referenced GameObjects
		private GameObject helperBegin;     //Start helper
		private GameObject helperEnd;       //End Helper
		private GameObject arrowPlane;      //arrow plane which is used to show shotPower
		private GameObject shootCircle;     //shoot Circle plane which is used to show shotPower

		private GameObject gameController;  //Reference to main game controller
		private float currentDistance;      //real distance of our touch/mouse position from initial drag position
		private float safeDistance;         //A safe distance value which is always between min and max to avoid supershoots

		private float pwr;                  //shoot power

		//this vector holds shooting direction
		private Vector3 shootDirectionVector;

		//prevent player to shoot twice in a round
		public static bool canShoot;
		public static bool showPlayerGlow;  //show a circle aroud the player unit if it is selectable/interactable

		/// <summary>
		/// Init
		/// </summary>
		void Awake()
		{
			//Find and cache important gameObjects
			helperBegin = GameObject.FindGameObjectWithTag("mouseHelperBegin");
			helperEnd = GameObject.FindGameObjectWithTag("mouseHelperEnd");
			arrowPlane = GameObject.FindGameObjectWithTag("helperArrow");
			gameController = GameObject.FindGameObjectWithTag("GameController");
			shootCircle = GameObject.FindGameObjectWithTag("shootCircle");

			//Init Variables
			pwr = 0.1f;
			currentDistance = 0;
			shootDirectionVector = new Vector3(0, 0, 0);
			canShoot = false;

			showPlayerGlow = true;
			arrowPlane.GetComponent<Renderer>().enabled = false; //hide arrowPlane
			shootCircle.GetComponent<Renderer>().enabled = false; //hide shoot Circle
		}

		void Start()
		{
			StartCoroutine(scaleAnimator(1));
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
		/// FSM
		/// </summary>
		void Update()
		{
			//Active the selection circles around Player units when they have the turn.
			if (gameObject.tag == "Player" && !GlobalGameManager.goalHappened && canShoot && showPlayerGlow)
				selectionCircle.GetComponent<Renderer>().enabled = true;

			if (!canShoot || !showPlayerGlow)
				selectionCircle.GetComponent<Renderer>().enabled = false;

			if (GlobalGameManager.goalHappened)
				GetComponent<Rigidbody>().drag = 2.0f;
			else
				GetComponent<Rigidbody>().drag = 0.7f;
		}

		/// <summary>
		/// Shoot calculations incase player selects and drag him mouse/touch on a unit...
		/// </summary>
		void OnMouseDrag()
		{
			if (canShoot && gameObject.tag == "Player")
			{
				//print("Draged");
				currentDistance = Vector3.Distance(helperBegin.transform.position, transform.position);

				//limiters
				if (currentDistance <= GlobalGameManager.maxDistance)
					safeDistance = currentDistance;
				else
					safeDistance = GlobalGameManager.maxDistance;

				pwr = Mathf.Abs(safeDistance) * 12; //this is very important. change with extreme caution. (default = 12)

				//show the power arrow above the unit and scale is accordingly.
				manageArrowTransform();

				//position of helperEnd
				//HelperEnd is the exact opposite (mirrored) version of our helperBegin object 
				//and help us to calculate debug vectors and lines for a perfect shoot.
				//Please refer to the basic geometry references of your choice to understand the math.
				Vector3 dxy = helperBegin.transform.position - transform.position;
				float diff = dxy.magnitude;
				helperEnd.transform.position = transform.position + ((dxy / diff) * currentDistance * -1);

				helperEnd.transform.position = new Vector3(helperEnd.transform.position.x,
															helperEnd.transform.position.y,
															-0.5f);

				//debug line from initial position to our current touch position
				//Debug.DrawLine(transform.position, helperBegin.transform.position, Color.red);
				//debug line from initial position to maximum power position (mirrored)
				//Debug.DrawLine(transform.position, arrowPlane.transform.position, Color.blue);
				//debug line from initial position to the exact opposite position (mirrored) of our current touch position
				//Debug.DrawLine(transform.position, (2 * transform.position) - helperBegin.transform.position, Color.yellow);

				//cast ray forward and collect informations
				castRay();

				//final vector used to shoot the unit.
				shootDirectionVector = Vector3.Normalize(helperBegin.transform.position - transform.position);
				//print(shootDirectionVector);
			}
		}

		/// <summary>
		/// Cast a ray forward and collect informations like if it hits anything...
		/// </summary>
		private RaycastHit hitInfo;
		private Ray ray;
		void castRay()
		{
			//cast the ray from units position with a normalized direction out of it which is mirrored to our current drag vector.
			ray = new Ray(transform.position, (helperEnd.transform.position - transform.position).normalized);

			if (Physics.Raycast(ray, out hitInfo, currentDistance * 5))
			{
				GameObject objectHit = hitInfo.transform.gameObject;

				//debug line whenever the ray hits something.
				Debug.DrawLine(ray.origin, hitInfo.point, Color.cyan);

				//new algorithm for reflection vector
				//accurate X & Y
				float Ydiff = Mathf.Abs(transform.position.y - objectHit.transform.position.y);
				float Xdiff = Mathf.Abs(transform.position.x - objectHit.transform.position.x);
				//normal continue
				Vector3 normalContinue = objectHit.transform.position - transform.position;
				Debug.DrawRay(hitInfo.transform.position, normalContinue, Color.blue, 0.05f);
				//Normal reflect
				Vector3 normalReflect = hitInfo.normal * -normalContinue.magnitude;
				Debug.DrawRay(hitInfo.transform.position, normalReflect, Color.red, 0.05f);

				//print ("Ydiff: " + Ydiff + "\n" + "Xdiff: " + Xdiff);
				Debug.DrawRay(hitInfo.transform.position, (normalContinue * (1.5f + Ydiff / 5)) + (normalReflect * (1.0f + Xdiff / 10)), Color.yellow, 0.05f);

				//Debug
				//print("Ray hits: " + objectHit.name + " At " + Time.time + " And Reflection is: " + reflectedVector);
			}
		}

		/// <summary>
		/// Unhide and process the transform and scale of the power Arrow object
		/// </summary>
		void manageArrowTransform()
		{
			//print("currentDistance: " + currentDistance);

			if (currentDistance < 0.9f)
			{
				arrowPlane.GetComponent<Renderer>().enabled = false;
				shootCircle.GetComponent<Renderer>().enabled = false;
				showPlayerGlow = true;
				return;
			}

			//unhide arrowPlane
			arrowPlane.GetComponent<Renderer>().enabled = true;
			shootCircle.GetComponent<Renderer>().enabled = true;
			//hide player glow
			showPlayerGlow = false;

			//calculate position
			if (currentDistance <= GlobalGameManager.maxDistance)
			{
				arrowPlane.transform.position = transform.position + new Vector3(0, 0, 0.03f);
			}
			else
			{
				Vector3 dxy = helperBegin.transform.position - transform.position;
				float diff = dxy.magnitude;
				arrowPlane.transform.position = transform.position + new Vector3(0, 0, 0.03f);
			}

			shootCircle.transform.position = transform.position + new Vector3(0, 0, 0.05f);

			//calculate rotation
			Vector3 dir = helperBegin.transform.position - transform.position;
			float outRotation; // between 0 - 360

			if (Vector3.Angle(dir, transform.forward) > 90)
				outRotation = Vector3.Angle(dir, transform.right);
			else
				outRotation = Vector3.Angle(dir, transform.right) * -1;

			arrowPlane.transform.eulerAngles = new Vector3(0, 0, outRotation);

			//calculate scale
			float scaleCoefX = Mathf.Log(0.4f + safeDistance / 2f, 1.6f) * 1.5f;
			float scaleCoefY = Mathf.Log(0.4f + safeDistance / 2f, 1.6f) * 1.5f;

			arrowPlane.transform.localScale = new Vector3(1 + scaleCoefX * 3.5f, 1 + scaleCoefY * 3.5f, 0.001f); //default scale
			shootCircle.transform.localScale = new Vector3(1 + scaleCoefX * 3.5f, 1 + scaleCoefY * 3.5f, 0.001f); //default scale
		}


		/// <summary>
		/// Main shoot event incase player release the touch/mouse input
		/// </summary>
		void OnMouseUp()
		{
			//give the player a second chance to choose another ball if drag on the unit is too low
			//print("currentDistance: " + currentDistance);
			if (currentDistance < 0.75f)
			{
				arrowPlane.GetComponent<Renderer>().enabled = false;
				shootCircle.GetComponent<Renderer>().enabled = false;
				return;
			}

			//But if player wants to shoot anyway:
			//prevent double shooting in a round
			if (!canShoot)
				return;

			//no more shooting is possible	
			canShoot = false;
			GlobalGameManager.shootHappened = true;

			//keep track of elapsed time after letting the ball go, 
			//so we can findout if ball has stopped and the round should be changed
			//this is the time which user released the button and shooted the ball
			GlobalGameManager.shootTime = Time.time;
			//print ("PlayerShootTime: " + GlobalGameManager.shootTime);
			StartCoroutine(gameController.GetComponent<GlobalGameManager>().checkGameover());

			//hide helper arrow object
			arrowPlane.GetComponent<Renderer>().enabled = false;
			shootCircle.GetComponent<Renderer>().enabled = false;

			//do the physics calculations and shoot the ball 
			Vector3 outPower = shootDirectionVector * pwr * -1.1f;

			//Bug fix. Avoid shoot powers over 45, or the ball might fly off the level bounds.
			//Introduced in version 1.5+
			if (outPower.magnitude >= 45)
				outPower *= 0.9f;

			//Special case. We might need to slow down the disks to get a better/sharper/more-accurate shoots.
			//This is not mandatory, but helps with the realism of the physics.
			if (outPower.magnitude <= 12)
			{
				outPower *= 0.75f;
			}

			//always make the player to move only in x-y plane and not on the z direction
			//print("shoot power: " + outPower.magnitude);		
			GetComponent<Rigidbody>().AddForce(outPower, ForceMode.Impulse);

			//fixColliderSize();
		}

		/// <summary>
		/// Set the collider size as default value.
		/// </summary>
		private bool sizeIsFixed = false;
		void fixColliderSize()
		{
			if (sizeIsFixed)
				return;
			sizeIsFixed = true;
			GetComponent<SphereCollider>().radius = 0.5f;
		}

	}
}