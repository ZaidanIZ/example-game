using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class credit : MonoBehaviour {

    public GameObject panel;
    bool a=false,on = true;
    Animator anim;
	void Start () {
        
	}
	public void click()
    {
        if (!on)
        {
            on = true;
            anim = panel.GetComponent<Animator>();
            anim.Play("creditsPanel");
            Invoke("off", 0.5f);
        }
        else if (on)
        {
            on = false;
            panel.SetActive(true);
            anim = panel.GetComponent<Animator>();
            anim.Play("creditsPanelOn");
            
        }
    }
    void off()
    {
        panel.SetActive(false);
    }
}
