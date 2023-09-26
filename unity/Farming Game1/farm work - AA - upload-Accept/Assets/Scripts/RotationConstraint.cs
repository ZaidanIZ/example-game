using System;
using UnityEngine;

[Serializable]
public class RotationConstraint : MonoBehaviour
{
	public ConstraintAxis axis;

	public float min;

	public float max;

	private Transform thisTransform;

	private Vector3 rotateAround;

	private Quaternion minQuaternion;

	private Quaternion maxQuaternion;

	private float range;

	public void Start()
	{
		this.thisTransform = this.transform;
		ConstraintAxis constraintAxis = this.axis;
		if (constraintAxis == ConstraintAxis.X)
		{
			this.rotateAround = Vector3.right;
		}
		else if (constraintAxis == ConstraintAxis.Y)
		{
			this.rotateAround = Vector3.up;
		}
		else if (constraintAxis == ConstraintAxis.Z)
		{
			this.rotateAround = Vector3.forward;
		}
		Quaternion lhs = Quaternion.AngleAxis(this.thisTransform.localRotation.eulerAngles[(int)this.axis], this.rotateAround);
		this.minQuaternion = lhs * Quaternion.AngleAxis(this.min, this.rotateAround);
		this.maxQuaternion = lhs * Quaternion.AngleAxis(this.max, this.rotateAround);
		this.range = this.max - this.min;
	}

	public void LateUpdate()
	{
		Quaternion localRotation = this.thisTransform.localRotation;
		Quaternion a = Quaternion.AngleAxis(localRotation.eulerAngles[(int)this.axis], this.rotateAround);
		float num = Quaternion.Angle(a, this.minQuaternion);
		float num2 = Quaternion.Angle(a, this.maxQuaternion);
		if (num > this.range || num2 > this.range)
		{
			Vector3 eulerAngles = localRotation.eulerAngles;
			if (num > num2)
			{
				eulerAngles[(int)this.axis] = this.maxQuaternion.eulerAngles[(int)this.axis];
			}
			else
			{
				eulerAngles[(int)this.axis] = this.minQuaternion.eulerAngles[(int)this.axis];
			}
			this.thisTransform.localEulerAngles = eulerAngles;
		}
	}

	public void Main()
	{
	}
}
