using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Estrellas : MonoBehaviour
{
	public float LifeTime;

	public float xinc;

	public float yinc;

	public float RotActual;

	public float Largo;

	public Sprite SpriteAlternativo;

	public Estrellas()
	{
		this.Largo = (float)1;
	}

	public void Start()
	{
		float num = 0f;
		this.LifeTime = UnityEngine.Random.value + (float)2;
		this.xinc = (UnityEngine.Random.value - 0.5f) * (float)4;
		this.yinc = (UnityEngine.Random.value - 0.5f) * (float)4;
		float x = this.transform.position.x + this.xinc * (float)10 * this.Largo;
		Vector3 position = this.transform.position;
		float num2 = position.x = x;
		Vector3 vector = this.transform.position = position;
		num = UnityEngine.Random.Range(0.1f, 0.4f);
		this.transform.localScale = new Vector3(num, num, (float)0);
		float g = UnityEngine.Random.value + 0.5f;
		Color color = ((Image)this.GetComponent(typeof(Image))).color;
		float num3 = color.g = g;
		Color color2 = ((Image)this.GetComponent(typeof(Image))).color = color;
		float g2 = ((Image)this.GetComponent(typeof(Image))).color.g;
		Color color3 = ((Image)this.GetComponent(typeof(Image))).color;
		float num4 = color3.b = g2;
		Color color4 = ((Image)this.GetComponent(typeof(Image))).color = color3;
		if (UnityEngine.Random.value > 0.5f)
		{
			((Image)this.GetComponent(typeof(Image))).sprite = this.SpriteAlternativo;
		}
	}

	public void Update()
	{
		float a = ((Image)this.GetComponent(typeof(Image))).color.a - Time.deltaTime / this.LifeTime;
		Color color = ((Image)this.GetComponent(typeof(Image))).color;
		float num = color.a = a;
		Color color2 = ((Image)this.GetComponent(typeof(Image))).color = color;
		this.RotActual += this.xinc * (float)2;
		this.transform.rotation = Quaternion.Euler((float)0, (float)0, this.RotActual);
		float x = this.transform.position.x + this.xinc;
		Vector3 position = this.transform.position;
		float num2 = position.x = x;
		Vector3 vector = this.transform.position = position;
		float y = this.transform.position.y + this.yinc;
		Vector3 position2 = this.transform.position;
		float num3 = position2.y = y;
		Vector3 vector2 = this.transform.position = position2;
		this.yinc -= 0.02f;
		if (((Image)this.GetComponent(typeof(Image))).color.a <= (float)0)
		{
			UnityEngine.Object.Destroy(this.gameObject);
		}
	}

	public void Main()
	{
	}
}
