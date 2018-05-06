using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static event Action<PlayerController> PlayerDied;

    #region Editor Fields
    [SerializeField]
    private float playerMoveSpeed = 8;
    [SerializeField]
    private float playerJumpHeight = 6;
    [SerializeField]
    private Transform groundCheckPosition;
    [SerializeField]
    private float groundCheckRadius = 0.35f;
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private int playerNumber = 1;
    [SerializeField]
    private AudioSource jumpAudioSource;
    [SerializeField]
    private AudioSource hitAudioSource;
    #endregion

    #region Private Fields
    private float horizontalInput;
    private bool pressedJump;
    private bool isOnGround;
    private bool facingRight = true;
    private Rigidbody2D playerRigidbody2D;
    private Animator playerAnimator;
    private BoxCollider2D playerBoxCollider2D;
    private CircleCollider2D playerCircleCollider2D;
    private AudioSource[] playerSounds;
    #endregion

    public bool IsAlive { get; set; }

    public int PlayerNumber
    { 
        get { return playerNumber; } 
    }

    void Start ()
    {
        IsAlive = true;
        playerRigidbody2D = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerBoxCollider2D = GetComponent<BoxCollider2D>();
        playerCircleCollider2D = GetComponent<CircleCollider2D>();
        playerSounds = GetComponents<AudioSource>();
        jumpAudioSource = playerSounds[0];
        hitAudioSource = playerSounds[1];
	}
	
	void Update ()
    {
        GetMovementInput();
        GetJumpInput();
        UpdateIsOnGround();

        if (IsAlive == true)
        {
            HandlePlayerJump();
            HandlePlayerMovement();
            HandlePlayerAnimation();
        }
	}


    private void GetMovementInput()
    {
        horizontalInput = Input.GetAxis("Horizontal" + playerNumber);
    }

    private void GetJumpInput()
    {
        pressedJump = Input.GetButtonDown("Jump" + playerNumber);
    }

    private void HandlePlayerMovement()
    {
        playerRigidbody2D.velocity = new Vector2(playerMoveSpeed * horizontalInput, playerRigidbody2D.velocity.y);

        if (horizontalInput > 0 && !facingRight)
        {
            Flip();
        }
        else if(horizontalInput < 0 && facingRight)
        {
            Flip();
        }
    }

    private void HandlePlayerJump()
    {
        if (pressedJump && isOnGround)
        {
            playerRigidbody2D.velocity = new Vector2(playerRigidbody2D.velocity.x, playerJumpHeight);

            jumpAudioSource.Play();

            isOnGround = false;
        }
    }

    private void HandlePlayerAnimation()
    {
        playerAnimator.SetFloat("Speed", Mathf.Abs(horizontalInput));

        playerAnimator.SetFloat("vSpeed", playerRigidbody2D.velocity.y);

        playerAnimator.SetBool("Ground", isOnGround);
    }

    public void TakeDamage()
    {
        Debug.Log("Player " + playerNumber + " Taking damage!");

        hitAudioSource.Play();

        IsAlive = false;

        //disable player rigidbody and colliders
        //this way they fall over and off the map
        //also doesnt allow multiple damage to be taken
        playerRigidbody2D.constraints = RigidbodyConstraints2D.None;
        playerBoxCollider2D.enabled = false;
        playerCircleCollider2D.enabled = false;

        if (PlayerDied != null)
        {
            PlayerDied.Invoke(this);
        }
    }

    private void UpdateIsOnGround()
    {
        Collider2D[] groundColliders = Physics2D.OverlapCircleAll(groundCheckPosition.position, groundCheckRadius, whatIsGround);

        isOnGround = groundColliders.Length > 0;
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
