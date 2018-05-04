using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hazard : MonoBehaviour
{
    public static event Action<PlayerController> PlayerDied;

    [SerializeField]
    private Rigidbody2D playerRigidbody2D;

    private AudioSource audioSource;
    private GameObject player;
    private PlayerController playerController;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = GetComponent<PlayerController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Hazard Hit Player");

            audioSource.Play();

            if (PlayerDied != null)
            {
                PlayerDied.Invoke(playerController);
            }
        }
    }

    private void DeactivatePlayer()
    {
        playerRigidbody2D.constraints = RigidbodyConstraints2D.None;
        //disable rigigbody
    }
}
