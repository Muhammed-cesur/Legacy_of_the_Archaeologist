using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;          // Prefab of the enemy to be spawned
    public Transform spawnPoint;            // Point where the enemies will be spawned
    public float horizontalRadius;          // Maximum horizontal radius for enemy spawning
    public float verticalRadius;            // Maximum vertical radius for enemy spawning
    public float zRadius;                   // Maximum z-axis radius for enemy spawning
    public int maxMonsters;                 // Maximum number of monsters to spawn
    public float spawnInterval;             // Time interval between spawns in seconds
    public int monstersPerSpawn;            // Number of monsters to spawn in each spawn

    public float attackRange = 1.5f;
    public float enemySpeed = 3f;
    public Transform character;

    private int currentMonsters;            // Current number of spawned monsters
    private Transform enemyParent;          // Parent object to hold the spawned enemies

    private void Start()
    {
        currentMonsters = 0;
        enemyParent = new GameObject("SpawnedEnemies").transform;
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (currentMonsters < maxMonsters)
        {
            SpawnMonsterGroup(monstersPerSpawn);
            currentMonsters += monstersPerSpawn;

            // Wait for the next spawn interval
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnMonsterGroup(int groupSize)
    {
        for (int i = 0; i < groupSize; i++)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        // Calculate random positions within the specified radii
        float randomX = Random.Range(-horizontalRadius, horizontalRadius);
        float randomY = Random.Range(-verticalRadius, verticalRadius);
        float randomZ = Random.Range(-zRadius, zRadius);
        Vector3 spawnPosition = spawnPoint.position + new Vector3(randomX, randomY, randomZ);

        // Instantiate the enemy prefab at the spawn position
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        EnemyController enemyController = enemy.GetComponent<EnemyController>();
        enemyController.character = character;
        enemyController.speed = enemySpeed;
        enemyController.attackRange = attackRange;

        // Set the spawned enemy as a child of the parent object
        enemy.transform.parent = enemyParent;
    }
}

