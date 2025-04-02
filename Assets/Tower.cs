using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public GameObject rotator;
    public GameObject shooter;
    public float speed = 15f;
    public float range = 6f;
    public float health = 10f;
    private float healthRegen;
    private bool _isRegenerating = false;
    public float damage = 1f;
    public float bodyDamage = 2f;
    [HideInInspector]public List<GameObject> _enemies = new();

    //private GameObject enemy;
    private Rigidbody2D _rb;
    private CircleCollider2D _col;
    private Vector2 _input;
    private List<GameObject> _projectiles = new();
    [SerializeField] private Sprite[] towerAnimations;
    private Sprite towerMovingAnimation;
    private bool _changeTowerAnimation = true;


    private GameObject _pickedUpWeapon;

    private void Start()
    {
        Multipliers multipliersScript = this.GetComponent<Multipliers>();
        bodyDamage *= multipliersScript.damageMultiplier;
        health *= multipliersScript.healthMultiplier;
        healthRegen = multipliersScript.healthMultiplier;
        damage *= multipliersScript.damageMultiplier;
        speed *= multipliersScript.moveSpeedMultiplier;
        if (gameObject != null)
        {
            _rb = this.gameObject.GetComponent<Rigidbody2D>();
            _col = this.gameObject.GetComponent<CircleCollider2D>();
            _col.radius = range;
            towerMovingAnimation = this.gameObject.GetComponent<SpriteRenderer>().sprite;
        }
    }

    private void Update()
    {
        if (gameObject != null)
        {
            if (!_isRegenerating)
            {
                _isRegenerating = true;
            }
            
            if (_enemies.Count > 0 && shooter.GetComponent<Shooter>()._automaticShooting)
            {
                if (_enemies[0] != null)
                {
                    Vector2 dirToEnemy = (Vector2)_enemies[0].transform.position - (Vector2)transform.position;
                    float angle = Mathf.Atan2(dirToEnemy.y, dirToEnemy.x) * Mathf.Rad2Deg - 90;
                    rotator.transform.rotation = Quaternion.Euler(0, 0, angle);
                }
            }
            else if (shooter.GetComponent<Shooter>()._automaticShooting == false)
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 dirToMouse = mousePos - (Vector2)transform.position;
                dirToMouse.Normalize();
        
                rotator.transform.up = dirToMouse;
            }

            _input.x = Input.GetAxisRaw("Horizontal");
            _input.y = Input.GetAxisRaw("Vertical");

            Vector2 direction = _rb.velocity.normalized;

            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
                transform.rotation = Quaternion.Euler(0, 0, angle);

                if (GetComponent<SpriteRenderer>().sprite == towerAnimations[0] && _changeTowerAnimation)
                {
                    towerMovingAnimation = towerAnimations[1];
                    _changeTowerAnimation = false;
                }
                else if (GetComponent<SpriteRenderer>().sprite == towerAnimations[1] && _changeTowerAnimation)
                {
                    towerMovingAnimation = towerAnimations[2];
                    _changeTowerAnimation = false;
                }
                else if (GetComponent<SpriteRenderer>().sprite == towerAnimations[2] && _changeTowerAnimation)
                {
                    towerMovingAnimation = towerAnimations[0];
                    _changeTowerAnimation = false;
                }

                if (towerMovingAnimation != null)
                {
                    GetComponent<SpriteRenderer>().sprite = towerMovingAnimation;
                    Invoke(nameof(ChangeAnimation), 0.5f);
                }
            }
        }
    }

    public void Regenerate()
    {
        health += healthRegen;
        _isRegenerating = false;
    }
    private void FixedUpdate()
    {
        if (gameObject != null)
        {
            if (_rb.velocity.magnitude < 0.1f)
            {
                _rb.velocity = Vector2.zero;
            }

            _rb.AddForce(_input * speed);
        }
    }

    private void ChangeAnimation()
    {
        if (gameObject != null)
        {
            _changeTowerAnimation = true;
        }
    }
    
    private void Damage(float damage)
    {
        if (gameObject != null)
        {
            health -= damage;
            Debug.Log("Tower damaged, health: " + health);
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void OnLevelUp()
    {
        Multipliers multipliersScript = this.GetComponent<Multipliers>();
        bodyDamage *= multipliersScript.damageMultiplier;
        health *= multipliersScript.healthMultiplier;
        healthRegen = multipliersScript.healthMultiplier;
        damage *= multipliersScript.damageMultiplier;
        speed *= multipliersScript.moveSpeedMultiplier;
    } 

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (gameObject != null){
            if (other.gameObject.CompareTag("Enemy"))
            {
                //other.gameObject.GetComponent<Enemy>().transform.position -= 1;
                other.gameObject.GetComponent<Enemy>().Damage(bodyDamage);
                Damage(other.gameObject.GetComponent<Enemy>().damage);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject != null)
        {
            if (other.gameObject.CompareTag("Enemy") && !_enemies.Contains(other.gameObject))
            {
                _enemies.Add(other.gameObject);
                Debug.Log("Enemy added: " + other.gameObject.name);
            }

            if (other.gameObject.CompareTag("Projectile") && !_projectiles.Contains(other.gameObject))
            {
                _projectiles.Add(other.gameObject);
                Debug.Log("Projectile added: " + other.gameObject.name);
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (gameObject != null)
        {
            if (other.gameObject.CompareTag("Enemy") && _enemies.Contains(other.gameObject))
            {
                _enemies.Remove(other.gameObject);
                GetComponentInChildren<Shooter>()._wait = true;
                Debug.Log("Enemy removed: " + other.gameObject.name);
            }

            if (other.gameObject.CompareTag("Projectile") && !_projectiles.Contains(other.gameObject))
            {
                _projectiles.Remove(other.gameObject);
                Destroy(other.gameObject);
                Debug.Log("Projectile removed: " + other.gameObject.name);
            }
        }
    }
}