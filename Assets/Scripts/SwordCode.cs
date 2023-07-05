// PlayerBlade.cs
using UnityEngine;
using System.Collections;

public class PlayerBlade : MonoBehaviour
{
    public float damageAmount = 10f; // Amount of damage to be dealt to the enemy

    private void OnTriggerEnter(Collider other)
    {
        SkeletonEnemyAI enemy = other.GetComponent<SkeletonEnemyAI>(); // Assuming you have a SkeletonEnemyAI script attached to the enemy object

        if (enemy != null)
        {
            Debug.Log(enemy.currentHealth);
            enemy.TakeDamage(damageAmount);
        }
    }
}
