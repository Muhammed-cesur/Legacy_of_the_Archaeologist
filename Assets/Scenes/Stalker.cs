using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform character;
    public float speed = 3f;
    public float attackRange = 1.5f;

    void Update()
    {
        if (character != null)
        {
            Vector3 direction = character.position - transform.position;
            direction.y = 0f;
            direction.Normalize();

            transform.rotation = Quaternion.LookRotation(direction);

            float distance = Vector3.Distance(transform.position, character.position);

            if (distance > attackRange)
            {
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
            else
            {
                // Attack the character
                Attack();
            }
        }
    }

    void Attack()
    {
        // Perform attack logic here
        Debug.Log("Enemy is attacking!");
    }
}
