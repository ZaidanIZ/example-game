using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[Serializable]
public class SidescrollControl : MonoBehaviour
{
	public Joystick moveTouchPad;

	public Joystick jumpTouchPad;

	public float forwardSpeed;

	public float backwardSpeed;

	public float jumpSpeed;

	public float inAirMultiplier;

	private Transform thisTransform;

	private CharacterController character;

	private Vector3 velocity;

	private bool canJump;

	public SidescrollControl()
	{
		this.forwardSpeed = (float)4;
		this.backwardSpeed = (float)4;
		this.jumpSpeed = (float)16;
		this.inAirMultiplier = 0.25f;
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
		this.jumpTouchPad.Disable();
		this.enabled = false;
	}

	public void Update()
	{
		Vector3 vector = Vector3.zero;
		if (this.moveTouchPad.position.x > (float)0)
		{
			vector = Vector3.right * this.forwardSpeed * this.moveTouchPad.position.x;
		}
		else
		{
			vector = Vector3.right * this.backwardSpeed * this.moveTouchPad.position.x;
		}
		if (this.character.isGrounded)
		{
			bool flag = false;
			Joystick joystick = this.jumpTouchPad;
			if (!joystick.IsFingerDown())
			{
				this.canJump = true;
			}
			if (this.canJump && joystick.IsFingerDown())
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
		}
		vector += this.velocity;
		vector += Physics.gravity;
		vector *= Time.deltaTime;
		this.character.Move(vector);
		if (this.character.isGrounded)
		{
			this.velocity = Vector3.zero;
		}
	}

	public void Main()
	{
	}
}
