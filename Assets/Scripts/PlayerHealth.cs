using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;  // Maximum health of the player
    public int currentHealth;  // Current health of the player
    public Slider healthBar;

    private UIManager _uıManager;


    private void Start()
    {
        currentHealth = maxHealth; // Başlangıçta canı maksimum değere ayarla
        UpdateHealthSlider();
        

    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount; // Hasarı mevcut cana uygula
        Debug.Log(currentHealth);
        UpdateHealthSlider();
        if (currentHealth <= 0f)
        {
            Die();
        }
    }
    private void UpdateHealthSlider()
    {
        if (healthBar != null)
        {
            float normalizedHealth = (float)currentHealth / maxHealth;
            healthBar.value = normalizedHealth;
            Debug.Log(healthBar.value);
        }
    }
    private void Die()
    {
        _uıManager.GameOver();
        Debug.Log("Player has died.");

        // Örneğin, oyunu yeniden başlat veya ölüm ekranını göster gibi işlemler yapılabilir.
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }
}

