using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject target;
    public float speed = 8.0f;
    public float damage = 5.0f;
    public float health = 30f;
    
    private Spawner spawner; // Store the reference

    private void Start()
    {
        // Find the Spawner inside CameraMovement
        GameObject cameraMovement = GameObject.Find("CameraMovement");

        if (cameraMovement != null)
        {
            spawner = cameraMovement.GetComponentInChildren<Spawner>(); // Look for Spawner inside
        }
        target = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (target != null)
        {
            Vector2 dirToTarget = (Vector2)target.transform.position - (Vector2)transform.position;
            float angle = Mathf.Atan2(dirToTarget.y, dirToTarget.x) * Mathf.Rad2Deg - 90;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
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
            Destroy(gameObject);
        }
    }
}
