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
    public Multipliers MultiplierScript;
    public TextMeshProUGUI BulDurText;
    public float speed = 10f;
    public float lifeTime = 1f;

    public void Init()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * speed;
        //GetComponent<Rigidbody2D>().AddForce(transform.right * speed);
    }
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        MultiplierScript = Player.GetComponent<Multipliers>();
        BulDurText = GameObject.Find("BulletLifeTime").GetComponent<TextMeshProUGUI>();
        lifeTime = MultiplierScript.bulletLifeTime;
        BulDurText.text = lifeTime.ToString();
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

    public void OnLevelUp()
    {
        lifeTime = MultiplierScript.bulletLifeTime;
        BulDurText.text = lifeTime.ToString();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger entered with: " + other.gameObject.name);

        if (other.gameObject.GetComponent<Enemy>())
        {
            Debug.Log("Damaging enemy: " + other.gameObject.name);
            other.gameObject.GetComponent<Enemy>().Damage(Player.GetComponent<Tower>().damageUsing);
            Destroy(gameObject);  // Destroy the projectile on trigger hit
        }
    }
}