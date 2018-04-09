using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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
    private Collider2D attackRangeCheck;
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

    void Start ()
    {
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
	}

    private void FixedUpdate()
    {
        HandlePlayerMovement();
        HandlePlayerJump();
        HandlePlayerAttack();
    }

    private void GetMovementInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
    }

    private void GetJumpInput()
    {
        pressedJump = Input.GetButtonDown("Jump");
    }

    private void GetAttackInput()
    {
        pressedAttack = Input.GetButton("Fire1");
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

            //audioSource.Play();

            isOnGround = false;
        }
    }

    private void HandlePlayerAttack()
    {
        

        if (pressedAttack && isOnGround)
        {

        }
    }

    private void UpdateIsOnGround()
    {
        Collider2D[] groundColliders = Physics2D.OverlapCircleAll(groundCheckPosition.position, groundCheckRadius, whatIsGround);

        isOnGround = groundColliders.Length > 0;
    }

    private void HandlePlayerAnimation()
    {

    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }


}
