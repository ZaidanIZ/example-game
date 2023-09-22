using System;
using UnityEngine;

[ExecuteInEditMode]
public class Water : MonoBehaviour
{
	[SerializeField]
	private Camera refractionCamera;

	[SerializeField]
	private bool useDynamicReflection = true;

	[SerializeField]
	private Texture2D fakeReflectionTexture;

	[SerializeField]
	private int reflectionTextureSize = 512;

	[SerializeField]
	private float reflClipPlaneOffset;

	[SerializeField]
	private Camera reflectionCamera;

	private RenderTexture reflectionTexture;

	private RenderTexture refractionTexture;

	private static bool isReflectionRendering;

	private void Awake()
	{
		Renderer component = base.GetComponent<Renderer>();
		if (!base.enabled || !component || !component.sharedMaterial || !component.enabled)
		{
			UnityEngine.Debug.LogError("Please make sure water renderer is available!");
			return;
		}
		this.InitRefraction(component);
		this.InitReflection(component);
	}

	private void InitRefraction(Renderer waterRenderer)
	{
		if (this.refractionCamera != null)
		{
			this.refractionTexture = new RenderTexture(Screen.width, Screen.height, 16, RenderTextureFormat.ARGB32);
			this.refractionTexture.isPowerOfTwo = false;
			this.refractionCamera.targetTexture = this.refractionTexture;
			waterRenderer.sharedMaterial.SetTexture("_RenderTex", this.refractionTexture);
			return;
		}
		UnityEngine.Debug.LogError("Please assign refraction camera!");
	}

	private void InitReflection(Renderer waterRenderer)
	{
		if (this.reflectionCamera != null)
		{
			this.reflectionCamera.enabled = false;
		}
		if (this.useDynamicReflection)
		{
			if (!(this.reflectionCamera != null))
			{
				UnityEngine.Debug.LogError("Please assign reflectionCamera since you want to use dynamic reflection!");
				return;
			}
			this.reflectionTexture = new RenderTexture(this.reflectionTextureSize, this.reflectionTextureSize, 16, RenderTextureFormat.ARGB32);
			this.reflectionTexture.name = "ReflectionTexture";
			this.reflectionTexture.isPowerOfTwo = true;
			this.reflectionTexture.hideFlags = HideFlags.DontSave;
			this.reflectionCamera.targetTexture = this.reflectionTexture;
			this.reflectionCamera.enabled = false;
			if (waterRenderer.sharedMaterial.HasProperty("_ReflectionTex"))
			{
				waterRenderer.sharedMaterial.SetTexture("_ReflectionTex", this.reflectionTexture);
			}
		}
		else if (this.fakeReflectionTexture != null)
		{
			if (waterRenderer.sharedMaterial.HasProperty("_ReflectionTex"))
			{
				waterRenderer.sharedMaterial.SetTexture("_ReflectionTex", this.fakeReflectionTexture);
			}
		}
		else
		{
			UnityEngine.Debug.LogError("Please assign fakeReflectionTexture since you don't use dynamic reflection!");
		}
	}

	private void OnDisable()
	{
	}

	private void OnWillRenderObject()
	{
		if (!this.useDynamicReflection)
		{
			return;
		}
		Camera current = Camera.current;
		if (!current)
		{
			return;
		}
		if (Water.isReflectionRendering)
		{
			return;
		}
		Water.isReflectionRendering = true;
		Vector3 position = base.transform.position;
		Vector3 up = base.transform.up;
		this.SynchronousCamera(current, this.reflectionCamera);
		float w = -Vector3.Dot(up, position) - this.reflClipPlaneOffset;
		Vector4 plane = new Vector4(up.x, up.y, up.z, w);
		Matrix4x4 zero = Matrix4x4.zero;
		Water.CalculateReflectionMatrix(ref zero, plane);
		Vector3 position2 = current.transform.position;
		Vector3 position3 = zero.MultiplyPoint(position2);
		this.reflectionCamera.worldToCameraMatrix = current.worldToCameraMatrix * zero;
		Vector4 clipPlane = this.CameraSpacePlane(this.reflectionCamera, position, up, 1f);
		Matrix4x4 projectionMatrix = current.CalculateObliqueMatrix(clipPlane);
		this.reflectionCamera.projectionMatrix = projectionMatrix;
		GL.SetRevertBackfacing(true);
		this.reflectionCamera.transform.position = position3;
		Vector3 eulerAngles = current.transform.eulerAngles;
		this.reflectionCamera.transform.eulerAngles = new Vector3(0f, eulerAngles.y, eulerAngles.z);
		this.reflectionCamera.Render();
		this.reflectionCamera.transform.position = position2;
		GL.SetRevertBackfacing(false);
		Water.isReflectionRendering = false;
	}

	private void SynchronousCamera(Camera src, Camera dest)
	{
		if (dest == null)
		{
			return;
		}
		dest.farClipPlane = src.farClipPlane;
		dest.nearClipPlane = src.nearClipPlane;
		dest.orthographic = src.orthographic;
		dest.fieldOfView = src.fieldOfView;
		dest.aspect = src.aspect;
		dest.orthographicSize = src.orthographicSize;
	}

	private Vector4 CameraSpacePlane(Camera cam, Vector3 pos, Vector3 normal, float sideSign)
	{
		Vector3 v = pos + normal * this.reflClipPlaneOffset;
		Matrix4x4 worldToCameraMatrix = cam.worldToCameraMatrix;
		Vector3 lhs = worldToCameraMatrix.MultiplyPoint(v);
		Vector3 rhs = worldToCameraMatrix.MultiplyVector(normal).normalized * sideSign;
		return new Vector4(rhs.x, rhs.y, rhs.z, -Vector3.Dot(lhs, rhs));
	}

	private static void CalculateReflectionMatrix(ref Matrix4x4 reflMatrix, Vector4 plane)
	{
		reflMatrix.m00 = 1f - 2f * plane[0] * plane[0];
		reflMatrix.m01 = -2f * plane[0] * plane[1];
		reflMatrix.m02 = -2f * plane[0] * plane[2];
		reflMatrix.m03 = -2f * plane[3] * plane[0];
		reflMatrix.m10 = -2f * plane[1] * plane[0];
		reflMatrix.m11 = 1f - 2f * plane[1] * plane[1];
		reflMatrix.m12 = -2f * plane[1] * plane[2];
		reflMatrix.m13 = -2f * plane[3] * plane[1];
		reflMatrix.m20 = -2f * plane[2] * plane[0];
		reflMatrix.m21 = -2f * plane[2] * plane[1];
		reflMatrix.m22 = 1f - 2f * plane[2] * plane[2];
		reflMatrix.m23 = -2f * plane[3] * plane[2];
		reflMatrix.m30 = 0f;
		reflMatrix.m31 = 0f;
		reflMatrix.m32 = 0f;
		reflMatrix.m33 = 1f;
	}
}
