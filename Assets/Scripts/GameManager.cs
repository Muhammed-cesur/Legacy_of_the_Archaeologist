using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int playerScore;
    private int playerLevel;
    private float playerHealth;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int GetPlayerScore()
    {
        return playerScore;
    }

    public void SetPlayerScore(int score)
    {
        playerScore = score;
    }

    public int GetPlayerLevel()
    {
        return playerLevel;
    }

    public void SetPlayerLevel(int level)
    {
        playerLevel = level;
    }

    public float GetPlayerHealth()
    {
        return playerHealth;
    }

    public void SetPlayerHealth(float health)
    {
        playerHealth = health;
    }

    // Other GameManager functionality...

    private void Start()
    {
        // Example: Initialize player data
        playerScore = 0;
        playerLevel = 1;
        playerHealth = 100f;
    }

    // Other GameManager methods and game logic...
}
