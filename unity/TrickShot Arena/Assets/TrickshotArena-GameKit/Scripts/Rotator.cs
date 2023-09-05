using UnityEngine;
using System.Collections;

namespace TrickshotArena
{
	public class Rotator : MonoBehaviour
	{
		/// <summary>
		/// Rotate the given object by the provided speed
		/// </summary>

		public int dir = 1;
		public int speed = 40;

		void Update()
		{
			transform.rotation = Quaternion.Euler(0, 180, Time.time * speed * dir);
		}
	}
}