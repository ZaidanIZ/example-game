﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class bullet : MonoBehaviour {

    GameObject temp;
    Vector3 cScale;
    float max=15;
    Rigidbody rb;
    GameObject movement;
    public GameObject failParticles, keysParticles, failPanel;
    Vector3 dScale;
    public Randomize randomize;
    Interstitial interstitial;


    // Use this for initialization
    void Start () {
        movement = FindObjectOfType<move>().gameObject;
        Destroy(gameObject, 0.5f);
        rb=GetComponent<Rigidbody>();
        dScale = new Vector3(2.11f, 2.11f, 2.1f);
    }

    // Update is called once per frame
    void Update () {

        rb.velocity = Vector3.ClampMagnitude(rb.velocity, max);
	}
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "keys")
        {
            Score.ScoreValue += 1;
            TotalScore.totalValue += 1;
            col.gameObject.GetComponent<MeshCollider>().enabled = false;
            Instantiate(keysParticles, new Vector3(transform.position.x,transform.position.y,transform.position.z), Quaternion.identity);
            col.gameObject.GetComponent<MeshRenderer>().material = GetComponent<MeshRenderer>().material;

            GetComponent<SphereCollider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
            Destroy(gameObject, 1f);
            col.transform.parent = col.transform.parent.transform.GetChild(col.transform.parent.transform.childCount - 1);
        }
        if (col.gameObject.tag == "enemy")
        {
            Instantiate(failParticles, transform.position, Quaternion.identity);
            if(PlayerPrefs.GetInt("mute")==0)
                Handheld.Vibrate();
            Instantiate(failPanel);
            movement.GetComponent<move>().enabled = false;
            Destroy(gameObject);

            //random number
            randomize.Randomz();
            //randomm();
            


        }
    }

    //private void randomm()
    //{
    //    int[] numbers = { 1, 2 };
    //    int randomIndex = Random.Range(0, numbers.Length);
    //    int randomNumber = numbers[randomIndex];

    //    Debug.Log(randomNumber);

    //    if (randomNumber == 1)
    //    {
    //        //randomize.adrand();
    //        Debug.Log("ad Muncul");
    //        //Advertisement.Show("interstitial");
    //    }
    //}
    
}
