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
    public float range = 3f;
    public float health = 30f;
    public float damage = 5f;
    public float bodyDamage = 10f;
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
            /*if (Input.GetKeyDown(KeyCode.E) && _enemies.Count > 0)
            {

                _pickedUpWeapon = _enemies[0];
                _pickedUpWeapon.GetComponent<WeaponScript>().isPickedUp = true;
                _pickedUpWeapon.transform.parent = rotator.transform.GetChild(0).transform;
                _pickedUpWeapon.transform.localPosition = new Vector3(0, 0, 0);
                _pickedUpWeapon.transform.localEulerAngles = new Vector3(0, 0, 0);
            }

            if (Input.GetKeyDown(KeyCode.G) && _pickedUpWeapon != null)
            {
                _pickedUpWeapon.transform.parent = null;
                _pickedUpWeapon.GetComponent<WeaponScript>().isPickedUp = false;
                _pickedUpWeapon = null;
            }*/

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

    public void OnLevelUp(float multiplier)
    {
        bodyDamage *= multiplier;
        health *= multiplier;
        damage *= multiplier;
        speed += 0.1f;
        range += 0.1f;
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