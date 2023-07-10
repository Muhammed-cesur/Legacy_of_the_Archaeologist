using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuvarOkTrap : MonoBehaviour
{
    [SerializeField]private int arrowDamage = 10;
    [SerializeField]private float arrowSpeed = 5f;

    private Rigidbody arrowRb;
    void Start()
    {
        arrowRb = GetComponent<Rigidbody>();
    }
    
 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            // Player'daki can Scripti'ne ulaş ve collision'dan ulaşabilirsin
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(arrowDamage);
                Destroy(gameObject, 0f);

            }
        }
        
    }
    // Update is called once per frame
    void Update()
    { 
        ApplyForwardForce();
        Destroy(gameObject, 2f);
    }

    void ApplyForwardForce()
    {
        // Rigidbody'nin gücünü kullanarak objeyi ileri doğru hareket ettir
        arrowRb.AddForce(transform.position * arrowSpeed*Time.deltaTime );
    }

}
