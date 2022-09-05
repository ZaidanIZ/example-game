using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeMove : MonoBehaviour
{
    private Vector2 startTouchPosition, endTouchPosition;

    private Touch touch;

    private IEnumerator goCoroutine;
    private bool coroutineAllowed;
    
    private void Start()
    {
        coroutineAllowed = true;
    }

    private void Update()
    {
        if (Input.touchCount > 0) 
        {
            touch = Input.GetTouch(0);
        }

        if (touch.phase == TouchPhase.Began) 
        {
            startTouchPosition = touch.position;
        } 

        if (Input.touchCount > 0 && touch.phase == TouchPhase.Ended && coroutineAllowed) 
        {
            endTouchPosition = touch.position;

            if ((endTouchPosition.y >  startTouchPosition.y)
            && (Mathf.Abs(touch.deltaPosition.y) > Mathf.Abs(touch.deltaPosition.x))) 
            {
                goCoroutine = Go(new Vector3(0f, 0f, 0.25f));
                StartCoroutine(goCoroutine);
            }

            else if ((endTouchPosition.y < startTouchPosition.y)
            && (Mathf.Abs(touch.deltaPosition.y) > Mathf.Abs(touch.deltaPosition.x))) 
            {
                goCoroutine = Go(new Vector3(0f, 0f, -0.25f));
                StartCoroutine(goCoroutine);
            }

            else if ((endTouchPosition.x > startTouchPosition.x)
            && (Mathf.Abs(touch.deltaPosition.x) > Mathf.Abs(touch.deltaPosition.y))) 
            {
                goCoroutine = Go(new Vector3(0.25f, 0f, 0f));
                StartCoroutine(goCoroutine);
            }

            else if ((endTouchPosition.x < startTouchPosition.x)
            && (Mathf.Abs(touch.deltaPosition.x) > Mathf.Abs(touch.deltaPosition.y))) 
            {
                goCoroutine = Go(new Vector3(-0.25f, 0f, 0f));
                StartCoroutine(goCoroutine);
            }
        }
    }

    private IEnumerator Go(Vector3 direction) 
    {
        coroutineAllowed = false;

        for (int i = 0; i <= 2; i++) 
        {
            transform.Translate(direction);
            yield return new WaitForSeconds(0.01f);
        }

        for (int i = 0; i <= 2; i++) 
        {
            transform.Translate(direction);
            yield return new WaitForSeconds(0.01f);
        }

        transform.Translate(direction);

        coroutineAllowed = true;
    }
}
