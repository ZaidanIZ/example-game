using System;
using UnityEngine;

[Serializable]
public class ZoomCamera : MonoBehaviour
{
	public Transform origin;

	public float zoom;

	public float zoomMin;

	public float zoomMax;

	public float seekTime;

	public bool smoothZoomIn;

	private Vector3 defaultLocalPosition;

	private Transform thisTransform;

	private float currentZoom;

	private float targetZoom;

	private float zoomVelocity;

	public ZoomCamera()
	{
		this.zoomMin = (float)(-5);
		this.zoomMax = (float)5;
		this.seekTime = 1f;
	}

	public void Start()
	{
		this.thisTransform = this.transform;
		this.defaultLocalPosition = this.thisTransform.localPosition;
		this.currentZoom = this.zoom;
	}

	public void Update()
	{
		this.zoom = Mathf.Clamp(this.zoom, this.zoomMin, this.zoomMax);
		int layerMask = -261;
		RaycastHit raycastHit = default(RaycastHit);
		Vector3 position = this.origin.position;
		Vector3 vector = this.defaultLocalPosition + this.thisTransform.parent.InverseTransformDirection(this.thisTransform.forward * this.zoom);
		Vector3 end = this.thisTransform.parent.TransformPoint(vector);
		if (Physics.Linecast(position, end, out raycastHit, layerMask))
		{
			Vector3 a = raycastHit.point + this.thisTransform.TransformDirection(Vector3.forward);
			this.targetZoom = (a - this.thisTransform.parent.TransformPoint(this.defaultLocalPosition)).magnitude;
		}
		else
		{
			this.targetZoom = this.zoom;
		}
		this.targetZoom = Mathf.Clamp(this.targetZoom, this.zoomMin, this.zoomMax);
		if (!this.smoothZoomIn && this.targetZoom - this.currentZoom > (float)0)
		{
			this.currentZoom = this.targetZoom;
		}
		else
		{
			this.currentZoom = Mathf.SmoothDamp(this.currentZoom, this.targetZoom, ref this.zoomVelocity, this.seekTime);
		}
		vector = this.defaultLocalPosition + this.thisTransform.parent.InverseTransformDirection(this.thisTransform.forward * this.currentZoom);
		this.thisTransform.localPosition = vector;
	}

	public void Main()
	{
	}
}
