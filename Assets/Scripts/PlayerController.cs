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
    [SerializeField]
    private GameObject opponent;
    #endregion

    #region Private Fields
    private float horizontalInput;
    private float attackTimer = 0;
    private float attackCoolDown = 0.3f;
    private bool pressedJump;
    private bool pressedAttack;
    private bool playerAttacking;
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
        attackRangeCheck.enabled = false;
	}
	
	void Update ()
    {
        GetMovementInput();
        GetJumpInput();
        GetAttackInput();
        UpdateIsOnGround();
        HandlePlayerJump();
        HandlePlayerAttack();
        HandlePlayerAnimation();
	}

    private void FixedUpdate()
    {
        HandlePlayerMovement();
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
        pressedAttack = Input.GetButtonDown("Fire1");
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

    private void HandlePlayerAttack()
    {
        if (pressedAttack && isOnGround && !playerAttacking)
        {
            Debug.Log("Attack");

            playerAttacking = true;

            attackTimer = attackCoolDown;

            attackRangeCheck.enabled = true;
        }

        if (playerAttacking)
        {
            if (attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;
                opponent.SetActive(false);
            }
            else
            {
                playerAttacking = false;
                attackRangeCheck.enabled = false;
            }
        }
    }

    private void UpdateIsOnGround()
    {
        Collider2D[] groundColliders = Physics2D.OverlapCircleAll(groundCheckPosition.position, groundCheckRadius, whatIsGround);

        isOnGround = groundColliders.Length > 0;
    }

    private void HandlePlayerAnimation()
    {
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));

        animator.SetFloat("vSpeed", playerRigidbody2D.velocity.y);

        animator.SetBool("Ground", isOnGround);

        animator.SetBool("Attacking", playerAttacking);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void DeactivatePlayer()
    {
        //unfreeze z rotation in player
        //diable rigigbody
    }
}
