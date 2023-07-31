using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class adsByDead : MonoBehaviour
{
    public myAds MA;
    // Start is called before the first frame update
    public static adsByDead adb;
    private void Awake()
    {
        if (adb == null)
        {
            adb = this;
        }
        else {

            if (adb != this) {
                Destroy(this.gameObject);
            }
        }
    }
    void Start()
    {
        MA = myAds.misal;
    }
    
    private void OnEnable()
    {
        MA = myAds.misal;
        MA.CountToAds();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
