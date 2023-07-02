using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public float horizontalRadius;
    public float verticalRadius;
    public float zRadius;
    public int maxMonsters;
    public float spawnInterval;
    public int monstersPerSpawn;
    public float attackRange = 1.5f;
    public float enemySpeed = 3f;
    public Transform character;

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
                SkeletonEnemyAI enemyAI = enemy.GetComponent<SkeletonEnemyAI>();
                enemyAI.attackRange = attackRange;
                enemyAI.movementSpeed = enemySpeed;
                enemyAI.player = character;

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
