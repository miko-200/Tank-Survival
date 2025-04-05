using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Shooter : MonoBehaviour
{
    public GameObject levelUi;
    public GameObject projectile;
    public PlayerStats pStatsS;
    
    [SerializeField]public Transform[] shootpointsGunPath_1;
    [SerializeField]public Transform[] shootpointsGunPath_2;
    [HideInInspector]public bool _wait = true;
    [SerializeField]public Sprite[] GunPath_1;
    [SerializeField]public Sprite[] GunPath_2;
    public int gunPath = 1;
    public bool _automaticShooting = true;
    
    [HideInInspector]public List<GameObject> _enemies = new();
    private List<GameObject> _projectiles = new();
    

    private float _shotTimer;
    private bool _canShoot = true;
    private bool _isReseting;
    private float waitBeforeFirstShot = 0.02f;
    [HideInInspector]public Sprite GunVariant;
    private SpriteRenderer _sr;
    private int shootpointsNeeded = 1;
    private CircleCollider2D _col;

    private void Start()
    {
        pStatsS = GetComponentInParent<PlayerStats>();
        levelUi = GameObject.FindGameObjectWithTag("Level");
        _sr = GetComponent<SpriteRenderer>();
        GunVariant = GunPath_1[0];
        _sr.sprite = GunVariant;
        _col = GetComponent<CircleCollider2D>();
        pStatsS.range = pStatsS.rangeDefault;
        _col.radius = pStatsS.range;
    }

    private void Update()
    {
        if (pStatsS.level == 15)
        {
            shootpointsNeeded = 2;
        }
        else if (pStatsS.level == 30)
        {
            shootpointsNeeded = 4;
        }
        else if (pStatsS.level == 45)
        {
            shootpointsNeeded = 8;
        }
        _sr.sprite = GunVariant;

        if (Input.GetKeyDown(KeyCode.E) && !_automaticShooting)
        {
            _automaticShooting = true;
        }
        else if (Input.GetKeyDown(KeyCode.E) && _automaticShooting)
        {
            _automaticShooting = false;
        }
        if ((_enemies.Count > 0 && _canShoot && _automaticShooting) || (Input.GetMouseButtonDown(0) && !_automaticShooting && _canShoot))
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
                    Debug.Log("Projectile spawned at " + shootpoint.position);
                    g1.transform.rotation = Quaternion.Euler(0, 0, g1.transform.rotation.eulerAngles.z);
                    g1.GetComponent<Projectile>().Init();
                }

                if (!_isReseting)
                {
                    _isReseting = true;
                    Invoke(nameof(ResetShot), pStatsS.firerate);
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
