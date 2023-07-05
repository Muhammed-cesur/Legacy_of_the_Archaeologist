// PlayerBlade.cs
using UnityEngine;

public class PlayerBlade : MonoBehaviour
{
    public float damageAmount = 10f; // Amount of damage to be dealt to the enemy

    private void OnTriggerEnter(Collider other)
    {
        SkeletonEnemyAI skeletonEnemy = other.GetComponent<SkeletonEnemyAI>();
        SpiderEnemyAI spiderEnemy = other.GetComponent<SpiderEnemyAI>();
        SpiderRangeEnemyAI spiderRangeEnemy = other.GetComponent<SpiderRangeEnemyAI>();
        PolygonalMetalonAI polygonalMetalonEnemy = other.GetComponent<PolygonalMetalonAI>();

        if (skeletonEnemy != null)
        {
            skeletonEnemy.TakeDamage(damageAmount);
        }
        else if (spiderEnemy != null)
        {
            spiderEnemy.TakeDamage(damageAmount);
        }
        else if (spiderRangeEnemy != null)
        {
            spiderRangeEnemy.TakeDamage(damageAmount);
        }
        else if (polygonalMetalonEnemy != null)
        {
            polygonalMetalonEnemy.TakeDamage(damageAmount);
        }
    }
}
