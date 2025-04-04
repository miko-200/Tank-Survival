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
    

    private float _shotTimer;
    private bool _canShoot = true;
    private bool _isReseting;
    private float waitBeforeFirstShot = 0.02f;
    private Sprite GunVariant;
    private SpriteRenderer _sr;
    private int shootpointsNeeded = 1;

    private void Start()
    {
        pStatsS = GetComponentInParent<PlayerStats>();
        levelUi = GameObject.FindGameObjectWithTag("Level");
        _sr = GetComponent<SpriteRenderer>();
        GunVariant = GunPath_1[0];
        _sr.sprite = GunVariant;
    }

    private void Update()
    {
        if (levelUi.GetComponent<Level>().level == 15)
        {
            if (gunPath == 1)
            {
                GunVariant = GunPath_1[1];
            }
            else
            {
                GunVariant = GunPath_2[1];
            }

            shootpointsNeeded = 2;
        }
        else if (levelUi.GetComponent<Level>().level == 30)
        {
            if (gunPath == 1)
            {
                GunVariant = GunPath_1[2];
            }
            else
            {
                GunVariant = GunPath_2[2];
            }
            shootpointsNeeded = 4;
        }
        else if (levelUi.GetComponent<Level>().level == 45)
        {
            if (gunPath == 1)
            {
                GunVariant = GunPath_1[3];
            }
            else
            {
                GunVariant = GunPath_2[3];
            }
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
        PlayerMovement playerMovement = GetComponentInParent<PlayerMovement>();
        if ((playerMovement._enemies.Count > 0 && _canShoot && _automaticShooting) || (Input.GetMouseButtonDown(0) && !_automaticShooting && _canShoot))
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
}
