using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private Transform attackRangePostion;
    [SerializeField]
    private float attackRadius = .5f;
    [SerializeField]
    private LayerMask whatCanBeAttacked;
    [SerializeField]
    private float attackCooldownInSeconds = .5f;

    private bool pressedAttack;
    private bool isOnCooldown = false;
    private bool playerAttacking;
    private float attackAnimationTimer = 0;
    private float attackAnimationCoolDown = 0.3f;
    private AudioSource attackAudioSource;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
        attackAudioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        GetAttackInput();

        if (playerController.IsAlive == true)
        {
            HandlePlayerAttack();
            HandlePlayerAnimation();
        }
    }

    private void GetAttackInput()
    {
        pressedAttack = Input.GetButtonDown("Fire" + playerController.PlayerNumber);
    }

    private void HandlePlayerAttack()
    {
        if (pressedAttack && !isOnCooldown)
        {
            isOnCooldown = true;

            playerAttacking = true;

            attackAnimationTimer = attackAnimationCoolDown;

            attackAudioSource.Play();

            StartCoroutine(WaitForAttackCooldown());

            Collider2D[] hitObjects = Physics2D.OverlapCircleAll(attackRangePostion.position, attackRadius, whatCanBeAttacked);

            bool didWeHitAnything = hitObjects.Length > 0;

            if (didWeHitAnything)
            {
                Debug.Log("Hit Player");

                PlayerController playerHit;

                for (int i = 0; i < hitObjects.Length; i++)
                {
                    playerHit = hitObjects[i].gameObject.GetComponent<PlayerController>();

                    if (playerHit != null)
                    {
                        playerHit.TakeDamage();
                    }
                }
            }

        }
    }

    private void HandlePlayerAnimation()
    {
        if (playerAttacking)
        {
            if (attackAnimationTimer > 0)
            {
                spriteRenderer.enabled = true;
                attackAnimationTimer -= Time.deltaTime;
            }
            else
            {
                playerAttacking = false;
                spriteRenderer.enabled = false;
            }
        }

        animator.SetBool("Attacking", playerAttacking);
    }

    private IEnumerator WaitForAttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldownInSeconds);
        isOnCooldown = false;
    }
}
