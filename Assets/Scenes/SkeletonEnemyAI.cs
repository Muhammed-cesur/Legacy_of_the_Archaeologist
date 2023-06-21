using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonEnemyAI : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float attackRange = 1.5f;
    public float attackCooldown = 2f;

    private Transform player;
    private Animator animator;
    private bool isAttacking = false;
    private bool canAttack = true;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isAttacking)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer > attackRange)
            {
                // Move towards the player
                Vector3 direction = player.position - transform.position;
                transform.Translate(direction.normalized * moveSpeed * Time.deltaTime, Space.World);

                // Rotate towards the player
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.1f);
            }
            else
            {
                // Attack the player
                if (canAttack)
                {
                    StartCoroutine(Attack());
                }
            }
        }
    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        canAttack = false;

        Debug.Log("Enemy attacking = true");

        animator.SetTrigger("Attack");

        // Perform attack animation
        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
        isAttacking = false;
    }
}

