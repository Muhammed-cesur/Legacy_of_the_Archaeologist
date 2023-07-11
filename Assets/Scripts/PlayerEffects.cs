using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public PlayerController playerController;

    public KeyCode healKey = KeyCode.Alpha1;
    public KeyCode powerUpKey = KeyCode.Alpha2;
    public KeyCode speedUpKey = KeyCode.Alpha3;

    public float powerUpDuration = 30f;
    public float speedUpDuration = 30f;

    private bool isPoweredUp = false;
    private bool isSpeedUp = false;

    private void Update()
    {
        if (Input.GetKeyDown(healKey))
        {
            // Heal the character
            playerHealth.TakeDamage(-50);
            Debug.Log("Player healed by 50 points.");
        }
        else if (Input.GetKeyDown(powerUpKey))
        {
            // Power up the character
            PowerUpCharacter();
        }
        else if (Input.GetKeyDown(speedUpKey))
        {
            // Speed up the character
            SpeedUpCharacter();
        }

        // Check if power-up effect has expired
        if (isPoweredUp)
        {
            powerUpDuration -= Time.deltaTime;
            if (powerUpDuration <= 0f)
            {
                // Remove power-up effect
                RemovePowerUpEffect();
            }
        }

        // Check if speed-up effect has expired
        if (isSpeedUp)
        {
            speedUpDuration -= Time.deltaTime;
            if (speedUpDuration <= 0f)
            {
                // Remove speed-up effect
                RemoveSpeedUpEffect();
            }
        }
    }

    private void PowerUpCharacter()
    {
        // Apply power-up logic here
        // For example, increase the damage amount of the player's sword
        PlayerBlade playerBlade = GetComponent<PlayerBlade>();
        playerBlade.damageAmount = 20f;

        // Set the flag for power-up effect
        isPoweredUp = true;
        powerUpDuration = 30f;
        Debug.Log("Power-up activated for 30 seconds.");
    }

    private void RemovePowerUpEffect()
    {
        // Remove power-up effect logic here
        // For example, revert the damage amount of the player's sword back to the default value
        PlayerBlade playerBlade = GetComponent<PlayerBlade>();
        playerBlade.damageAmount = 10f;

        // Reset the flag for power-up effect
        isPoweredUp = false;
        Debug.Log("Power-up effect deactivated.");
    }

    private void SpeedUpCharacter()
    {
        // Apply speed-up logic here
        // For example, increase the movement speed of the player
        // playerController.maxSpeed = 4f;

        // Set the flag for speed-up effect
        isSpeedUp = true;
        speedUpDuration = 30f;
        Debug.Log("Speed-up activated for 30 seconds.");
    }

    private void RemoveSpeedUpEffect()
    {
        // Remove speed-up effect logic here
        // For example, revert the movement speed of the player back to the default value
        // playerController.maxSpeed = 2f;

        // Reset the flag for speed-up effect
        isSpeedUp = false;
        Debug.Log("Speed-up effect deactivated.");
    }
}
