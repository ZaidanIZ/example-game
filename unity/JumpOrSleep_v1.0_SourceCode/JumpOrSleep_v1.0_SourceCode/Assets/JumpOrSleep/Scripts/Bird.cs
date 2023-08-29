using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class Bird : MonoBehaviour
{
	/// <summary>
	///gravity for jump
	/// </summary>
	public float gravity = -.1f;
	/// <summary>
	///speed for jump
	/// </summary>
	float jumpSpeed;
	/// <summary>
	///lists that hold tags and layers object can collide with that affect jump
	/// </summary>
	public List<string> collidingTagList;
	public LayerMask collidingLayerMask;
	/// <summary>
	///current gravity of bird
	/// </summary>
	private float currentGravity = 0;
	/// <summary>
	///move direction for jumping
	/// </summary>
	private Vector2 moveDirection = Vector2.zero;
	private Vector2 jumpDirection = Vector2.zero;

	/// <summary>
	///check the bird is ground or no?
	/// </summary>
	public bool IsGrounded { get; private set; }

	/// <summary>
	///check the bird is in air or no
	/// </summary>
	public bool InAir { get; private set; }

	/// <summary>
	///the point where bird hit ground
	/// </summary>
	public Transform checkGround;
	/// <summary>
	///timer and cooldown for scaling bird
	/// </summary>
	protected float timer;
	public float cooldown;
	/// <summary>
	///animator of main charactor
	/// </summary>
	protected Animator birdAnimator;
	/// <summary>
	///if the bird is died
	/// </summary>
	[HideInInspector]
	public bool isDie;
	/// <summary>
	///Game Manager
	/// </summary>
	protected GameManager gameManager;
	/// <summary>
	///index of platform
	/// </summary>
	private int platformIndex;
	/// <summary>
	///check the bird is hit platform
	/// </summary>
	bool colVertical;
	/// <summary>
	///effect appears when the bird hit platform
	/// </summary>
	public GameObject hitGroundEffect;
	/// <summary>
	///if the bird just got coin
	/// </summary>
	[HideInInspector]
	public bool coinGetAlready;
	/// <summary>
	///trigger for hiting ground
	/// </summary>
	private bool trigger;

	// Use this for initialization
	void Start ()
	{
		IsGrounded = false;
		isDie = false;
		InAir = true;
		currentGravity = gravity;
		birdAnimator = GetComponent<Animator> ();
		gameManager = FindObjectOfType<GameManager> ();
		platformIndex = 0;
		colVertical = false;
		coinGetAlready = true;
		Application.targetFrameRate = 60;
	}
	
	// Update is called once per frame
	void Update ()
	{
		//if the bird is live, not touch on UI and not in Shop, the bird can jump
		if (!isDie && !IsPointerOverUIObject () && !gameManager.inShop) {
			
			if (Input.GetMouseButtonDown (0)) {
				
				timer = 0.0f;
				gameManager.OnStart ();

			}
			if (Input.GetMouseButton (0)) {
				
				if (timer < 2.0f) {
					
					timer += Time.deltaTime;
					ScaleBird (GetScaleByTimer (timer));

				}
			}
			if (Input.GetMouseButtonUp (0)) {
				
				ScaleBird (1.0f);
				AudioManager.Instance.PlaySound ("jump");

			}
			// check collision with ground
			if (RaycastCollideVertical () && moveDirection.y < 0.0f && !IsGrounded) {
				
				IsGrounded = true;
				InAir = false;
				currentGravity = 0;
				jumpDirection = Vector2.zero;
				AudioManager.Instance.PlaySound ("hit");

			} 

			AnimateFly ();
			trigger = false;
			trigger = RaycastCollideVertical ();

			if (trigger) {
				
				TriggerWithPlatform (GetColliderRaycast ());

			}

			// perform physics update
			CalculateMoveDirection ();
			// move via transform component
			transform.Translate (moveDirection * Time.deltaTime);
		}
	}

	/// <summary>
	///check the touch is put on UI button element
	/// </summary>
	private bool IsPointerOverUIObject ()
	{
		// the ray cast appears to require only eventData.position.
		bool check = false;
		PointerEventData eventDataCurrentPosition = new PointerEventData (EventSystem.current);
		eventDataCurrentPosition.position = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
		List<RaycastResult> results = new List<RaycastResult> ();
		EventSystem.current.RaycastAll (eventDataCurrentPosition, results);

		foreach (RaycastResult r in results) {
			
			if (r.gameObject.GetComponent<Button> () != null)
				
				check = true;
			
		}

		return check;
	}

	/// <summary>
	///get true if touch is up
	/// </summary>
	bool JumpInputCheck ()
	{
		// button press
		return Input.GetMouseButtonUp (0);
	}

	/// <summary>
	///move the bird down and up 
	/// </summary>
	void CalculateMoveDirection ()
	{
		moveDirection = Vector2.zero;
		// checks: on ground or double jumping this frame, jump button pushed, and not hitting ceiling
		if (!InAir && JumpInputCheck ()) {
			
			InAir = true;
			IsGrounded = false;
			currentGravity = gravity;
			// if variable jump height allowed, start us moving at variable jump height speed else regular jump (or second jump of double jump)
			jumpDirection = new Vector2 (0, GetForceByTimer (timer));

		}
		// if airborne, apply gravity to current jump speed then add to movement vector
		if (InAir) {
			
			// apply gravity if normal jump or if allowing variable jump height, at max variable jump height or released button
			jumpDirection += new Vector2 (0, currentGravity);
			// moveDirection holds final value of movement delta
			moveDirection += jumpDirection;

		}
	}

	/// <summary>
	///check hit platform
	/// </summary>
	bool RaycastCollideVertical ()
	{
		

		// cast rays for collision checking
		RaycastHit2D hitBottomSide = Physics2D.Raycast (checkGround.position, -Vector2.up, 
			                             0.1f, collidingLayerMask);
		// check hit on left side
		if (hitBottomSide.collider != null) {
			// we hit explicit tagged object
			if (HitTagOrLayerObject (hitBottomSide.collider.gameObject)) {
				// set our position to hit point so we don't stutter in place
				transform.position = new Vector2 (transform.position.x, 
					hitBottomSide.point.y + 0.1f);
				return true; 
			}
		}
		// no hit
		return false;
	}
	/// <summary>
	///return collider if hit platform
	/// </summary>
	Collider2D GetColliderRaycast ()
	{
		// cast rays for collision checking
		RaycastHit2D hitBottomSide = Physics2D.Raycast (checkGround.position, -Vector2.up, 
			                             0.1f, collidingLayerMask);
		// check hit on left side
		if (hitBottomSide.collider != null) {
			// we hit explicit tagged object
			if (HitTagOrLayerObject (hitBottomSide.collider.gameObject)) {
				
				return hitBottomSide.collider; 
			}
		}
		// no hit
		return null;
	}
	/// <summary>
	///hit with tag or layer
	/// </summary>
	bool HitTagOrLayerObject (GameObject collisionObject)
	{
		// check if we are colliding by tag
		if (collidingTagList.Count > 0) {
			
			foreach (string tag in collidingTagList) {
				
				if (collisionObject.CompareTag (tag)) {
					
					return true;

				}

			}

			return false;
		}
		return true;
	}
	/// <summary>
	///animate the bird
	/// </summary>
	void AnimateFly ()
	{
		if (RaycastCollideVertical ()) {
			
			birdAnimator.SetBool ("Grounded", true);

		} else {
			
			birdAnimator.SetBool ("Grounded", false);

		}
	}
	/// <summary>
	///the bird is die
	/// </summary>
	public void Die ()
	{
		birdAnimator.SetBool ("Died", true);
		AudioManager.Instance.PlaySound ("die");
		currentGravity = 0;
		jumpDirection = Vector2.zero;
		isDie = true;
		gameManager.Shake ();
		gameManager.GameOver ();
	}
	/// <summary>
	///the bird get coin
	/// </summary>
	public void GetCoin (Vector3 pos)
	{
		gameManager.UpdateCoin ();
		gameManager.ShowFloatText (pos);
		AudioManager.Instance.PlaySound ("coin");
	}
	/// <summary>
	///force the bird with timer
	/// </summary>
	float GetForceByTimer (float rangeOfTime)
	{
		return 5.0f + (25.0f - 5.0f) * (timer / 2.0f);
	}
	/// <summary>
	///scale the bird with timer
	/// </summary>
	float GetScaleByTimer (float rangeOfTime)
	{
		float scale = 0.0f;
		scale = 1.0f - timer / 2.0f;
		return scale >= 0.33f ? scale : 0.33f;
	}
	/// <summary>
	///scale the bird with scale delta
	/// </summary>
	void ScaleBird (float delta)
	{
		transform.localScale = new Vector3 (1.0f, delta, 1.0f);
	}
	/// <summary>
	///trigger the bird hit platform
	/// </summary>
	void TriggerWithPlatform (Collider2D col)
	{
		
		if (col.tag == "Platform" && moveDirection.y < 0.0f) {
			
			Instantiate (hitGroundEffect, transform.position, transform.rotation);
			if (GameManager.firstJump) {
				
				gameManager.FirstSwapPlatform ();
				GameManager.firstJump = false;

			} else {
				
				if (col.gameObject.name == "Below")
					gameManager.MoveBelowPlatformGameOver ();
				else if (col.gameObject.name == "Above") {
					
					transform.parent = col.gameObject.transform;
					gameManager.MoveBelowPlatformClear ();
					gameManager.MoveAbovePlatformTop ();

				}
			}
			colVertical = true;
		}
		if (col.tag == "StartPlatform" && moveDirection.y < 0.0f) {
			
			Instantiate (hitGroundEffect, transform.position, transform.rotation);
			colVertical = true;

		}
		if (col.tag == "Coin") {
			
			Destroy (col.gameObject);
			coinGetAlready = true;

		}
	}
}
