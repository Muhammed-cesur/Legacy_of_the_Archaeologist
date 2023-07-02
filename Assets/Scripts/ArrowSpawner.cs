using System.Threading;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform[] spawnPoints;
    public float ArrowFrequency = 3f;

    private float nextArrowTimer;

    private void Start()
    {
        nextArrowTimer = Time.time;
    }

    private void Update()
    {
        if (Time.time >= nextArrowTimer)
        {
            SpawnArrow();
            nextArrowTimer = Time.time + ArrowFrequency;
        }
    }
    private void SpawnArrow()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Instantiate(arrowPrefab, spawnPoints[i].position, spawnPoints[i].rotation);
        }
    }
    
}