using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public AudioSource
        back,
        genericBtn,
        getCoinAndDiamond,
        buy,
        getItems,
        hurt,
        death,
        timer,
        powerDown,
        bgDay,
        bgNight,
        bgMusic,
        bgVar1,
        bgVar2,
        bgVar3,
        bgVar4;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(this.gameObject);
        }
    }
}
