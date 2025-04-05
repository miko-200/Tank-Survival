using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;
using TMPro;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    public GameObject rotator;
    public GameObject shooter;
    public PauseGame pause;
    public GameOver gameOver;
    
    private Multipliers multipliersScript;
    private PlayerStats pStatsS;
    
    private Rigidbody2D _rb;
    private CircleCollider2D _col;
    private Vector2 _input;
    
    private List<GameObject> _enemies;
    
    [FormerlySerializedAs("towerAnimations")] [SerializeField] private Sprite[] tankAnimations;
    private Sprite tankMovingAnimation;
    private bool _changeTankAnimation = true;

    private void Start()
    {
        gameObject.SetActive(true);
        multipliersScript = GetComponent<Multipliers>();
        pStatsS = GetComponent<PlayerStats>();
        
        pStatsS.speed = pStatsS.speedDefault * multipliersScript.moveSpeedMultiplier;
        pStatsS.SpeedText.text = pStatsS.speed.ToString();
        if (gameObject != null)
        {
            _rb = this.gameObject.GetComponent<Rigidbody2D>();
            _col = this.gameObject.GetComponent<CircleCollider2D>();
            tankMovingAnimation = this.gameObject.GetComponent<SpriteRenderer>().sprite;
        }
    }

    private void Update()
    {
        if (gameObject != null)
        {
            _enemies = shooter.GetComponent<Shooter>()._enemies;
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
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
                transform.rotation = Quaternion.Euler(0, 0, angle);

                if (GetComponent<SpriteRenderer>().sprite == tankAnimations[0] && _changeTankAnimation)
                {
                    tankMovingAnimation = tankAnimations[1];
                    _changeTankAnimation = false;
                }
                else if (GetComponent<SpriteRenderer>().sprite == tankAnimations[1] && _changeTankAnimation)
                {
                    tankMovingAnimation = tankAnimations[2];
                    _changeTankAnimation = false;
                }
                else if (GetComponent<SpriteRenderer>().sprite == tankAnimations[2] && _changeTankAnimation)
                {
                    tankMovingAnimation = tankAnimations[0];
                    _changeTankAnimation = false;
                }

                if (tankMovingAnimation != null)
                {
                    GetComponent<SpriteRenderer>().sprite = tankMovingAnimation;
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

            _rb.AddForce(_input * pStatsS.speed);
        }
    }
    
    private void ChangeAnimation()
    {
        if (gameObject != null)
        {
            _changeTankAnimation = true;
        }
    }
    
    /*IEnumerator DamageOnCol(Collision2D other, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        //other.gameObject.GetComponent<Enemy>().transform.position -= 1;
        other.gameObject.GetComponent<Enemy>().Damage(playerStatsScript.bodyDamage);
        Damage(other.gameObject.GetComponent<Enemy>().damage);
    }*/
    
    public void Damage(float damage)
    {
        if (gameObject != null)
        {
            pStatsS.health -= damage;
            pStatsS.HPText.text = pStatsS.health.ToString();
            Debug.Log("Tower damaged, health: " + pStatsS.health);
            if (pStatsS.health <= 0)
            {
                pStatsS.health = 0;
                pStatsS.HPText.text = Math.Round(pStatsS.health, 2).ToString();
                Debug.Log("BeforePause");
                pause.TogglePause();
                Debug.Log("AfterPause");
                gameOver.GameOverTimer();
                gameObject.SetActive(false);
            }
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (gameObject != null){
            if (other.gameObject.CompareTag("Enemy"))
            {
                //StartCoroutine(DamageOnCol(other, 0.5f));
                other.gameObject.GetComponent<EnemyStats>().Damage(pStatsS.bodyDamage);
                Damage(other.gameObject.GetComponent<EnemyStats>().bodyDamage);
            }
        }
    }

    /*private void OnCollisionExit2D(Collision2D other)
    {
        if (gameObject != null)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                StopCoroutine(DamageOnCol(other, 0.5f));
            }
        }
    }*/
}
