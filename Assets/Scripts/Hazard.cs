using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hazard : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D playerRigidbody2D;

    private AudioSource audioSource;
    private GameObject player;
    private PlayerController playerController;
    private PlayerController playerHit;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = GetComponent<PlayerController>();
        playerHit = player.GetComponent<PlayerController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Hazard Hit Player");

            audioSource.Play();

            if (playerHit != null)
            {
                playerHit.TakeDamage();
            }
        }
    }
}
