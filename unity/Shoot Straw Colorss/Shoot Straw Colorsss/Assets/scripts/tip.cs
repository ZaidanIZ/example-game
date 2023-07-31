using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tip : MonoBehaviour {

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Destroy(gameObject,0.3f);
        }
    }
}
