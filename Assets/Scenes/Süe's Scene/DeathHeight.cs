using UnityEngine;
using System.Collections;

public class CharacterDeath : MonoBehaviour
{
    public float deathHeight = -10f; // Karakterin ölümü için belirlenen yükseklik deðeri
    public Transform respawnPoint; // Doðma noktasý
    private HealthController healthController; // Saðlýk kontrolcüsü referansý

    private void Start()
    {
        healthController = GetComponent<HealthController>();
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
        if (healthController != null)
        {
            healthController.CurrentHealth = 0;
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
        if (healthController != null)
        {
            healthController.ResetHealth();
        }
    }
}