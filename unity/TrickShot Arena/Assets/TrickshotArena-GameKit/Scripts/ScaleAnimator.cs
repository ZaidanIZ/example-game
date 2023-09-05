using UnityEngine;
using System.Collections;

namespace TrickshotArena
{
    public class PlayerCircleRotator : MonoBehaviour
    {
        void Update()
        {
            transform.Rotate(new Vector3(0, 0, -90 * Time.deltaTime));
            transform.localScale = new Vector3(5f, 5f, 0.01f);
        }
    }
}