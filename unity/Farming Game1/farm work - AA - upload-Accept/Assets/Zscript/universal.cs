using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class universal : MonoBehaviour
{
    public GameObject btnAds;
    public static universal univers;

    private void Awake()
    {
        if (univers == null)
        {
            univers = this;
        }
        DontDestroyOnLoad(this);
        if (univers != this)
        {
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
