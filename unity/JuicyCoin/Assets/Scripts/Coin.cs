using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    Animator animator;
    AudioSource AS;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CollectCoin();
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        AS = GetComponent<AudioSource>();
    }

    private void CollectCoin()
    {
        MainText.instance.coinCount++;
        animator.SetTrigger("Collect");
        AS.Play();
    }
}
