using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public List<GameObject> itemDropPrefabs;
    public float horizontalRadius;
    public float verticalRadius;
    public float zRadius;
    public int maxMonsters;
    public float spawnInterval;
    public int monstersPerSpawn;
    public float attackRange = 1.5f;
    public float enemySpeed = 3f;
    public Transform character;
    public int maxHealth = 20;
    private int currentMonsters;
    private Transform enemyParent;

    private void Start()
    {
        enemyParent = new GameObject("Enemies").transform;
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (currentMonsters < maxMonsters)
        {
            for (int i = 0; i < monstersPerSpawn; i++)
            {
                if (currentMonsters >= maxMonsters)
                    break;

                Vector3 spawnPosition = GetRandomSpawnPosition();
                GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, enemyParent);

                SkeletonEnemyAI skeletonEnemy = enemy.GetComponent<SkeletonEnemyAI>();
                SpiderEnemyAI spiderEnemy = enemy.GetComponent<SpiderEnemyAI>();
                SpiderRangeEnemyAI spiderRangeEnemy = enemy.GetComponent<SpiderRangeEnemyAI>();
                PolygonalMetalonAI polygonalMetalonEnemy = enemy.GetComponent<PolygonalMetalonAI>();
                Minotour minotour = enemy.GetComponent<Minotour>();
                if (skeletonEnemy != null)
                {
                    skeletonEnemy.attackRange = attackRange;
                    skeletonEnemy.movementSpeed = enemySpeed;
                    skeletonEnemy.player = character;
                    skeletonEnemy.itemDropPrefabs = itemDropPrefabs;
                    skeletonEnemy.maxHealth = maxHealth;
                }
                else if (spiderEnemy != null)
                {
                    spiderEnemy.attackRange = attackRange;
                    spiderEnemy.movementSpeed = enemySpeed;
                    spiderEnemy.player = character;
                    spiderEnemy.itemDropPrefabs = itemDropPrefabs;
                    spiderEnemy.maxHealth = maxHealth;
                }
                else if (spiderRangeEnemy != null)
                {
                    spiderRangeEnemy.attackRange = attackRange;
                    spiderRangeEnemy.movementSpeed = enemySpeed;
                    spiderRangeEnemy.player = character;
                    spiderRangeEnemy.itemDropPrefabs = itemDropPrefabs;
                    spiderRangeEnemy.maxHealth = maxHealth;
                }
                else if (polygonalMetalonEnemy != null)
                {
                    polygonalMetalonEnemy.attackRange = attackRange;
                    polygonalMetalonEnemy.movementSpeed = enemySpeed;
                    polygonalMetalonEnemy.player = character;
                    polygonalMetalonEnemy.itemDropPrefabs = itemDropPrefabs;
                    polygonalMetalonEnemy.maxHealth = maxHealth;
                }
                else if (minotour != null)
                {
                    minotour.attackRange = attackRange;
                    minotour.movementSpeed = enemySpeed;
                    minotour.player = character;
                    minotour.itemDropPrefabs = itemDropPrefabs;
                    minotour.maxHealth = maxHealth;
                }
                currentMonsters++;
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float randomX = Random.Range(-horizontalRadius, horizontalRadius);
        float randomY = Random.Range(-verticalRadius, verticalRadius);
        float randomZ = Random.Range(-zRadius, zRadius);
        Vector3 randomPosition = new Vector3(spawnPoint.position.x + randomX, spawnPoint.position.y + randomY, spawnPoint.position.z + randomZ);
        return randomPosition;
    }
}
