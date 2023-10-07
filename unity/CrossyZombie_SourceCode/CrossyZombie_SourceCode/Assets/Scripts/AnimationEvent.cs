using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    // Animation event
    public void Disable()
    {
        this.gameObject.SetActive(false);
    }
}
