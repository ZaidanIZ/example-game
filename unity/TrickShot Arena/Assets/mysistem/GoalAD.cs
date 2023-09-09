using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalAD : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start coll");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ball") && collision.gameObject.CompareTag("opponentGoalTrigger"))
        {
            //Randomize.randomizee.Randomz();
            Debug.Log("goal");
        }
    }

}
