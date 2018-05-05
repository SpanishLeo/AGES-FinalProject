using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    private Rigidbody2D myRigidBody;
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;

	void Start ()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            audioSource.Play();

            myRigidBody.isKinematic = false;
        }
    }
}
