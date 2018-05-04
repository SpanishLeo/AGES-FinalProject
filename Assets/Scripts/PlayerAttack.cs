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
    private float attackCooldownInSeconds = 1f;

    private bool isOnCooldown = false;
    private bool playerAttacking;
    private float attackTimer = 0;
    private float attackCoolDown = 0.3f;
    private Animator animator;


    private string AttackInputName
    {
        get
        {
            return "Fire" + playerController.PlayerNumber;
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (playerController.IsAlive == true)
        {
            HandlePlayerAttack();
        }
    }

    private void HandlePlayerAttack()
    {
        bool pressedAttack = Input.GetButtonDown(AttackInputName);

        if (pressedAttack && !isOnCooldown)
        {
            isOnCooldown = true;

            playerAttacking = true;

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
        animator.SetBool("Attacking", playerAttacking);
    }

    private IEnumerator WaitForAttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldownInSeconds);
        isOnCooldown = false;
    }
}
