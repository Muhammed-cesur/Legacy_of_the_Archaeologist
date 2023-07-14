using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;  // Maximum health of the player
    public int currentHealth;  // Current health of the player
    public Slider healthBar;

    private UIManager uıManager;
    private Vector3 savedPosition;  // Saved position of the player
    private Animator _Anim;

    private void Start()
    {
        uıManager = FindObjectOfType<UIManager>();
        currentHealth = maxHealth; // Set current health to maximum at the start
        UpdateHealthSlider();
        _Anim=GetComponent<Animator>();

    }


    private void Update()
    {
        // Check for the player's death
        if (currentHealth <= 0)
        {
            Die();
            _Anim.SetTrigger("Die"); 
        }

        // Handle saving and loading inputs
        if (Input.GetKeyDown(KeyCode.F5))
        {
            SavePlayerData();
        }
        else if (Input.GetKeyDown(KeyCode.F9))
        {
            LoadPlayerData();
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount; // Apply damage to the current health
        Debug.Log(currentHealth);
        UpdateHealthSlider();
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

    public void Die()
    {
        // Actions to perform when the player dies
        Debug.Log("Player has died.");
        
        
        //StartCoroutine(uıManager.GameOverCoroutine(2f));

        
        // Respawn the player with full health
        Respawn();
    }
    
    

    private void Respawn()
    {
        // Reset the player's position to the saved position
        transform.position = savedPosition;

        // Reset the player's health to full
        ResetHealth();
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        UpdateHealthSlider();
    }

    public void SavePlayerData()
    {
        // Save the player's position and health
        savedPosition = transform.position;
        PlayerPrefs.SetFloat("SavedPositionX", savedPosition.x);
        PlayerPrefs.SetFloat("SavedPositionY", savedPosition.y);
        PlayerPrefs.SetFloat("SavedPositionZ", savedPosition.z);
        PlayerPrefs.SetInt("SavedHealth", currentHealth);

        Debug.Log("Player data saved.");
    }

    public void LoadPlayerData()
    {
        // Load the saved player's position and health
        float savedPositionX = PlayerPrefs.GetFloat("SavedPositionX");
        float savedPositionY = PlayerPrefs.GetFloat("SavedPositionY");
        float savedPositionZ = PlayerPrefs.GetFloat("SavedPositionZ");
        savedPosition = new Vector3(savedPositionX, savedPositionY, savedPositionZ);

        // Set the player's position
        transform.position = savedPosition;

        currentHealth = PlayerPrefs.GetInt("SavedHealth");
        UpdateHealthSlider();

        Debug.Log("Player data loaded.");

        // Restart the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }
}