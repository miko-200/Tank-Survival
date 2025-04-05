using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;
    public GameObject CenterObject;
    public Timer timer;
    public float spawnInterval = 5f;
    public float minRadius;
    public float maxRadius;
    public int maxSpawnCount = 45;
    public bool bulkSpawn = false;
    public int bulkSpawnCount = 5;
    public bool randomBulkSpawn = false;
    [HideInInspector]public int spawnCount = 0;

    private GameObject EnemyUpgrades;
    private Vector2 center;
    private float angle1 = 0f;
    private float angle2 = 360f;
    private Coroutine spawnRoutine;
    
    public float mapMinX = -225f;
    public float mapMaxX = 225f;
    public float mapMinY = -225f;
    public float mapMaxY = 225f;
    public float spawnMargin = 1f;

    private void Start()
    {
        EnemyUpgrades = GameObject.FindGameObjectWithTag("Timer");
        StartSpawning();
    }
    private void BulkSpawn()
    {
        center = CenterObject.transform.position;
        angle1 = Random.Range(0f, 330f);
        angle2 = angle1 + 30f;

        for (int i = 0; i < bulkSpawnCount; i++)
        {
            Vector2 spawnPosition = GetValidSpawnPosition(angle1, angle2);
            SpawnEnemyAt(spawnPosition);
        }
    }

    private void RandomBulkSpawn()
    {
        center = CenterObject.transform.position;
        for (int i = 0; i < bulkSpawnCount; i++)
        {
            angle1 = Random.Range(0f, 330f);
            angle2 = angle1 + 30f;
            
            Vector2 spawnPosition = GetValidSpawnPosition(angle1, angle2);
            SpawnEnemyAt(spawnPosition);
        }
    }

    private void Spawn()
    {
        center = CenterObject.transform.position;
        float angle = Random.Range(0f, 360f);
        Vector2 spawnPosition = GetValidSpawnPosition(angle, angle);
        SpawnEnemyAt(spawnPosition);
    }

    private Vector2 GetValidSpawnPosition(float angleStart, float angleEnd)
    {
        int maxTries = 10;
        int tries = 0;
        Vector2 spawnPosition = Vector2.zero;

        do
        {
            float angle = Random.Range(angleStart, angleEnd);
            float radius = Random.Range(minRadius, maxRadius);

            spawnPosition = center + new Vector2(
                Mathf.Cos(angle * Mathf.Deg2Rad) * radius,
                Mathf.Sin(angle * Mathf.Deg2Rad) * radius
            );

            // Clamp to keep inside bounds
            spawnPosition.x = Mathf.Clamp(spawnPosition.x, mapMinX + spawnMargin, mapMaxX - spawnMargin);
            spawnPosition.y = Mathf.Clamp(spawnPosition.y, mapMinY + spawnMargin, mapMaxY - spawnMargin);

            tries++;

        } while (tries < maxTries && (spawnPosition.x < mapMinX || spawnPosition.x > mapMaxX || 
                                      spawnPosition.y < mapMinY || spawnPosition.y > mapMaxY));

        return spawnPosition;
    }

    private void SpawnEnemyAt(Vector2 spawnPosition)
    {
        GameObject newEnemy = Instantiate(enemy, spawnPosition, Quaternion.identity);
        EnemyStats newEnemyStatsScript = newEnemy.GetComponent<EnemyStats>();
        newEnemyStatsScript.xpAmount = timer.xpAmount;
        newEnemyStatsScript.health = timer.health;
        newEnemyStatsScript.damage = timer.damage;
        spawnCount++;
        
        newEnemy.GetComponent<EnemyMovement>().spawner = this.gameObject;
    }

    public void StartSpawning()
    {
        if (spawnRoutine != null)
        {
            StopCoroutine(spawnRoutine);
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