// PlayerBlade.cs
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerBlade : MonoBehaviour
{
    public float damageAmount = 10f; // Amount of damage to be dealt to the enemy
    private bool canAttack = true; // Flag to check if the player can attack

    private void OnTriggerEnter(Collider other)
    {
        if (canAttack)
        {
            SkeletonEnemyAI skeletonEnemy = other.GetComponent<SkeletonEnemyAI>();
            SpiderEnemyAI spiderEnemy = other.GetComponent<SpiderEnemyAI>();
            SpiderRangeEnemyAI spiderRangeEnemy = other.GetComponent<SpiderRangeEnemyAI>();
            PolygonalMetalonAI polygonalMetalonEnemy = other.GetComponent<PolygonalMetalonAI>();
            Minotour minotour = other.GetComponent<Minotour>();

            if (skeletonEnemy != null)
            {
                skeletonEnemy.TakeDamage(damageAmount);
            }
            else if (spiderEnemy != null)
            {
                spiderEnemy.TakeDamage(damageAmount);
            }
            else if (spiderRangeEnemy != null)
            {
                spiderRangeEnemy.TakeDamage(damageAmount);
            }
            else if (polygonalMetalonEnemy != null)
            {
                polygonalMetalonEnemy.TakeDamage(damageAmount);
            }
            else if (minotour != null)
            {
                minotour.TakeDamage(damageAmount);
            }

            // Start the attack cooldown coroutine
            StartCoroutine(AttackCooldown());
        }
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false; // Disable attacking
        yield return new WaitForSeconds(1f); // Wait for 0.5 seconds
        canAttack = true; // Enable attacking
    }
}
