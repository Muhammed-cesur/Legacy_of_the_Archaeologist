using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;  // Maximum health of the player
    private int currentHealth;  // Current health of the player

    private void Start()
    {
        currentHealth = maxHealth; // Başlangıçta canı maksimum değere ayarla
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount; // Hasarı mevcut cana uygula
        Debug.Log(currentHealth);

        if (currentHealth <= 0f)
        {
            Die(); // Can 0 veya daha az ise öl
        }
    }

    private void Die()
    {
        // Oyuncu ölme durumunda yapılması gereken işlemler
        Debug.Log("Player has died.");
        // Örneğin, oyunu yeniden başlat veya ölüm ekranını göster gibi işlemler yapılabilir.
    }
}

