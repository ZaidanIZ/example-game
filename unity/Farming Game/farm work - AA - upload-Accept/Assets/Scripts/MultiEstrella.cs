using System;
using UnityEngine;

[Serializable]
public class MultiEstrella : MonoBehaviour
{
	public Transform prefab;

	public void Emitir()
	{
		for (int i = 0; i < 100; i++)
		{
			Transform transform = UnityEngine.Object.Instantiate<Transform>(this.prefab, this.transform.position + this.transform.forward * 1.2f, this.transform.rotation);
			transform.transform.SetParent(this.gameObject.transform);
		}
	}

	public void Main()
	{
	}
}
