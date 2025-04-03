using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Enemy : MonoBehaviour
{
    public GameObject target;
    public GameObject levelUi;
    public GameObject xpUntilNextLevelUi;
    public float speed = 8.0f;
    public float FixedDamage = 1f;
    public float damage = 1.0f;
    public float FixedHealth = 3.0f;
    public float health = 3f;
    public float FixedXPAmount = 1f;
    public float xpAmount = 1f;
    
    [SerializeField] private Sprite[] enemyAnimations;
    private Spawner spawner; // Store the reference
    private Sprite enemyMovingAnimation;
    private bool _changeEnemyAnimation = false;

    private void Start()
    {
        // Find the Spawner inside CameraMovement
        GameObject cameraMovement = GameObject.Find("CameraMovement");

        if (cameraMovement != null)
        {
            spawner = cameraMovement.GetComponentInChildren<Spawner>(); // Look for Spawner inside
        }
        target = GameObject.FindGameObjectWithTag("Player");
        levelUi = GameObject.FindGameObjectWithTag("Level");
        xpUntilNextLevelUi = GameObject.FindGameObjectWithTag("Xp");
        enemyMovingAnimation = GetComponentInChildren<SpriteRenderer>().sprite;
    }

    private void Update()
    {
        if (target != null)
        {
            Vector2 dirToTarget = (Vector2)target.transform.position - (Vector2)transform.position;
            float angle = Mathf.Atan2(dirToTarget.y, dirToTarget.x) * Mathf.Rad2Deg - 90;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        
        if (GetComponent<SpriteRenderer>().sprite == enemyAnimations[0] && _changeEnemyAnimation)
        {
            enemyMovingAnimation = enemyAnimations[1];
            _changeEnemyAnimation = false;
        }
        else if (GetComponent<SpriteRenderer>().sprite == enemyAnimations[1] && _changeEnemyAnimation)
        {
            enemyMovingAnimation = enemyAnimations[2];
            _changeEnemyAnimation = false;
        }
        else if (GetComponent<SpriteRenderer>().sprite == enemyAnimations[2] && _changeEnemyAnimation)
        {
            enemyMovingAnimation = enemyAnimations[0];
            _changeEnemyAnimation = false;
        }

        if (enemyMovingAnimation != null)
        {
            GetComponent<SpriteRenderer>().sprite = enemyMovingAnimation;
            Invoke(nameof(ChangeAnimation), 0.5f);
        }
    }
    
    private void ChangeAnimation()
    {
        _changeEnemyAnimation = true;
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            Vector2 dirToTarget = (Vector2)target.transform.position - (Vector2)transform.position;
            dirToTarget.Normalize(); // Normalize to make it a unit vector
            GetComponent<Rigidbody2D>().velocity = dirToTarget * speed;
        }
    }

    public void Damage(float damage)
    {
        health -= damage;
        
        if (health <= 0)
        {
            spawner.spawnCount--;
            Debug.Log("Spawn count: " + spawner.spawnCount);
            levelUi.GetComponent<Level>().xp += xpAmount;
            Destroy(gameObject);
        }
    }
}
