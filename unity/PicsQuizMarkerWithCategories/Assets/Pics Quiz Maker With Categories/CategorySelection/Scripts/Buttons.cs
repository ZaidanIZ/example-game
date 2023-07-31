using UnityEngine;
using System.Collections;

public class Buttons : MonoBehaviour {

	public void GoToLevels () {
        GameObject.Find("Main Camera").transform.position = new Vector3(-11, 0, -10);
        Application.LoadLevel(Application.loadedLevelName);
	}

    public void GoToCategories()
    {
        GameObject.Find("Main Camera").transform.position = new Vector3(-22, 0, -10);
        Application.LoadLevel(Application.loadedLevelName);
    }

    public void GoToMenu()
    {
        GameObject.Find("Main Camera").transform.position = new Vector3(-32, 0, -10);
        Application.LoadLevel(Application.loadedLevelName);
    }

}
