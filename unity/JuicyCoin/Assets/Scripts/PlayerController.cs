using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb2D;

    public float speed;
    public float jumpheight;

    public LayerMask groundmask;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        bool grounded = Physics2D.Raycast(transform.position, Vector2.down, 1, groundmask);
        if (grounded)
        {
            Debug.Log("on Ground!");
        }
        else
        {
            Debug.Log("Not grounded!");
        }

        float movement = Input.GetAxis("Horizontal");
        float jump = Input.GetKeyDown(KeyCode.Space) && grounded ? 1.0f * jumpheight : rb2D.velocity.y;
        rb2D.velocity = new Vector2(movement * speed, jump);
    }
}
