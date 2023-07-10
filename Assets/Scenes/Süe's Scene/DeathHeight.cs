using UnityEngine;
using System.Collections;

public class CharacterDeath : MonoBehaviour
{
    public float deathHeight = -10f; // Karakterin �l�m� i�in belirlenen y�kseklik de�eri
    public Transform respawnPoint; // Do�ma noktas�
    private PlayerHealth playerHealth; // Sa�l�k kontrolc�s� referans�

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