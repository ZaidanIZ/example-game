using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    public new MeshRenderer renderer;

    private float posX = 0.0f;

    void Update()
    {
        if (!GameController.Instance.Paused)
        {
            if (this.tag == "BG")
            {
                posX += GameController.Instance.backgroundSpeed;
            }
            else if (this.tag == "FG")
            {
                posX += GameController.Instance.streetSpeed;
            }

            if (posX > 1.0f)
            {
                posX -= 1.0f;
            }

            renderer.material.mainTextureOffset = new Vector2(posX, 0.0f);
        }
    }
}
