using UnityEngine;
using System.Collections;

namespace TrickshotArena
{
	public class mouseFollow : MonoBehaviour
	{
		/// <summary>
		/// Make this game object to follow exact mouse position on the 3d->2d world
		/// </summary>

		private float zOffset = -0.5f;      //fixed position on Z axis.
		private Vector3 tmpPosition;

		void Start()
		{
			transform.position = new Vector3(transform.position.x, transform.position.y, zOffset);
		}

		void Update()
		{
			//get mouse position in game scene.
			tmpPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
			//follow the mouse
			transform.position = new Vector3(tmpPosition.x, tmpPosition.y, zOffset);
		}

	}
}