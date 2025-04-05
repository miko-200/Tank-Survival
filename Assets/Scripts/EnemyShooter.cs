using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject player;
    public GameObject projectile;
    public EnemyStats eStatsS;
    
    [SerializeField]public Transform[] shootpointsGunPath_1;
    [SerializeField]public Transform[] shootpointsGunPath_2;
    [HideInInspector]public bool _wait = true;
    [SerializeField]public Sprite[] GunPath_1;
    [SerializeField]public Sprite[] GunPath_2;
    public int gunPath = 1;
    public bool _automaticShooting = true;
    [HideInInspector]public bool _playerInRange = false;
    

    private float _shotTimer;
    private bool _canShoot = true;
    private bool _isReseting;
    private float waitBeforeFirstShot = 0.02f;
    private Sprite GunVariant;
    private SpriteRenderer _sr;
    private int shootpointsNeeded = 1;

    private void Start()
    {
        eStatsS = GetComponentInParent<EnemyStats>();
        player = GameObject.FindGameObjectWithTag("Player");
        _sr = GetComponent<SpriteRenderer>();
        GunVariant = GunPath_1[0];
        _sr.sprite = GunVariant;
    }

    private void Update()
    {
        if (_playerInRange)
        {
            EnemyMovement eM = GetComponentInParent<EnemyMovement>();
            Invoke(nameof(eM.Stop), 0.5f);
        }
        if (_playerInRange && _canShoot && _automaticShooting)
        {
            Invoke(nameof(Wait), waitBeforeFirstShot);
            if (!_wait)
            {
                _canShoot = false;
                for (int i = 0; i < shootpointsNeeded; i++)
                {
                    Transform shootpoint;
                    if (gunPath == 1)
                    {
                       shootpoint  = shootpointsGunPath_1[i];
                    }
                    else
                    {
                        shootpoint = shootpointsGunPath_2[i+1];
                    }
                    GameObject g1 = Instantiate(projectile, shootpoint.position, shootpoint.rotation);
                    g1.GetComponent<EnemyProjectile>().Enemy = this.gameObject;
                    Debug.Log("Projectile spawned at " + shootpoint.position);
                    g1.transform.rotation = Quaternion.Euler(0, 0, g1.transform.rotation.eulerAngles.z);
                    g1.GetComponent<EnemyProjectile>().Init();
                }

                if (!_isReseting)
                {
                    _isReseting = true;
                    Invoke(nameof(ResetShot), eStatsS.firerate);
                }
            }
        }
    }

    private void Wait()
    {
        _wait = false;
    }
    private void ResetShot()
    {
        _isReseting = false;
        _canShoot = true;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject != null)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _playerInRange = true;
                Debug.Log("Player in range");
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (gameObject != null)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _playerInRange = false;
                Debug.Log("Player not in range");
            }
        }
    }
}
