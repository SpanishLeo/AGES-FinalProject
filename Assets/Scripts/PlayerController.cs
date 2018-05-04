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
    #endregion

    #region Private Fields
    private float horizontalInput;
    private bool pressedJump;
    private bool pressedAttack;
    private bool isOnGround;
    private bool facingRight = true;
    private Rigidbody2D playerRigidbody2D;
    private AudioSource audioSource;
    private Animator animator;
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
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
	}
	
	void Update ()
    {
        GetMovementInput();
        GetJumpInput();
        GetAttackInput();
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

    private void GetAttackInput()
    {
        pressedAttack = Input.GetButtonDown("Fire" + playerNumber);
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

            audioSource.Play();

            isOnGround = false;
        }
    }

    private void HandlePlayerAnimation()
    {
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));

        animator.SetFloat("vSpeed", playerRigidbody2D.velocity.y);

        animator.SetBool("Ground", isOnGround);
    }

    public void TakeDamage()
    {
        Debug.Log("Taking damage!");

        IsAlive = false;

        playerRigidbody2D.constraints = RigidbodyConstraints2D.None;

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
