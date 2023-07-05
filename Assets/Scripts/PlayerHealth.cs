using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;  // Maximum health of the player
    private int currentHealth;  // Current health of the player

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        // Reduce health by the given damage amount
        currentHealth -= damage;

        // Check if the player is dead
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Perform death logic here, such as playing death animation, disabling controls, etc.
        // ...

        // Destroy the player object or trigger game over
        Destroy(gameObject);
    }
}
