using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Projectile : MonoBehaviour
{
    public GameObject Player;
    public PlayerStats pStatsS;
    public float speed = 10f;

    public void Init()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * speed;
        //GetComponent<Rigidbody2D>().AddForce(transform.right * speed);
    }
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        pStatsS = Player.GetComponent<PlayerStats>();
        GetComponent<Rigidbody2D>().velocity = transform.up * speed;
        //GetComponent<Rigidbody2D>().AddForce(transform.right * speed);
        
        Destroy(gameObject, pStatsS.bulletLifeTime);
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

        if (other.gameObject.GetComponent<Enemy>())
        {
            Debug.Log("Damaging enemy: " + other.gameObject.name);
            other.gameObject.GetComponent<Enemy>().Damage(pStatsS.damage);
            Destroy(gameObject);  // Destroy the projectile on trigger hit
        }
    }
}