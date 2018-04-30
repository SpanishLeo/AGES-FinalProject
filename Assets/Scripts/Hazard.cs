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

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            DeactivatePlayer();
 
            PlayerRespawn playerRespawn = collision.gameObject.GetComponent<PlayerRespawn>();

            audioSource.Play();

            playerRespawn.Respawn();
        }
    }

    private void DeactivatePlayer()
    {
        playerRigidbody2D.constraints = RigidbodyConstraints2D.None;
        //diable rigigbody
    }
}
