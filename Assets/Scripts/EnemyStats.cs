using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyStats : MonoBehaviour
{
    public GameObject target;
    public GameObject levelUi;
    public GameObject xpUntilNextLevelUi;
    
    public float healthMaxDefault = 3f;
    public float healthRegenDefault = 1;
    public float damageDefault = 1f;
    public float bodyDamageDefault = 1f;
    public float speedDefault = 2f;
    public float rangeDefault = 6f;
    public float fireRateDefault = 1f;
    public float bulletLifeTimeDefault = 0.6f;
    public float xpAmountDefault = 1f;
    
    [HideInInspector]public float healthMax = 1;
    [HideInInspector]public float health = 1;
    [HideInInspector]public float healthRegen = 1;
    [HideInInspector]public float damage = 1f;
    [HideInInspector]public float bodyDamage = 1f;
    [HideInInspector]public float speed = 1;
    [HideInInspector]public float range = 1f;
    [HideInInspector]public float firerate = 1f;
    [HideInInspector]public float bulletLifeTime;
    [HideInInspector]public float xpAmount = 1f;

    public bool _isEnemyShooting = false;

    private Spawner spawnerS;
    private void Start()
    {
        if (!_isEnemyShooting)
        {
            spawnerS = GameObject.FindGameObjectWithTag("SpawnerEnemy").GetComponent<Spawner>();
        }
        else
        {
            spawnerS = GameObject.FindGameObjectWithTag("SpawnerEnemyShooting").GetComponent<Spawner>();
        }
        levelUi = GameObject.FindGameObjectWithTag("Level");
        xpUntilNextLevelUi = GameObject.FindGameObjectWithTag("Xp");

        health = healthMaxDefault;
        healthRegen = healthRegenDefault;
        damage = damageDefault;
        bodyDamage = bodyDamageDefault;
        speed = speedDefault;
        range = rangeDefault;
        bulletLifeTime = bulletLifeTimeDefault;
        xpAmount = xpAmountDefault;
    }

    private void Update()
    {
        bool _canUpgrade = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Timer>()._canUpgrade;
        float multiplier = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Timer>().multiplier;
        if (_canUpgrade)
        {
            xpAmount = xpAmountDefault * multiplier;
            health = healthMaxDefault * multiplier;
            damage = damageDefault * multiplier;
            Debug.Log("Enemy HP: " + health + ", Damage: " + damage + ", xpAmount: "+ xpAmount);
        }
    }
    public void Damage(float damage)
    {
        health -= damage;
        
        if (health <= 0)
        {
            spawnerS.spawnCount--;
            Debug.Log("Spawn count: " + spawnerS.spawnCount);
            levelUi.GetComponent<Level>().xp += xpAmount;
            Destroy(gameObject);
        }
    }
}
