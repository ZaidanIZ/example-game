using System;
using UnityEngine;

public class ScrollingUVs : MonoBehaviour
{
	public int materialIndex;

	public Vector2 uvAnimationRate = new Vector2(1f, 0f);

	public string textureName = "_MainTex";

	public bool ScrollBump = true;

	public string bumpName = "_BumpMap";

	private Vector2 uvOffset = Vector2.zero;

	private void LateUpdate()
	{
		this.uvOffset += this.uvAnimationRate * Time.deltaTime;
		if (base.GetComponent<Renderer>().enabled)
		{
			base.GetComponent<Renderer>().materials[this.materialIndex].SetTextureOffset(this.textureName, this.uvOffset);
			if (this.ScrollBump)
			{
				base.GetComponent<Renderer>().materials[this.materialIndex].SetTextureOffset(this.bumpName, this.uvOffset);
			}
		}
	}
}
