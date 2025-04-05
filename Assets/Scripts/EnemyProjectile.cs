using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public GameObject Enemy;
    public EnemyStats eStatsS;
    public float speed = 8f;

    public void Init()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * speed;
        //GetComponent<Rigidbody2D>().AddForce(transform.right * speed);
    }
    private void Start()
    {
        eStatsS = Enemy.GetComponentInParent<EnemyStats>();

        GetComponent<Rigidbody2D>().velocity = transform.up * speed;
        //GetComponent<Rigidbody2D>().AddForce(transform.right * speed);
        Destroy(gameObject, eStatsS.bulletLifeTime);
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

        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Damaging enemy: " + other.gameObject.name);
            other.gameObject.GetComponent<PlayerMovement>().Damage(eStatsS.damage);

            Destroy(gameObject);  // Destroy the projectile on trigger hit
        }
    }
}
