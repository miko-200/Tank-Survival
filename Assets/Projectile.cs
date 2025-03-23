using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 2f;
    public float damage = 10f;

    public void Init()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * speed;
        //GetComponent<Rigidbody2D>().AddForce(transform.right * speed);
    }
    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * speed;
        //GetComponent<Rigidbody2D>().AddForce(transform.right * speed);
        
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        if (transform.childCount > 0)
        {
            transform.GetChild(0).up = GetComponent<Rigidbody2D>().velocity.normalized;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger entered with: " + other.gameObject.name);

        if (other.gameObject.GetComponent<Damagable>())
        {
            Debug.Log("Damaging enemy: " + other.gameObject.name);
            other.gameObject.GetComponent<Damagable>().Damage(damage);
            Destroy(gameObject);  // Destroy the projectile on trigger hit
        }
    }
}