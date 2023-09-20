using System;
using UnityEngine;

[Serializable]
public class MoverOlas : MonoBehaviour
{
	public float scrollSpeed;

	public float scrollSpeed2;

	public MoverOlas()
	{
		this.scrollSpeed = 0.09f;
		this.scrollSpeed2 = 0.09f;
	}

	public void FixedUpdate()
	{
		float num = Time.time * this.scrollSpeed;
		float num2 = Time.time * this.scrollSpeed2;
		this.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(num2, -num);
		this.GetComponent<Renderer>().material.SetTextureOffset("_BumpMap", new Vector2(-num2, -num));
	}

	public void Main()
	{
	}
}
