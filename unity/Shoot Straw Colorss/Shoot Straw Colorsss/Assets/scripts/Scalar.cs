using UnityEngine;

public class Scalar : MonoBehaviour
{
    Vector3 dPos,pPos;
    bool begin;
    private void Start()
    {
        dPos = transform.position + new Vector3(0, 0, 1f);
        pPos = transform.position;
    }
    private void Update()
    {
        if (transform.position == dPos)
            begin = false;
        if (begin)
        {
            transform.position = Vector3.MoveTowards(transform.position, dPos, Time.deltaTime * 10);
            //transform.Translate(0, 0, 1, Space.Self);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, pPos, Time.deltaTime * 5);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
            begin = true;
    }
}
