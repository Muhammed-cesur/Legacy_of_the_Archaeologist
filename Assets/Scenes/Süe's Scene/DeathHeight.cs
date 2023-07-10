using UnityEngine;
using System.Collections;

public class CharacterDeath : MonoBehaviour
{
    public float deathHeight = -10f; // Karakterin ölümü için belirlenen yükseklik deðeri
    public Transform respawnPoint; // Doðma noktasý
    private PlayerHealth playerHealth; // Saðlýk kontrolcüsü referansý

    private void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        float characterHeight = transform.position.y;
        if (characterHeight < deathHeight)
        {
            KillCharacter();
        }
    }

    private void KillCharacter()
    {
        if (playerHealth != null)
        {
            playerHealth.currentHealth = 0;
            RespawnCharacter();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void RespawnCharacter()
    {
        transform.position = respawnPoint.position;
        if (playerHealth != null)
        {
            playerHealth.ResetHealth();
        }
    }
}