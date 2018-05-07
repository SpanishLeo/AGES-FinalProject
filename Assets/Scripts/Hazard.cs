using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    private GameObject player;
    private PlayerController playerHit;

    private void Start()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController playerControllerHit = collision.gameObject.GetComponent<PlayerController>();

            Debug.Log("Hazard Hit Player");

            if (playerControllerHit != null)
            {
                playerControllerHit.TakeDamage();
            }
        }
    }
}