
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject target;
    public GameObject spawner;
    public GameObject rotator;
    private EnemyStats eStatsS;
    
    [SerializeField] private Sprite[] enemyAnimations;
    private Sprite enemyMovingAnimation;
    private bool _changeEnemyAnimation = false;
    private bool _stop = false;
    
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        enemyMovingAnimation = GetComponentInChildren<SpriteRenderer>().sprite;
        eStatsS = GetComponent<EnemyStats>();
    }
    
    private void Update()
    {
        if (target != null)
        {
            Vector2 dirToEnemy = (Vector2)target.transform.position - (Vector2)transform.position;
            float angle = Mathf.Atan2(dirToEnemy.y, dirToEnemy.x) * Mathf.Rad2Deg - 90;
            rotator.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        
        if (!_stop)
        {
            if (target != null)
            {
                Vector2 dirToTarget = (Vector2)target.transform.position - (Vector2)transform.position;
                float angle = Mathf.Atan2(dirToTarget.y, dirToTarget.x) * Mathf.Rad2Deg - 90;
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }

            if (GetComponent<SpriteRenderer>().sprite == enemyAnimations[0] && _changeEnemyAnimation)
            {
                enemyMovingAnimation = enemyAnimations[1];
                _changeEnemyAnimation = false;
            }
            else if (GetComponent<SpriteRenderer>().sprite == enemyAnimations[1] && _changeEnemyAnimation)
            {
                enemyMovingAnimation = enemyAnimations[2];
                _changeEnemyAnimation = false;
            }
            else if (GetComponent<SpriteRenderer>().sprite == enemyAnimations[2] && _changeEnemyAnimation)
            {
                enemyMovingAnimation = enemyAnimations[0];
                _changeEnemyAnimation = false;
            }

            if (enemyMovingAnimation != null)
            {
                GetComponent<SpriteRenderer>().sprite = enemyMovingAnimation;
                Invoke(nameof(ChangeAnimation), 0.5f);
            }
        }
    }

    public void Stop()
    {
        bool _playerInRange = GetComponentInChildren<EnemyShooter>()._playerInRange;
        if (_playerInRange)
        {
            _stop = false;
        }
        else
        {
            _stop = true;
        }
    }
    private void FixedUpdate()
    {
        if (!_stop)
        {
            if (target != null)
            {
                Vector2 dirToTarget = (Vector2)target.transform.position - (Vector2)transform.position;
                dirToTarget.Normalize(); // Normalize to make it a unit vector
                GetComponent<Rigidbody2D>().velocity = dirToTarget * eStatsS.speed;
            }
        }
    }
    
    private void ChangeAnimation()
    {
        _changeEnemyAnimation = true;
    }
}
