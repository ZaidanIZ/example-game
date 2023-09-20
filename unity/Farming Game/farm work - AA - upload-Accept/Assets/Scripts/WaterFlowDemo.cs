using System;
using System.Collections;
using UnityEngine;

public class WaterFlowDemo : MonoBehaviour
{
	private Transform[] m_WaterList;

	public Texture[] m_HeightMapTextures;

	private int m_CurrentWaterIndex;

	private eWaterShaderType m_CurrentWaterShaderType;

	private WaterFlowOrbitCamera m_OrbitCamera;

	private Color m_WaterDiffuse_Diffuse;

	private float m_WaterDiffuse_Multiply;

	private float m_WaterDiffuse_UMoveSpeed;

	private float m_WaterDiffuse_VMoveSpeed;

	private Color m_WaterDiffuseOld_Diffuse;

	private float m_WaterDiffuseOld_Multiply;

	private float m_WaterDiffuseOld_UMoveSpeed;

	private float m_WaterDiffuseOld_VMoveSpeed;

	private Color m_WaterDiffuseOriginal_Diffuse;

	private float m_WaterDiffuseOriginal_Multiply;

	private float m_WaterDiffuseOriginal_UMoveSpeed;

	private float m_WaterDiffuseOriginal_VMoveSpeed;

	private int m_WaterHeightMap1Index;

	private int m_WaterHeightMap2Index;

	private float m_WaterHeightmap_WaterTile;

	private float m_WaterHeightmap_HeightmapTile;

	private float m_WaterHeightmap_Refraction;

	private float m_WaterHeightmap_Strength;

	private float m_WaterHeightmap_Multiply;

	private float m_WaterHeightmapOld_WaterTile;

	private float m_WaterHeightmapOld_HeightmapTile;

	private float m_WaterHeightmapOld_Refraction;

	private float m_WaterHeightmapOld_Strength;

	private float m_WaterHeightmapOld_Multiply;

	private float m_WaterHeightmapOriginal_WaterTile;

	private float m_WaterHeightmapOriginal_HeightmapTile;

	private float m_WaterHeightmapOriginal_Refraction;

	private float m_WaterHeightmapOriginal_Strength;

	private float m_WaterHeightmapOriginal_Multiply;

	private Color m_WaterSimple_Ambient;

	private float m_WaterSimple_UMoveSpeed;

	private float m_WaterSimple_VMoveSpeed;

	private Color m_WaterSimpleOld_Ambient;

	private float m_WaterSimpleOld_UMoveSpeed;

	private float m_WaterSimpleOld_VMoveSpeed;

	private Color m_WaterSimpleOriginal_Ambient;

	private float m_WaterSimpleOriginal_UMoveSpeed;

	private float m_WaterSimpleOriginal_VMoveSpeed;

	private void Start()
	{
		this.m_OrbitCamera = UnityEngine.Object.FindObjectOfType<WaterFlowOrbitCamera>();
		this.m_WaterList = new Transform[base.transform.childCount];
		int num = 0;
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform transform = (Transform)enumerator.Current;
				this.m_WaterList[num] = transform;
				this.m_WaterList[num].gameObject.SetActive(false);
				string name = this.m_WaterList[num].gameObject.GetComponent<Renderer>().material.shader.name;
				if (name != null)
				{
					if (!(name == "Water Flow/Water Diffuse"))
					{
						if (!(name == "Water Flow/Water Heightmap"))
						{
							if (name == "Water Flow/Water Simple")
							{
								this.m_WaterSimpleOriginal_Ambient = RenderSettings.ambientLight;
								this.m_WaterSimpleOriginal_UMoveSpeed = this.m_WaterList[num].gameObject.GetComponent<Renderer>().material.GetFloat("_MainTexMoveSpeedU");
								this.m_WaterSimpleOriginal_VMoveSpeed = this.m_WaterList[num].gameObject.GetComponent<Renderer>().material.GetFloat("_MainTexMoveSpeedV");
								this.m_WaterSimpleOld_Ambient = (this.m_WaterSimple_Ambient = this.m_WaterSimpleOriginal_Ambient);
								this.m_WaterSimpleOld_UMoveSpeed = (this.m_WaterSimple_UMoveSpeed = this.m_WaterSimpleOriginal_UMoveSpeed);
								this.m_WaterSimpleOld_VMoveSpeed = (this.m_WaterSimple_VMoveSpeed = this.m_WaterSimpleOriginal_VMoveSpeed);
							}
						}
						else
						{
							this.m_WaterHeightmapOriginal_WaterTile = this.m_WaterList[num].gameObject.GetComponent<Renderer>().material.GetFloat("_MainTexTile");
							this.m_WaterHeightmapOriginal_HeightmapTile = this.m_WaterList[num].gameObject.GetComponent<Renderer>().material.GetFloat("_HeightMapTile");
							this.m_WaterHeightmapOriginal_Refraction = this.m_WaterList[num].gameObject.GetComponent<Renderer>().material.GetFloat("_MainTexRefraction");
							this.m_WaterHeightmapOriginal_Strength = this.m_WaterList[num].gameObject.GetComponent<Renderer>().material.GetFloat("_HeightMapStrength");
							this.m_WaterHeightmapOriginal_Multiply = this.m_WaterList[num].gameObject.GetComponent<Renderer>().material.GetFloat("_HeightMapMultiply");
							this.m_WaterHeightmapOld_WaterTile = (this.m_WaterHeightmap_WaterTile = this.m_WaterHeightmapOriginal_WaterTile);
							this.m_WaterHeightmapOld_HeightmapTile = (this.m_WaterHeightmap_HeightmapTile = this.m_WaterHeightmapOriginal_HeightmapTile);
							this.m_WaterHeightmapOld_Refraction = (this.m_WaterHeightmap_Refraction = this.m_WaterHeightmapOriginal_Refraction);
							this.m_WaterHeightmapOld_Strength = (this.m_WaterHeightmap_Strength = this.m_WaterHeightmapOriginal_Strength);
							this.m_WaterHeightmapOld_Multiply = (this.m_WaterHeightmap_Multiply = this.m_WaterHeightmapOriginal_Multiply);
						}
					}
					else
					{
						this.m_WaterDiffuseOriginal_Diffuse = this.m_WaterList[num].gameObject.GetComponent<Renderer>().material.GetColor("_MainTexColor");
						this.m_WaterDiffuseOriginal_Multiply = this.m_WaterList[num].gameObject.GetComponent<Renderer>().material.GetFloat("_MainTexMultiply");
						this.m_WaterDiffuseOriginal_UMoveSpeed = this.m_WaterList[num].gameObject.GetComponent<Renderer>().material.GetFloat("_MainTexMoveSpeedU");
						this.m_WaterDiffuseOriginal_VMoveSpeed = this.m_WaterList[num].gameObject.GetComponent<Renderer>().material.GetFloat("_MainTexMoveSpeedV");
						this.m_WaterDiffuseOld_Diffuse = (this.m_WaterDiffuse_Diffuse = this.m_WaterDiffuseOriginal_Diffuse);
						this.m_WaterDiffuseOld_Multiply = (this.m_WaterDiffuse_Multiply = this.m_WaterDiffuseOriginal_Multiply);
						this.m_WaterDiffuseOld_UMoveSpeed = (this.m_WaterDiffuse_UMoveSpeed = this.m_WaterDiffuseOriginal_UMoveSpeed);
						this.m_WaterDiffuseOld_VMoveSpeed = (this.m_WaterDiffuse_VMoveSpeed = this.m_WaterDiffuseOriginal_VMoveSpeed);
					}
				}
				IL_399:
				num++;
				continue;
				goto IL_399;
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
		this.m_CurrentWaterIndex = 0;
		this.UpdateCurrentShaderType();
	}

	private void Update()
	{
		if (Input.GetMouseButtonUp(0) && !this.m_OrbitCamera.enabled)
		{
			this.m_OrbitCamera.enabled = true;
		}
	}

	private void OnGUI()
	{
		GUI.Window(1, new Rect((float)(Screen.width - 260), 5f, 250f, 80f), new GUI.WindowFunction(this.AppNameWindow), "Water Flow FREE 1.0.2");
		int num = 10 + 195 * base.transform.childCount;
		GUI.Window(2, new Rect((float)((Screen.width - num) / 2), (float)(Screen.height - 65), (float)num, 60f), new GUI.WindowFunction(this.DemoWater), "Select Water");
		this.ShaderGUIWindow();
	}

	private void AppNameWindow(int id)
	{
		if (GUI.Button(new Rect(15f, 25f, 220f, 20f), "www.ge-team.com"))
		{
			Application.OpenURL("http://ge-team.com/pages/unity-3d/");
		}
		if (GUI.Button(new Rect(15f, 50f, 220f, 20f), "support: geteamdev@gmail.com"))
		{
			Application.OpenURL("mailto:geteamdev@gmail.com");
		}
	}

	private void DemoWater(int id)
	{
		if (this.m_CurrentWaterIndex >= 0)
		{
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			for (int i = 0; i < base.transform.childCount; i++)
			{
				if (i == this.m_CurrentWaterIndex)
				{
					GUI.enabled = false;
					if (!this.m_WaterList[i].gameObject.activeSelf)
					{
						this.m_WaterList[i].gameObject.SetActive(true);
					}
				}
				else
				{
					GUI.enabled = true;
					if (this.m_WaterList[i].gameObject.activeSelf)
					{
						this.m_WaterList[i].gameObject.SetActive(false);
					}
				}
				if (GUI.Button(new Rect((float)(10 + 195 * i), 25f, 185f, 25f), this.m_WaterList[i].name))
				{
					this.m_WaterList[this.m_CurrentWaterIndex].gameObject.SetActive(false);
					this.m_WaterList[i].gameObject.SetActive(true);
					this.m_CurrentWaterIndex = i;
					this.UpdateCurrentShaderType();
				}
			}
			GUILayout.EndHorizontal();
		}
	}

	private void ShaderGUIWindow()
	{
		if (this.m_CurrentWaterShaderType == eWaterShaderType.WaterDiffuse)
		{
			GUI.Window(3, new Rect(0f, 0f, 300f, 240f), new GUI.WindowFunction(this.WaterDiffuseGUIWindow), this.m_WaterList[this.m_CurrentWaterIndex].gameObject.GetComponent<Renderer>().material.shader.name.Replace("Water Flow/", "Shader: "));
		}
		else if (this.m_CurrentWaterShaderType == eWaterShaderType.WaterHeightmap)
		{
			GUI.Window(4, new Rect(0f, 0f, 300f, 240f), new GUI.WindowFunction(this.WaterHeightmapGUIWindow), this.m_WaterList[this.m_CurrentWaterIndex].gameObject.GetComponent<Renderer>().material.shader.name.Replace("Water Flow/", "Shader: "));
		}
		else if (this.m_CurrentWaterShaderType == eWaterShaderType.WaterSimple)
		{
			GUI.Window(5, new Rect(0f, 0f, 300f, 240f), new GUI.WindowFunction(this.WaterSimpleGUIWindow), this.m_WaterList[this.m_CurrentWaterIndex].gameObject.GetComponent<Renderer>().material.shader.name.Replace("Water Flow/", "Shader: "));
		}
	}

	private void WaterDiffuseGUIWindow(int id)
	{
		GUI.Label(new Rect(10f, 25f, 65f, 22f), "Diffuse R");
		GUI.Label(new Rect(10f, 45f, 65f, 22f), "Diffuse G");
		GUI.Label(new Rect(10f, 65f, 65f, 22f), "Diffuse B");
		GUI.Label(new Rect(10f, 85f, 65f, 22f), "Diffuse A");
		this.m_WaterDiffuse_Diffuse = new Color(GUI.HorizontalSlider(new Rect(125f, 30f, 150f, 15f), this.m_WaterDiffuse_Diffuse.r, 0f, 1f), GUI.HorizontalSlider(new Rect(125f, 50f, 150f, 15f), this.m_WaterDiffuse_Diffuse.g, 0f, 1f), GUI.HorizontalSlider(new Rect(125f, 70f, 150f, 15f), this.m_WaterDiffuse_Diffuse.b, 0f, 1f), GUI.HorizontalSlider(new Rect(125f, 90f, 150f, 15f), this.m_WaterDiffuse_Diffuse.a, 0f, 1f));
		if (this.m_WaterDiffuseOld_Diffuse != this.m_WaterDiffuse_Diffuse)
		{
			this.PauseOrbitCamera();
			this.m_WaterDiffuseOld_Diffuse = this.m_WaterDiffuse_Diffuse;
			this.m_WaterList[this.m_CurrentWaterIndex].gameObject.GetComponent<Renderer>().material.SetColor("_MainTexColor", this.m_WaterDiffuse_Diffuse);
		}
		GUI.Label(new Rect(10f, 125f, 65f, 22f), "Multiply");
		this.m_WaterDiffuse_Multiply = GUI.HorizontalSlider(new Rect(125f, 130f, 150f, 15f), this.m_WaterDiffuse_Multiply, 0f, 5f);
		if (this.m_WaterDiffuseOld_Multiply != this.m_WaterDiffuse_Multiply)
		{
			this.PauseOrbitCamera();
			this.m_WaterDiffuseOld_Multiply = this.m_WaterDiffuse_Multiply;
			this.m_WaterList[this.m_CurrentWaterIndex].gameObject.GetComponent<Renderer>().material.SetFloat("_MainTexMultiply", this.m_WaterDiffuse_Multiply);
		}
		GUI.Label(new Rect(10f, 145f, 65f, 22f), "U Speed");
		this.m_WaterDiffuse_UMoveSpeed = GUI.HorizontalSlider(new Rect(125f, 150f, 150f, 15f), this.m_WaterDiffuse_UMoveSpeed, -6f, 6f);
		if (this.m_WaterDiffuseOld_UMoveSpeed != this.m_WaterDiffuse_UMoveSpeed)
		{
			this.PauseOrbitCamera();
			this.m_WaterDiffuseOld_UMoveSpeed = this.m_WaterDiffuse_UMoveSpeed;
			this.m_WaterList[this.m_CurrentWaterIndex].gameObject.GetComponent<Renderer>().material.SetFloat("_MainTexMoveSpeedU", this.m_WaterDiffuse_UMoveSpeed);
		}
		GUI.Label(new Rect(10f, 165f, 65f, 22f), "V Speed");
		this.m_WaterDiffuse_VMoveSpeed = GUI.HorizontalSlider(new Rect(125f, 170f, 150f, 15f), this.m_WaterDiffuse_VMoveSpeed, -6f, 6f);
		if (this.m_WaterDiffuseOld_VMoveSpeed != this.m_WaterDiffuse_VMoveSpeed)
		{
			this.PauseOrbitCamera();
			this.m_WaterDiffuseOld_VMoveSpeed = this.m_WaterDiffuse_VMoveSpeed;
			this.m_WaterList[this.m_CurrentWaterIndex].gameObject.GetComponent<Renderer>().material.SetFloat("_MainTexMoveSpeedV", this.m_WaterDiffuse_VMoveSpeed);
		}
		if (GUI.Button(new Rect(125f, 200f, 150f, 30f), "Reset"))
		{
			this.m_WaterList[this.m_CurrentWaterIndex].gameObject.GetComponent<Renderer>().material.SetColor("_MainTexColor", this.m_WaterDiffuseOriginal_Diffuse);
			this.m_WaterList[this.m_CurrentWaterIndex].gameObject.GetComponent<Renderer>().material.SetFloat("_MainTexMultiply", this.m_WaterDiffuseOriginal_Multiply);
			this.m_WaterList[this.m_CurrentWaterIndex].gameObject.GetComponent<Renderer>().material.SetFloat("_MainTexMoveSpeedU", this.m_WaterDiffuseOriginal_UMoveSpeed);
			this.m_WaterList[this.m_CurrentWaterIndex].gameObject.GetComponent<Renderer>().material.SetFloat("_MainTexMoveSpeedV", this.m_WaterDiffuseOriginal_VMoveSpeed);
			this.m_WaterDiffuse_Diffuse = new Color(GUI.HorizontalSlider(new Rect(125f, 30f, 150f, 15f), this.m_WaterDiffuseOriginal_Diffuse.r, 0f, 1f), GUI.HorizontalSlider(new Rect(125f, 50f, 150f, 15f), this.m_WaterDiffuseOriginal_Diffuse.g, 0f, 1f), GUI.HorizontalSlider(new Rect(125f, 70f, 150f, 15f), this.m_WaterDiffuseOriginal_Diffuse.b, 0f, 1f), GUI.HorizontalSlider(new Rect(125f, 90f, 150f, 15f), this.m_WaterDiffuseOriginal_Diffuse.a, 0f, 1f));
			this.m_WaterDiffuse_Multiply = GUI.HorizontalSlider(new Rect(125f, 130f, 150f, 15f), this.m_WaterDiffuseOriginal_Multiply, 0f, 5f);
			this.m_WaterDiffuse_UMoveSpeed = GUI.HorizontalSlider(new Rect(125f, 150f, 150f, 15f), this.m_WaterDiffuseOriginal_UMoveSpeed, -6f, 6f);
			this.m_WaterDiffuse_VMoveSpeed = GUI.HorizontalSlider(new Rect(125f, 170f, 150f, 15f), this.m_WaterDiffuseOriginal_VMoveSpeed, -6f, 6f);
		}
	}

	private void WaterHeightmapGUIWindow(int id)
	{
		GUI.Label(new Rect(10f, 25f, 90f, 22f), "Texture Tile");
		this.m_WaterHeightmap_WaterTile = GUI.HorizontalSlider(new Rect(125f, 30f, 150f, 15f), this.m_WaterHeightmap_WaterTile, 0.25f, 5f);
		if (this.m_WaterHeightmapOld_WaterTile != this.m_WaterHeightmap_WaterTile)
		{
			this.PauseOrbitCamera();
			this.m_WaterHeightmapOld_WaterTile = this.m_WaterHeightmap_WaterTile;
			this.m_WaterList[this.m_CurrentWaterIndex].gameObject.GetComponent<Renderer>().material.SetFloat("_MainTexTile", this.m_WaterHeightmap_WaterTile);
		}
		GUI.Label(new Rect(10f, 55f, 230f, 22f), "HeightMap 1: " + this.m_HeightMapTextures[this.m_WaterHeightMap1Index].name);
		if (GUI.Button(new Rect(245f, 55f, 20f, 22f), "<"))
		{
			this.m_WaterHeightMap1Index--;
			if (this.m_WaterHeightMap1Index < 0)
			{
				this.m_WaterHeightMap1Index = this.m_HeightMapTextures.Length - 1;
			}
			this.m_WaterList[this.m_CurrentWaterIndex].gameObject.GetComponent<Renderer>().material.SetTexture("_HeightMap1", this.m_HeightMapTextures[this.m_WaterHeightMap1Index]);
		}
		if (GUI.Button(new Rect(265f, 55f, 20f, 22f), ">"))
		{
			this.m_WaterHeightMap1Index++;
			if (this.m_WaterHeightMap1Index >= this.m_HeightMapTextures.Length - 1)
			{
				this.m_WaterHeightMap1Index = 0;
			}
			this.m_WaterList[this.m_CurrentWaterIndex].gameObject.GetComponent<Renderer>().material.SetTexture("_HeightMap1", this.m_HeightMapTextures[this.m_WaterHeightMap1Index]);
		}
		GUI.Label(new Rect(10f, 75f, 230f, 22f), "HeightMap 2: " + this.m_HeightMapTextures[this.m_WaterHeightMap2Index].name);
		if (GUI.Button(new Rect(245f, 75f, 20f, 22f), "<"))
		{
			this.m_WaterHeightMap2Index--;
			if (this.m_WaterHeightMap2Index < 0)
			{
				this.m_WaterHeightMap2Index = this.m_HeightMapTextures.Length - 1;
			}
			this.m_WaterList[this.m_CurrentWaterIndex].gameObject.GetComponent<Renderer>().material.SetTexture("_HeightMap2", this.m_HeightMapTextures[this.m_WaterHeightMap2Index]);
		}
		if (GUI.Button(new Rect(265f, 75f, 20f, 22f), ">"))
		{
			this.m_WaterHeightMap2Index++;
			if (this.m_WaterHeightMap2Index >= this.m_HeightMapTextures.Length - 1)
			{
				this.m_WaterHeightMap2Index = 0;
			}
			this.m_WaterList[this.m_CurrentWaterIndex].gameObject.GetComponent<Renderer>().material.SetTexture("_HeightMap2", this.m_HeightMapTextures[this.m_WaterHeightMap2Index]);
		}
		GUI.Label(new Rect(10f, 95f, 90f, 22f), "HeightMap Tile");
		this.m_WaterHeightmap_HeightmapTile = GUI.HorizontalSlider(new Rect(125f, 100f, 150f, 15f), this.m_WaterHeightmap_HeightmapTile, 0.25f, 5f);
		if (this.m_WaterHeightmapOld_HeightmapTile != this.m_WaterHeightmap_HeightmapTile)
		{
			this.PauseOrbitCamera();
			this.m_WaterHeightmapOld_HeightmapTile = this.m_WaterHeightmap_HeightmapTile;
			this.m_WaterList[this.m_CurrentWaterIndex].gameObject.GetComponent<Renderer>().material.SetFloat("_HeightMapTile", this.m_WaterHeightmap_HeightmapTile);
		}
		GUI.Label(new Rect(10f, 125f, 90f, 22f), "Refraction");
		this.m_WaterHeightmap_Refraction = GUI.HorizontalSlider(new Rect(125f, 130f, 150f, 15f), this.m_WaterHeightmap_Refraction, 0.1f, 5f);
		if (this.m_WaterHeightmapOld_Refraction != this.m_WaterHeightmap_Refraction)
		{
			this.PauseOrbitCamera();
			this.m_WaterHeightmapOld_Refraction = this.m_WaterHeightmap_Refraction;
			this.m_WaterList[this.m_CurrentWaterIndex].gameObject.GetComponent<Renderer>().material.SetFloat("_MainTexRefraction", this.m_WaterHeightmap_Refraction);
		}
		GUI.Label(new Rect(10f, 145f, 90f, 22f), "Strength");
		this.m_WaterHeightmap_Strength = GUI.HorizontalSlider(new Rect(125f, 150f, 150f, 15f), this.m_WaterHeightmap_Strength, 0f, 5f);
		if (this.m_WaterHeightmapOld_Strength != this.m_WaterHeightmap_Strength)
		{
			this.PauseOrbitCamera();
			this.m_WaterHeightmapOld_Strength = this.m_WaterHeightmap_Strength;
			this.m_WaterList[this.m_CurrentWaterIndex].gameObject.GetComponent<Renderer>().material.SetFloat("_HeightMapStrength", this.m_WaterHeightmap_Strength);
		}
		GUI.Label(new Rect(10f, 165f, 90f, 22f), "Multiply");
		this.m_WaterHeightmap_Multiply = GUI.HorizontalSlider(new Rect(125f, 170f, 150f, 15f), this.m_WaterHeightmap_Multiply, 0.05f, 0.5f);
		if (this.m_WaterHeightmapOld_Multiply != this.m_WaterHeightmap_Multiply)
		{
			this.PauseOrbitCamera();
			this.m_WaterHeightmapOld_Multiply = this.m_WaterHeightmap_Multiply;
			this.m_WaterList[this.m_CurrentWaterIndex].gameObject.GetComponent<Renderer>().material.SetFloat("_HeightMapMultiply", this.m_WaterHeightmap_Multiply);
		}
		if (GUI.Button(new Rect(125f, 200f, 150f, 30f), "Reset"))
		{
			this.m_WaterList[this.m_CurrentWaterIndex].gameObject.GetComponent<Renderer>().material.SetFloat("_MainTexTile", this.m_WaterHeightmapOriginal_WaterTile);
			this.m_WaterList[this.m_CurrentWaterIndex].gameObject.GetComponent<Renderer>().material.SetFloat("_HeightMapTile", this.m_WaterHeightmapOriginal_HeightmapTile);
			this.m_WaterList[this.m_CurrentWaterIndex].gameObject.GetComponent<Renderer>().material.SetFloat("_MainTexRefraction", this.m_WaterHeightmapOriginal_Refraction);
			this.m_WaterList[this.m_CurrentWaterIndex].gameObject.GetComponent<Renderer>().material.SetFloat("_HeightMapStrength", this.m_WaterHeightmapOriginal_Strength);
			this.m_WaterList[this.m_CurrentWaterIndex].gameObject.GetComponent<Renderer>().material.SetFloat("_HeightMapMultiply", this.m_WaterHeightmapOriginal_Multiply);
			this.m_WaterHeightmap_WaterTile = GUI.HorizontalSlider(new Rect(125f, 30f, 150f, 15f), this.m_WaterHeightmapOriginal_WaterTile, 0.25f, 5f);
			this.m_WaterHeightmap_HeightmapTile = GUI.HorizontalSlider(new Rect(125f, 100f, 150f, 15f), this.m_WaterHeightmapOriginal_HeightmapTile, 0.25f, 5f);
			this.m_WaterHeightmap_Refraction = GUI.HorizontalSlider(new Rect(125f, 130f, 150f, 15f), this.m_WaterHeightmapOriginal_Refraction, 0.1f, 5f);
			this.m_WaterHeightmap_Strength = GUI.HorizontalSlider(new Rect(125f, 150f, 150f, 15f), this.m_WaterHeightmapOriginal_Strength, 0f, 5f);
			this.m_WaterHeightmap_Multiply = GUI.HorizontalSlider(new Rect(125f, 170f, 150f, 15f), this.m_WaterHeightmapOriginal_Multiply, 0.05f, 0.5f);
		}
	}

	private void WaterSimpleGUIWindow(int id)
	{
		GUI.Label(new Rect(10f, 25f, 100f, 22f), "Ambient Red");
		GUI.Label(new Rect(10f, 45f, 100f, 22f), "Ambient Green");
		GUI.Label(new Rect(10f, 65f, 100f, 22f), "Ambient Blue");
		GUI.Label(new Rect(10f, 85f, 100f, 22f), "Ambient Alpha");
		this.m_WaterSimple_Ambient = new Color(GUI.HorizontalSlider(new Rect(125f, 30f, 150f, 15f), RenderSettings.ambientLight.r, 0f, 1f), GUI.HorizontalSlider(new Rect(125f, 50f, 150f, 15f), RenderSettings.ambientLight.g, 0f, 1f), GUI.HorizontalSlider(new Rect(125f, 70f, 150f, 15f), RenderSettings.ambientLight.b, 0f, 1f), 1f);
		if (this.m_WaterSimpleOld_Ambient != this.m_WaterSimple_Ambient)
		{
			this.PauseOrbitCamera();
			this.m_WaterSimpleOld_Ambient = this.m_WaterSimple_Ambient;
			RenderSettings.ambientLight = this.m_WaterSimple_Ambient;
		}
		if (GUI.Button(new Rect(115f, 87f, 170f, 18f), "Transparancy and more"))
		{
			Application.OpenURL("https://www.assetstore.unity3d.com/#!/content/26430");
		}
		GUI.Label(new Rect(10f, 125f, 65f, 22f), "U Speed");
		this.m_WaterSimple_UMoveSpeed = GUI.HorizontalSlider(new Rect(125f, 130f, 150f, 15f), this.m_WaterSimple_UMoveSpeed, -6f, 6f);
		if (this.m_WaterSimpleOld_UMoveSpeed != this.m_WaterSimple_UMoveSpeed)
		{
			this.PauseOrbitCamera();
			this.m_WaterSimpleOld_UMoveSpeed = this.m_WaterSimple_UMoveSpeed;
			this.m_WaterList[this.m_CurrentWaterIndex].gameObject.GetComponent<Renderer>().material.SetFloat("_MainTexMoveSpeedU", this.m_WaterSimple_UMoveSpeed);
		}
		GUI.Label(new Rect(10f, 145f, 65f, 22f), "V Speed");
		this.m_WaterSimple_VMoveSpeed = GUI.HorizontalSlider(new Rect(125f, 150f, 150f, 15f), this.m_WaterSimple_VMoveSpeed, -6f, 6f);
		if (this.m_WaterSimpleOld_VMoveSpeed != this.m_WaterSimple_VMoveSpeed)
		{
			this.PauseOrbitCamera();
			this.m_WaterSimpleOld_VMoveSpeed = this.m_WaterSimple_VMoveSpeed;
			this.m_WaterList[this.m_CurrentWaterIndex].gameObject.GetComponent<Renderer>().material.SetFloat("_MainTexMoveSpeedV", this.m_WaterSimple_VMoveSpeed);
		}
		if (GUI.Button(new Rect(125f, 175f, 150f, 30f), "Reset"))
		{
			RenderSettings.ambientLight = this.m_WaterSimpleOriginal_Ambient;
			this.m_WaterList[this.m_CurrentWaterIndex].gameObject.GetComponent<Renderer>().material.SetFloat("_MainTexMoveSpeedU", this.m_WaterSimpleOriginal_UMoveSpeed);
			this.m_WaterList[this.m_CurrentWaterIndex].gameObject.GetComponent<Renderer>().material.SetFloat("_MainTexMoveSpeedV", this.m_WaterSimpleOriginal_VMoveSpeed);
			this.m_WaterSimple_Ambient = new Color(GUI.HorizontalSlider(new Rect(125f, 30f, 150f, 15f), this.m_WaterSimpleOriginal_Ambient.r, 0f, 1f), GUI.HorizontalSlider(new Rect(125f, 50f, 150f, 15f), this.m_WaterSimpleOriginal_Ambient.g, 0f, 1f), GUI.HorizontalSlider(new Rect(125f, 70f, 150f, 15f), this.m_WaterSimpleOriginal_Ambient.b, 0f, 1f), GUI.HorizontalSlider(new Rect(125f, 90f, 150f, 15f), this.m_WaterSimpleOriginal_Ambient.a, 0f, 1f));
			this.m_WaterSimple_UMoveSpeed = GUI.HorizontalSlider(new Rect(125f, 130f, 150f, 15f), this.m_WaterSimpleOriginal_UMoveSpeed, -6f, 6f);
			this.m_WaterSimple_VMoveSpeed = GUI.HorizontalSlider(new Rect(125f, 150f, 150f, 15f), this.m_WaterSimpleOriginal_VMoveSpeed, -6f, 6f);
		}
	}

	private void UpdateCurrentShaderType()
	{
		if (this.m_WaterList[this.m_CurrentWaterIndex].gameObject.GetComponent<Renderer>() == null)
		{
			return;
		}
		if (this.m_WaterList[this.m_CurrentWaterIndex].gameObject.GetComponent<Renderer>().material == null)
		{
			return;
		}
		string name = this.m_WaterList[this.m_CurrentWaterIndex].gameObject.GetComponent<Renderer>().material.shader.name;
		if (name != null)
		{
			if (name == "Water Flow/Water Diffuse")
			{
				this.m_CurrentWaterShaderType = eWaterShaderType.WaterDiffuse;
				this.m_WaterDiffuse_Diffuse = this.m_WaterList[this.m_CurrentWaterIndex].gameObject.GetComponent<Renderer>().material.GetColor("_MainTexColor");
				this.m_WaterDiffuse_Multiply = this.m_WaterList[this.m_CurrentWaterIndex].gameObject.GetComponent<Renderer>().material.GetFloat("_MainTexMultiply");
				this.m_WaterDiffuse_UMoveSpeed = this.m_WaterList[this.m_CurrentWaterIndex].gameObject.GetComponent<Renderer>().material.GetFloat("_MainTexMoveSpeedU");
				this.m_WaterDiffuse_VMoveSpeed = this.m_WaterList[this.m_CurrentWaterIndex].gameObject.GetComponent<Renderer>().material.GetFloat("_MainTexMoveSpeedV");
				this.m_WaterDiffuseOld_Diffuse = this.m_WaterDiffuse_Diffuse;
				this.m_WaterDiffuseOld_Multiply = this.m_WaterDiffuse_Multiply;
				this.m_WaterDiffuseOld_UMoveSpeed = this.m_WaterDiffuse_UMoveSpeed;
				this.m_WaterDiffuseOld_VMoveSpeed = this.m_WaterDiffuse_VMoveSpeed;
				return;
			}
			if (name == "Water Flow/Water Heightmap")
			{
				this.m_CurrentWaterShaderType = eWaterShaderType.WaterHeightmap;
				Texture texture = this.m_WaterList[this.m_CurrentWaterIndex].gameObject.GetComponent<Renderer>().material.GetTexture("_HeightMap1");
				Texture texture2 = this.m_WaterList[this.m_CurrentWaterIndex].gameObject.GetComponent<Renderer>().material.GetTexture("_HeightMap2");
				for (int i = 0; i < this.m_HeightMapTextures.Length; i++)
				{
					if (this.m_HeightMapTextures[i].name == texture.name)
					{
						this.m_WaterHeightMap1Index = i;
					}
					if (this.m_HeightMapTextures[i].name == texture2.name)
					{
						this.m_WaterHeightMap2Index = i;
					}
				}
				this.m_WaterHeightmap_WaterTile = this.m_WaterList[this.m_CurrentWaterIndex].gameObject.GetComponent<Renderer>().material.GetFloat("_MainTexTile");
				this.m_WaterHeightmap_HeightmapTile = this.m_WaterList[this.m_CurrentWaterIndex].gameObject.GetComponent<Renderer>().material.GetFloat("_HeightMapTile");
				this.m_WaterHeightmap_Refraction = this.m_WaterList[this.m_CurrentWaterIndex].gameObject.GetComponent<Renderer>().material.GetFloat("_MainTexRefraction");
				this.m_WaterHeightmap_Strength = this.m_WaterList[this.m_CurrentWaterIndex].gameObject.GetComponent<Renderer>().material.GetFloat("_HeightMapStrength");
				this.m_WaterHeightmap_Multiply = this.m_WaterList[this.m_CurrentWaterIndex].gameObject.GetComponent<Renderer>().material.GetFloat("_HeightMapMultiply");
				this.m_WaterHeightmapOld_WaterTile = this.m_WaterHeightmap_WaterTile;
				this.m_WaterHeightmapOld_HeightmapTile = this.m_WaterHeightmap_HeightmapTile;
				this.m_WaterHeightmapOld_Refraction = this.m_WaterHeightmap_Refraction;
				this.m_WaterHeightmapOld_Strength = this.m_WaterHeightmap_Strength;
				this.m_WaterHeightmapOld_Multiply = this.m_WaterHeightmap_Multiply;
				return;
			}
			if (name == "Water Flow/Water Simple")
			{
				this.m_CurrentWaterShaderType = eWaterShaderType.WaterSimple;
				this.m_WaterSimple_Ambient = RenderSettings.ambientLight;
				this.m_WaterSimple_UMoveSpeed = this.m_WaterList[this.m_CurrentWaterIndex].gameObject.GetComponent<Renderer>().material.GetFloat("_MainTexMoveSpeedU");
				this.m_WaterSimple_VMoveSpeed = this.m_WaterList[this.m_CurrentWaterIndex].gameObject.GetComponent<Renderer>().material.GetFloat("_MainTexMoveSpeedV");
				this.m_WaterSimpleOld_Ambient = this.m_WaterSimple_Ambient;
				this.m_WaterSimpleOld_UMoveSpeed = this.m_WaterSimple_UMoveSpeed;
				this.m_WaterSimpleOld_VMoveSpeed = this.m_WaterSimple_VMoveSpeed;
				return;
			}
		}
		this.m_CurrentWaterShaderType = eWaterShaderType.None;
	}

	private void PauseOrbitCamera()
	{
		if (this.m_OrbitCamera.enabled)
		{
			this.m_OrbitCamera.enabled = false;
		}
	}
}
