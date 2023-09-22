using System;
using UnityEngine;

[Serializable]
public class ControlParcela : MonoBehaviour
{
	public bool Arada;

	public int x;

	public int y;

	public float AlfaTime;

	public Material[] materials;

	public ControlParcela()
	{
		this.AlfaTime = (float)(-1);
	}

	public void Start()
	{
		this.GetComponent<Renderer>().sharedMaterial = this.materials[0];
		this.x = (int)Mathf.Round(this.transform.position.x / (float)4);
		this.y = (int)Mathf.Round(this.transform.position.z / (float)4);
		this.transform.position = new Vector3((float)(this.x * 4) + 0.1f, 0.01f, (float)(this.y * 4) + 0.1f);
		this.transform.localScale = new Vector3(3.8f, 0.0001f, 3.8f);
		this.name = "Parcelita:" + this.x + "," + this.y;
		this.tag = "ParcelaLibre";
		this.AlfaTime = (float)(-1);
		this.GetComponent<Renderer>().sharedMaterial = this.materials[0];
		((ContadorDeTiempo)GameObject.FindGameObjectWithTag("Tiempo").GetComponent(typeof(ContadorDeTiempo))).TotalParcela = ((ContadorDeTiempo)GameObject.FindGameObjectWithTag("Tiempo").GetComponent(typeof(ContadorDeTiempo))).TotalParcela + 1;
	}

	public void Update()
	{
		if (this.AlfaTime >= (float)0)
		{
			this.AlfaTime += Time.deltaTime;
			if (this.AlfaTime > 0.6f)
			{
				this.AlfaTime = (float)(-1);
				this.GetComponent<Renderer>().sharedMaterial = this.materials[1];
			}
		}
	}

	public void ChangeToPisada(float Direccion)
	{
		this.Arada = true;
		this.tag = "ParcelaArada";
		this.transform.rotation = Quaternion.Euler((float)0, Direccion, (float)0);
		this.AlfaTime = (float)0;
	}

	public void ChangeToArada(int Direccion)
	{
		this.GetComponent<Renderer>().sharedMaterial = this.materials[Direccion];
	}

	public void Main()
	{
	}
}
