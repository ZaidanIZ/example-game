using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pindah : MonoBehaviour
{
    public void LoadToScene(string SceneName) 
    {
        SceneManager.LoadScene(SceneName);
    }
}
