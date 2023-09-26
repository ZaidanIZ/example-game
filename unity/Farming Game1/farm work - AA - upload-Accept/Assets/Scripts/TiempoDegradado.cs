using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class TiempoDegradado : MonoBehaviour
{
	public float AlfaTime;

	public void Update()
	{
		float num = 0f;
		float num2 = 0f;
		num = ((Slider)this.GetComponent(typeof(Slider))).value;
		((Slider)this.GetComponent(typeof(Slider))).value = ((Slider)GameObject.Find("SliderTiempo").GetComponent(typeof(Slider))).value;
		num2 = ((Slider)this.GetComponent(typeof(Slider))).value;
		if (num >= 0.33f && num2 < 0.33f)
		{
			this.AlfaTime = (float)1;
		}
		else if (num >= 0.66f && num2 < 0.66f)
		{
			this.AlfaTime = (float)1;
		}
		if (this.AlfaTime > (float)0)
		{
			this.AlfaTime -= Time.deltaTime;
			float alfaTime = this.AlfaTime;
			Color color = ((Image)GameObject.Find("Fill2").GetComponent(typeof(Image))).color;
			float num3 = color.a = alfaTime;
			Color color2 = ((Image)GameObject.Find("Fill2").GetComponent(typeof(Image))).color = color;
			if (this.AlfaTime <= (float)0)
			{
				((Image)GameObject.Find("Fill2").GetComponent(typeof(Image))).sprite = ((Image)GameObject.Find("Fill").GetComponent(typeof(Image))).sprite;
				int num4 = 1;
				Color color3 = ((Image)GameObject.Find("Fill2").GetComponent(typeof(Image))).color;
				float num5 = color3.a = (float)num4;
				Color color4 = ((Image)GameObject.Find("Fill2").GetComponent(typeof(Image))).color = color3;
			}
		}
	}

	public void Main()
	{
	}
}
