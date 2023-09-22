using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[Serializable]
public class CameraRelativeControl : MonoBehaviour
{
	public Joystick moveJoystick;

	public Joystick rotateJoystick;

	public Transform cameraPivot;

	public Transform cameraTransform;

	public float speed;

	public float jumpSpeed;

	public float inAirMultiplier;

	public Vector2 rotationSpeed;

	private Transform thisTransform;

	private CharacterController character;

	private Vector3 velocity;

	private bool canJump;

	public CameraRelativeControl()
	{
		this.speed = (float)5;
		this.jumpSpeed = (float)8;
		this.inAirMultiplier = 0.25f;
		this.rotationSpeed = new Vector2((float)50, (float)25);
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

	public void FaceMovementDirection()
	{
		Vector3 vector = this.character.velocity;
		vector.y = (float)0;
		if (vector.magnitude > 0.1f)
		{
			this.thisTransform.forward = vector.normalized;
		}
	}

	public void OnEndGame()
	{
		this.moveJoystick.Disable();
		this.rotateJoystick.Disable();
		this.enabled = false;
	}

	public void Update()
	{
		Vector3 vector = this.cameraTransform.TransformDirection(new Vector3(this.moveJoystick.position.x, (float)0, this.moveJoystick.position.y));
		vector.y = (float)0;
		vector.Normalize();
		Vector2 vector2 = new Vector2(Mathf.Abs(this.moveJoystick.position.x), Mathf.Abs(this.moveJoystick.position.y));
		vector *= this.speed * ((vector2.x <= vector2.y) ? vector2.y : vector2.x);
		if (this.character.isGrounded)
		{
			if (!this.rotateJoystick.IsFingerDown())
			{
				this.canJump = true;
			}
			if (this.canJump && this.rotateJoystick.tapCount == 2)
			{
				this.velocity = this.character.velocity;
				this.velocity.y = this.jumpSpeed;
				this.canJump = false;
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
		this.FaceMovementDirection();
		Vector2 a = this.rotateJoystick.position;
		a.x *= this.rotationSpeed.x;
		a.y *= this.rotationSpeed.y;
		a *= Time.deltaTime;
		this.cameraPivot.Rotate((float)0, a.x, (float)0, Space.World);
		this.cameraPivot.Rotate(a.y, (float)0, (float)0);
	}

	public void Main()
	{
	}
}
