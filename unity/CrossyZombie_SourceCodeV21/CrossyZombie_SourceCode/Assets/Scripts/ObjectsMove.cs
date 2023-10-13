using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsMove : MonoBehaviour
{
    private float
        speed,
        endPos,
        startPos;

    private int index;

    public void SetstartPosAndEndPos()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Vector3.zero);

        float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        float offset = gameObject.GetComponentInChildren<SpriteRenderer>().bounds.size.x / 2;

        endPos = worldPoint.x - offset;
        startPos = worldPoint.x + worldScreenWidth + offset;
    }

    void Update()
    {
        if (!GameController.Instance.Paused)
        {
            if (this.tag == "Zombie")
            {
                speed = GameController.Instance.zombieSpeed[index];
            }
            else
            {
                speed = GameController.Instance.zombieSpeed[0];
            }

            transform.position -= new Vector3(speed, 0.0f, 0.0f);

            if (transform.position.x <= endPos)
            {
                this.gameObject.SetActive(false);
            }
        }
    }

    void OnEnable()
    {
        if (this.tag == "Zombie")
        {
            index = Random.Range(0, GameController.Instance.zombieSpeed.Length - 1);
        }

        float randomPos = Random.Range(GameController.Instance.objectMinYPos, GameController.Instance.objectMaxYPos);
        transform.position = new Vector3(startPos, randomPos, 0.0f);
    }
}
