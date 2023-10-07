using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortOrderSprite : MonoBehaviour
{
    [Tooltip("Value increases or decreases to sort order of spriteRenderer")]
    public int parameter;

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!GameController.Instance.GameOver)
        {
            if (col.tag == "Check" || col.tag == "ItemCheck")
            {
                if (col.transform.parent.transform.position.y <= transform.position.y)
                {
                    spriteRenderer.sortingOrder = col.GetComponentInParent<SpriteRenderer>().sortingOrder - parameter;
                }
                else
                {
                    spriteRenderer.sortingOrder = col.GetComponentInParent<SpriteRenderer>().sortingOrder + parameter;
                }
            }
        }
    }
}
