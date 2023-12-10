using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainText : MonoBehaviour
{

    public static MainText instance { get; private set; }

    public int coinCount = 0;
    public bool GameOver = false;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    TextMeshProUGUI textMesh;

    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameOver)
        {
            textMesh.text = coinCount.ToString();
        }
        else
        {
            textMesh.text = "You win!";
        }
    }
}
