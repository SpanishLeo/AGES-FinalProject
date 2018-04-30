using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public static SpawnPoint currentCheckpoint;

    private SpriteRenderer spriteRenderer;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        DeactivateCheckpoint();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (currentCheckpoint != this)
            {
                if (currentCheckpoint != null)
                {
                    currentCheckpoint.DeactivateCheckpoint();
                }
                ActivateCheckpoint();
            }
        }
    }

    private void ActivateCheckpoint()
    {
        Debug.Log("Checkpoint Reached");
        spriteRenderer.enabled = true;
        audioSource.Play();
        currentCheckpoint = this;
    }

    private void DeactivateCheckpoint()
    {
        spriteRenderer.enabled = false;
    }
}
