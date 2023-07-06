using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YerTrap : MonoBehaviour
{
    [SerializeField] private int canAzalmaMiktari = 10;
    private bool temasEdildi = false;



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !temasEdildi)
        {
            temasEdildi = true;
            
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            // Player'daki can Scripti'ne ulaş ve collision'dan ulaşabilirsin
            if (playerHealth != null)
            {
              playerHealth.TakeDamage(canAzalmaMiktari);
              
            }
        }
    }
    
}
