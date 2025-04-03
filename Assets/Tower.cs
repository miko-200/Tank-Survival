using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public GameObject rotator;
    public GameObject shooter;
    public PauseGame pause;
    public GameOver gameOver;
    public TextMeshProUGUI HPText;
    public TextMeshProUGUI HPMaxText;
    public TextMeshProUGUI HRText;
    public TextMeshProUGUI DMGText;
    public TextMeshProUGUI DMGMulText;
    public TextMeshProUGUI MoveText;
    public float speed = 15f;
    private float speedUsing;
    public float range = 6f;
    public float healthMax = 10f;
    private float healthMaxUsing;
    public float health = 1;
    private float healthRegen;
    public bool _isRegenerating = false;
    public float damage = 1f;
    public float damageUsing;
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

    private void Start()
    {
        gameObject.SetActive(true);
        Multipliers multipliersScript = this.GetComponent<Multipliers>();
        bodyDamage *= multipliersScript.damageMultiplier;
        healthMaxUsing = healthMax * multipliersScript.healthMultiplier;
        healthRegen = multipliersScript.healthMultiplier;
        damageUsing = damage * multipliersScript.damageMultiplier;
        speedUsing = speed * multipliersScript.moveSpeedMultiplier;
        health = healthMax;
        HPMaxText.text = healthMax.ToString();
        HRText.text = healthRegen.ToString();
        DMGText.text = damage.ToString();
        DMGMulText.text = multipliersScript.damageMultiplier.ToString();
        MoveText.text = speed.ToString();
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
        if (_isRegenerating)
        {
            Invoke(nameof(ResetRegenerate), 1);
        }
        if (gameObject != null)
        {
            if (_enemies.Count > 0 && shooter.GetComponent<Shooter>()._automaticShooting == true)
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
                if (shooter.GetComponent<Shooter>()._automaticShooting != false)
                {
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
                    transform.rotation = Quaternion.Euler(0, 0, angle);
                }

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

    private void ResetRegenerate()
    {
        _isRegenerating = false;
    }
    public void Regenerate()
    {
        _isRegenerating = true;
        if (health < healthMaxUsing)
        health += healthRegen;
        HPText.text = health.ToString();
    }
    private void FixedUpdate()
    {
        if (gameObject != null)
        {
            if (_rb.velocity.magnitude < 0.1f)
            {
                _rb.velocity = Vector2.zero;
            }

            _rb.AddForce(_input * speedUsing);
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
            HPText.text = health.ToString();
            Debug.Log("Tower damaged, health: " + health);
            if (health <= 0)
            {
                health = 0;
                HPText.text = health.ToString();
                Debug.Log("BeforePause");
                pause.TogglePause();
                Debug.Log("AfterPause");
                gameOver.GameOverTimer();
                gameObject.SetActive(false);
            }
        }
    }

    public void OnLevelUp()
    {
        Multipliers multipliersScript = this.GetComponent<Multipliers>();
        bodyDamage *= multipliersScript.damageMultiplier;
        healthMaxUsing = healthMax * multipliersScript.healthMultiplier;
        healthRegen = multipliersScript.healthMultiplier;
        damageUsing = damage * multipliersScript.damageMultiplier;
        speedUsing = speed * multipliersScript.moveSpeedMultiplier;
        HPMaxText.text = healthMax.ToString();
        HRText.text = healthRegen.ToString();
        DMGText.text = damageUsing.ToString();
        DMGMulText.text = multipliersScript.damageMultiplier.ToString();
        MoveText.text = speedUsing.ToString();
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