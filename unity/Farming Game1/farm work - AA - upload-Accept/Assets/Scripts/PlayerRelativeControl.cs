using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[Serializable]
public class PlayerRelativeControl : MonoBehaviour
{
	public Joystick moveJoystick;

	public Joystick rotateJoystick;

	public Transform cameraPivot;

	public float forwardSpeed;

	public float backwardSpeed;

	public float sidestepSpeed;

	public float jumpSpeed;

	public float inAirMultiplier;

	public Vector2 rotationSpeed;

	private Transform thisTransform;

	private CharacterController character;

	private Vector3 cameraVelocity;

	private Vector3 velocity;

	public PlayerRelativeControl()
	{
		this.forwardSpeed = (float)4;
		this.backwardSpeed = (float)1;
		this.sidestepSpeed = (float)1;
		this.jumpSpeed = (float)8;
		this.inAirMultiplier = 0.25f;
		this.rotationSpeed = new Vector2((float)50, (float)25);
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
		this.moveJoystick.Disable();
		this.rotateJoystick.Disable();
		this.enabled = false;
	}

	public void Update()
	{
		Vector3 vector = this.thisTransform.TransformDirection(new Vector3(this.moveJoystick.position.x, (float)0, this.moveJoystick.position.y));
		vector.y = (float)0;
		vector.Normalize();
		Vector3 zero = Vector3.zero;
		Vector2 vector2 = new Vector2(Mathf.Abs(this.moveJoystick.position.x), Mathf.Abs(this.moveJoystick.position.y));
		if (vector2.y > vector2.x)
		{
			if (this.moveJoystick.position.y > (float)0)
			{
				vector *= this.forwardSpeed * vector2.y;
			}
			else
			{
				vector *= this.backwardSpeed * vector2.y;
				zero.z = this.moveJoystick.position.y * 0.75f;
			}
		}
		else
		{
			vector *= this.sidestepSpeed * vector2.x;
			zero.x = -this.moveJoystick.position.x * 0.5f;
		}
		if (this.character.isGrounded)
		{
			if (this.rotateJoystick.tapCount == 2)
			{
				this.velocity = this.character.velocity;
				this.velocity.y = this.jumpSpeed;
			}
		}
		else
		{
			this.velocity.y = this.velocity.y + Physics.gravity.y * Time.deltaTime;
			zero.z = -this.jumpSpeed * 0.25f;
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
		Vector3 localPosition = this.cameraPivot.localPosition;
		localPosition.x = Mathf.SmoothDamp(localPosition.x, zero.x, ref this.cameraVelocity.x, 0.3f);
		localPosition.z = Mathf.SmoothDamp(localPosition.z, zero.z, ref this.cameraVelocity.z, 0.5f);
		this.cameraPivot.localPosition = localPosition;
		if (this.character.isGrounded)
		{
			Vector2 a = this.rotateJoystick.position;
			a.x *= this.rotationSpeed.x;
			a.y *= this.rotationSpeed.y;
			a *= Time.deltaTime;
			this.thisTransform.Rotate((float)0, a.x, (float)0, Space.World);
			this.cameraPivot.Rotate(a.y, (float)0, (float)0);
		}
	}

	public void Main()
	{
	}
}
