using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[Serializable]
public class FirstPersonControl : MonoBehaviour
{
	public Joystick moveTouchPad;

	public Joystick rotateTouchPad;

	public Transform cameraPivot;

	public float forwardSpeed;

	public float backwardSpeed;

	public float sidestepSpeed;

	public float jumpSpeed;

	public float inAirMultiplier;

	public Vector2 rotationSpeed;

	public float tiltPositiveYAxis;

	public float tiltNegativeYAxis;

	public float tiltXAxisMinimum;

	private Transform thisTransform;

	private CharacterController character;

	private Vector3 cameraVelocity;

	private Vector3 velocity;

	private bool canJump;

	public FirstPersonControl()
	{
		this.forwardSpeed = (float)4;
		this.backwardSpeed = (float)1;
		this.sidestepSpeed = (float)1;
		this.jumpSpeed = (float)8;
		this.inAirMultiplier = 0.25f;
		this.rotationSpeed = new Vector2((float)50, (float)25);
		this.tiltPositiveYAxis = 0.6f;
		this.tiltNegativeYAxis = 0.4f;
		this.tiltXAxisMinimum = 0.1f;
		this.canJump = true;
	}

	public void Start()
	{
		this.thisTransform = (Transform)this.GetComponent(typeof(Transform));
		this.character = (CharacterController)this.GetComponent(typeof(CharacterController));
		GameObject gameObject = GameObject.Find("PlayerSpawn");
		if (gameObject)
		{
			this.thisTransform.position = gameObject.transform.position;
		}
	}

	public void OnEndGame()
	{
		this.moveTouchPad.Disable();
		if (this.rotateTouchPad)
		{
			this.rotateTouchPad.Disable();
		}
		this.enabled = false;
	}

	public void Update()
	{
		Vector3 vector = this.thisTransform.TransformDirection(new Vector3(this.moveTouchPad.position.x, (float)0, this.moveTouchPad.position.y));
		vector.y = (float)0;
		vector.Normalize();
		Vector2 vector2 = new Vector2(Mathf.Abs(this.moveTouchPad.position.x), Mathf.Abs(this.moveTouchPad.position.y));
		if (vector2.y > vector2.x)
		{
			if (this.moveTouchPad.position.y > (float)0)
			{
				vector *= this.forwardSpeed * vector2.y;
			}
			else
			{
				vector *= this.backwardSpeed * vector2.y;
			}
		}
		else
		{
			vector *= this.sidestepSpeed * vector2.x;
		}
		if (this.character.isGrounded)
		{
			bool flag = false;
			Joystick joystick;
			if (this.rotateTouchPad)
			{
				joystick = this.rotateTouchPad;
			}
			else
			{
				joystick = this.moveTouchPad;
			}
			if (!joystick.IsFingerDown())
			{
				this.canJump = true;
			}
			if (this.canJump && joystick.tapCount >= 2)
			{
				flag = true;
				this.canJump = false;
			}
			if (flag)
			{
				this.velocity = this.character.velocity;
				this.velocity.y = this.jumpSpeed;
			}
		}
		else
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
		if (this.character.isGrounded)
		{
			Vector2 a = Vector2.zero;
			if (this.rotateTouchPad)
			{
				a = this.rotateTouchPad.position;
			}
			else
			{
				Vector3 acceleration = Input.acceleration;
				float num = Mathf.Abs(acceleration.x);
				if (acceleration.z < (float)0 && acceleration.x < (float)0)
				{
					if (num >= this.tiltPositiveYAxis)
					{
						a.y = (num - this.tiltPositiveYAxis) / ((float)1 - this.tiltPositiveYAxis);
					}
					else if (num <= this.tiltNegativeYAxis)
					{
						a.y = -(this.tiltNegativeYAxis - num) / this.tiltNegativeYAxis;
					}
				}
				if (Mathf.Abs(acceleration.y) >= this.tiltXAxisMinimum)
				{
					a.x = -(acceleration.y - this.tiltXAxisMinimum) / ((float)1 - this.tiltXAxisMinimum);
				}
			}
			a.x *= this.rotationSpeed.x;
			a.y *= this.rotationSpeed.y;
			a *= Time.deltaTime;
			this.thisTransform.Rotate((float)0, a.x, (float)0, Space.World);
			this.cameraPivot.Rotate(-a.y, (float)0, (float)0);
		}
	}

	public void Main()
	{
	}
}
