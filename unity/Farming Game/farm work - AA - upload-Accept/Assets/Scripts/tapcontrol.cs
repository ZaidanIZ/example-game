using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class tapcontrol : MonoBehaviour
{
	public GameObject cameraObject;

	public Transform cameraPivot;

	public Image jumpButton;

	public float speed;

	public float jumpSpeed;

	public float inAirMultiplier;

	public float minimumDistanceToMove;

	public float minimumTimeUntilMove;

	public bool zoomEnabled;

	public float zoomEpsilon;

	public float zoomRate;

	public bool rotateEnabled;

	public float rotateEpsilon;

	private ZoomCamera zoomCamera;

	private Camera cam;

	private Transform thisTransform;

	private CharacterController character;

	private Vector3 targetLocation;

	private bool moving;

	private float rotationTarget;

	private float rotationVelocity;

	private Vector3 velocity;

	private ControlState state;

	private int[] fingerDown;

	private Vector2[] fingerDownPosition;

	private int[] fingerDownFrame;

	private float firstTouchTime;

	public tapcontrol()
	{
		this.inAirMultiplier = 0.25f;
		this.minimumDistanceToMove = 1f;
		this.minimumTimeUntilMove = 0.25f;
		this.rotateEpsilon = (float)1;
		this.state = ControlState.WaitingForFirstTouch;
		this.fingerDown = new int[2];
		this.fingerDownPosition = new Vector2[2];
		this.fingerDownFrame = new int[2];
	}

	public void Start()
	{
		this.thisTransform = this.transform;
		this.zoomCamera = (ZoomCamera)this.cameraObject.GetComponent(typeof(ZoomCamera));
		this.cam = this.cameraObject.GetComponent<Camera>();
		this.character = (CharacterController)this.GetComponent(typeof(CharacterController));
		this.ResetControlState();
		GameObject gameObject = GameObject.Find("PlayerSpawn");
		if (gameObject)
		{
			this.thisTransform.position = gameObject.transform.position;
		}
	}

	public void OnEndGame()
	{
		this.enabled = false;
	}

	public void FaceMovementDirection()
	{
		Vector3 vector = this.character.velocity;
		vector.y = (float)0;
		if (vector.magnitude > 0.1f)
		{
			this.thisTransform.forward = vector.normalized;
		}
	}

	public void CameraControl(Touch touch0, Touch touch1)
	{
		if (this.rotateEnabled && this.state == ControlState.RotatingCamera)
		{
			Vector2 a = touch1.position - touch0.position;
			Vector2 lhs = a / a.magnitude;
			Vector2 a2 = touch1.position - touch1.deltaPosition - (touch0.position - touch0.deltaPosition);
			Vector2 rhs = a2 / a2.magnitude;
			float num = Vector2.Dot(lhs, rhs);
			if (num < (float)1)
			{
				Vector3 lhs2 = new Vector3(a.x, a.y);
				Vector3 rhs2 = new Vector3(a2.x, a2.y);
				float z = Vector3.Cross(lhs2, rhs2).normalized.z;
				float num2 = Mathf.Acos(num);
				this.rotationTarget += num2 * 57.29578f * z;
				if (this.rotationTarget < (float)0)
				{
					this.rotationTarget += (float)360;
				}
				else if (this.rotationTarget >= (float)360)
				{
					this.rotationTarget -= (float)360;
				}
			}
		}
		else if (this.zoomEnabled && this.state == ControlState.ZoomingCamera)
		{
			float magnitude = (touch1.position - touch0.position).magnitude;
			float magnitude2 = (touch1.position - touch1.deltaPosition - (touch0.position - touch0.deltaPosition)).magnitude;
			float num3 = magnitude - magnitude2;
			this.zoomCamera.zoom = this.zoomCamera.zoom + num3 * this.zoomRate * Time.deltaTime;
		}
	}

	public void CharacterControl()
	{
		int touchCount = UnityEngine.Input.touchCount;
		if (touchCount == 1 && this.state == ControlState.MovingCharacter)
		{
			Touch touch = UnityEngine.Input.GetTouch(0);
			//if (this.character.isGrounded && this.jumpButton.HitTest(touch.position))
			{
				this.velocity = this.character.velocity;
				this.velocity.y = this.jumpSpeed;
			}
			//else if (!this.jumpButton.HitTest(touch.position) && touch.phase != TouchPhase.Began)
			{
				Ray ray = this.cam.ScreenPointToRay(new Vector3(touch.position.x, touch.position.y));
				RaycastHit raycastHit = default(RaycastHit);
				if (Physics.Raycast(ray, out raycastHit))
				{
					float magnitude = (this.transform.position - raycastHit.point).magnitude;
					if (magnitude > this.minimumDistanceToMove)
					{
						this.targetLocation = raycastHit.point;
					}
					this.moving = true;
				}
			}
		}
		Vector3 vector = Vector3.zero;
		if (this.moving)
		{
			vector = this.targetLocation - this.thisTransform.position;
			vector.y = (float)0;
			float magnitude2 = vector.magnitude;
			if (magnitude2 < (float)1)
			{
				this.moving = false;
			}
			else
			{
				vector = vector.normalized * this.speed;
			}
		}
		if (!this.character.isGrounded)
		{
			this.velocity.y = this.velocity.y + Physics.gravity.y * Time.deltaTime;
			vector.x *= this.inAirMultiplier;
			vector.z *= this.inAirMultiplier;
		}
		vector += this.velocity;
		vector += Physics.gravity;
		vector *= Time.deltaTime;
		this.character.Move(vector);
		if (this.character.isGrounded)
		{
			this.velocity = Vector3.zero;
		}
		this.FaceMovementDirection();
	}

	public void ResetControlState()
	{
		this.state = ControlState.WaitingForFirstTouch;
		this.fingerDown[0] = -1;
		this.fingerDown[1] = -1;
	}

	public void Update()
	{
		int touchCount = UnityEngine.Input.touchCount;
		if (touchCount == 0)
		{
			this.ResetControlState();
		}
		else
		{
			int i = 0;
			Touch touch = default(Touch);
			Touch[] touches = Input.touches;
			Touch touch2 = default(Touch);
			Touch touch3 = default(Touch);
			bool flag = false;
			bool flag2 = false;
			if (this.state == ControlState.WaitingForFirstTouch)
			{
				for (i = 0; i < touchCount; i++)
				{
					touch = touches[i];
					if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
					{
						this.state = ControlState.WaitingForSecondTouch;
						this.firstTouchTime = Time.time;
						this.fingerDown[0] = touch.fingerId;
						this.fingerDownPosition[0] = touch.position;
						this.fingerDownFrame[0] = Time.frameCount;
						break;
					}
				}
			}
			if (this.state == ControlState.WaitingForSecondTouch)
			{
				for (i = 0; i < touchCount; i++)
				{
					touch = touches[i];
					if (touch.phase != TouchPhase.Canceled)
					{
						if (touchCount >= 2 && touch.fingerId != this.fingerDown[0])
						{
							this.state = ControlState.WaitingForMovement;
							this.fingerDown[1] = touch.fingerId;
							this.fingerDownPosition[1] = touch.position;
							this.fingerDownFrame[1] = Time.frameCount;
							break;
						}
						if (touchCount == 1)
						{
							Vector2 vector = touch.position - this.fingerDownPosition[0];
							if (touch.fingerId == this.fingerDown[0] && (Time.time > this.firstTouchTime + this.minimumTimeUntilMove || touch.phase == TouchPhase.Ended))
							{
								this.state = ControlState.MovingCharacter;
								break;
							}
						}
					}
				}
			}
			if (this.state == ControlState.WaitingForMovement)
			{
				for (i = 0; i < touchCount; i++)
				{
					touch = touches[i];
					if (touch.phase == TouchPhase.Began)
					{
						if (touch.fingerId == this.fingerDown[0] && this.fingerDownFrame[0] == Time.frameCount)
						{
							touch2 = touch;
							flag = true;
						}
						else if (touch.fingerId != this.fingerDown[0] && touch.fingerId != this.fingerDown[1])
						{
							this.fingerDown[1] = touch.fingerId;
							touch3 = touch;
							flag2 = true;
						}
					}
					if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Ended)
					{
						if (touch.fingerId == this.fingerDown[0])
						{
							touch2 = touch;
							flag = true;
						}
						else if (touch.fingerId == this.fingerDown[1])
						{
							touch3 = touch;
							flag2 = true;
						}
					}
				}
				if (flag)
				{
					if (flag2)
					{
						Vector2 a = this.fingerDownPosition[1] - this.fingerDownPosition[0];
						Vector2 a2 = touch3.position - touch2.position;
						Vector2 lhs = a / a.magnitude;
						Vector2 rhs = a2 / a2.magnitude;
						float num = Vector2.Dot(lhs, rhs);
						if (num < (float)1)
						{
							float num2 = Mathf.Acos(num);
							if (num2 > this.rotateEpsilon * 0.0174532924f)
							{
								this.state = ControlState.RotatingCamera;
							}
						}
						if (this.state == ControlState.WaitingForMovement)
						{
							float f = a.magnitude - a2.magnitude;
							if (Mathf.Abs(f) > this.zoomEpsilon)
							{
								this.state = ControlState.ZoomingCamera;
							}
						}
					}
				}
				else
				{
					this.state = ControlState.WaitingForNoFingers;
				}
			}
			if (this.state == ControlState.RotatingCamera || this.state == ControlState.ZoomingCamera)
			{
				for (i = 0; i < touchCount; i++)
				{
					touch = touches[i];
					if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Ended)
					{
						if (touch.fingerId == this.fingerDown[0])
						{
							touch2 = touch;
							flag = true;
						}
						else if (touch.fingerId == this.fingerDown[1])
						{
							touch3 = touch;
							flag2 = true;
						}
					}
				}
				if (flag)
				{
					if (flag2)
					{
						this.CameraControl(touch2, touch3);
					}
				}
				else
				{
					this.state = ControlState.WaitingForNoFingers;
				}
			}
		}
		this.CharacterControl();
	}

	public void LateUpdate()
	{
		float y = Mathf.SmoothDampAngle(this.cameraPivot.eulerAngles.y, this.rotationTarget, ref this.rotationVelocity, 0.3f);
		Vector3 eulerAngles = this.cameraPivot.eulerAngles;
		float num = eulerAngles.y = y;
		Vector3 vector = this.cameraPivot.eulerAngles = eulerAngles;
	}

	public void Main()
	{
	}
}
