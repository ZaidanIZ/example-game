using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour {
    public bool go;
    int noc,d;
    public float pos;
    //Vector3 dRot;
	// Use this for initialization
	void Start () {
        d=Random.Range(0, 1);
        if (d == 0)
            d = 10;
        else if (d == 1)
            d = -10;
        noc = transform.childCount;
        pos = transform.position.z;
        //dRot = new Vector3(0,0,5);
    }
	// Update is called once per frame
	void Update () {
       // transform.eulerAngles = Vector3.MoveTowards(transform.eulerAngles, dRot, Time.deltaTime * 250f);
        if (noc > transform.childCount)
        {
            transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + d);
            //dRot = new Vector3(0, 0, transform.eulerAngles.z + d);
            noc = transform.childCount;
        }
        if (transform.childCount == 1)
        {
            go = true;
        }
	}
}
