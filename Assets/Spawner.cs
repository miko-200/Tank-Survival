using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;
    public GameObject CenterObject;
    public float spawnInterval = 5f;
    public float minRadius;
    public float maxRadius;
    public int maxSpawnCount = 45;
    public bool bulkSpawn = false;
    public int bulkSpawnCount = 5;
    public bool randomBulkSpawn = false;
    [HideInInspector]public int spawnCount = 0;
  
    private Vector2 center;
    private float angle1 = 0f;
    private float angle2 = 360f;
    private Coroutine spawnRoutine;
    
    private void BulkSpawn()
    {
        center = CenterObject.transform.position;
        angle1 = Random.Range(0f, 330f); 
        angle2 = angle1 + 30f;
        for (int i = 0; i < bulkSpawnCount; i++) 
        {
            float angle = Random.Range(angle1, angle2); // Random angle in degrees
            float radius = Random.Range(minRadius, maxRadius); // Random distance within range

            Vector2 spawnPosition = center + new Vector2(
                Mathf.Cos(angle * Mathf.Deg2Rad) * radius,
                Mathf.Sin(angle * Mathf.Deg2Rad) * radius
            );
            Instantiate(enemy, spawnPosition, Quaternion.identity);
            spawnCount++;
        }
    }
    private void RandomBulkSpawn()
    {
        center = CenterObject.transform.position;
        for (int i = 0; i < bulkSpawnCount; i++)
        {
            angle1 = Random.Range(0f, 330f);
            angle2 = angle1 + 30f;
        
            float angle = Random.Range(angle1, angle2); // Random angle in degrees
            float radius = Random.Range(minRadius, maxRadius); // Random distance within range

            Vector2 spawnPosition = center + new Vector2(
                Mathf.Cos(angle * Mathf.Deg2Rad) * radius,
                Mathf.Sin(angle * Mathf.Deg2Rad) * radius
            );
            Instantiate(enemy, spawnPosition, Quaternion.identity);
            spawnCount++;
        }
    }
    private void Spawn()
    {
        center = CenterObject.transform.position;
        float angle = Random.Range(0f, 360f); // Random angle in degrees
        float radius = Random.Range(minRadius, maxRadius); // Random distance within range

        Vector2 spawnPosition = center + new Vector2(
            Mathf.Cos(angle * Mathf.Deg2Rad) * radius,
            Mathf.Sin(angle * Mathf.Deg2Rad) * radius
        );
        Instantiate(enemy, spawnPosition, Quaternion.identity);
        spawnCount++;
    }

    private void Start()
    {
        StartSpawning();
    }

    public void StartSpawning()
    {
        if (spawnRoutine != null)
        {
            StopCoroutine(spawnRoutine); // Stop old coroutine if running
        }
        spawnRoutine = StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (spawnCount < maxSpawnCount)
            {
                if (!bulkSpawn)
                {
                    Spawn();
                }
                else
                {
                    if (!randomBulkSpawn)
                    {
                        BulkSpawn();
                    }
                    else
                    {
                        RandomBulkSpawn();
                    }
                }
                Debug.Log("Spawn count: " + spawnCount);
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void SetSpawnInterval(float newInterval)
    {
        spawnInterval = newInterval;
        StartSpawning(); // Restart spawning with the new interval
        Debug.Log("Spawn interval changed to: " + newInterval);
    }
} 