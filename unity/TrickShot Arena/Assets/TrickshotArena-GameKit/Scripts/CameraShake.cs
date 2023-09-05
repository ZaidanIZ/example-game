using UnityEngine;
using System.Collections;

namespace TrickshotArena
{
	public class CameraShake : MonoBehaviour
	{
		/// <summary>
		/// We use this class the shake the camera by changing camera's position in a quick succession.
		/// </summary>

		public static CameraShake instance;
		private Transform camTransform;
		internal float decreaseFactor = 1.0f;
		private Vector3 originalPos;

		void Awake()
		{
			instance = this;
			camTransform = GetComponent(typeof(Transform)) as Transform;
			originalPos = camTransform.localPosition;
		}

		public static CameraShake GetInstance()
		{
			return instance;
		}

		/// <summary>
		/// Shake the camera with the given duration and power
		/// </summary>
		/// <param name="duration"></param>
		/// <param name="power"></param>
		/// <returns></returns>
		public IEnumerator Shake(float duration, float power)
		{
			float t = 0f;
			while (t < duration)
			{
				t += Time.deltaTime * decreaseFactor;
				camTransform.localPosition = originalPos + Random.insideUnitSphere * power;
				yield return 0;
			}

			if (t >= duration)
			{
				camTransform.localPosition = originalPos;
			}
		}

	}
}