using System;
using UnityEngine;

public class WaterFlow : MonoBehaviour
{
	public float m_SpeedU = 0.1f;

	public float m_SpeedV = -0.1f;

	private void Update()
	{
		float x = Time.time * this.m_SpeedU;
		float y = Time.time * this.m_SpeedV;
		if (base.GetComponent<Renderer>())
		{
			base.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(x, y);
		}
	}
}
