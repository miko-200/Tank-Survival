using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject projectile;
    public Transform shootpoint;
    public float timeBetweenShots = 1f;
    [HideInInspector]public bool _wait = true;
    

    private float _shotTimer;
    private bool _canShoot = false;
    private bool _isReseting;
    private float waitBeforeFirstShot = (float)0.02;
    private void Update()
    {
        Tower tower = GetComponentInParent<Tower>();
        if (tower._enemies.Count > 0 && _canShoot)
        {
            Invoke(nameof(Wait), waitBeforeFirstShot);
            if (!_wait)
            {
                _canShoot = false;
                GameObject g = Instantiate(projectile, shootpoint.position, shootpoint.rotation);
                Debug.Log("Projectile spawned at " + shootpoint.position);
                g.transform.rotation = Quaternion.Euler(0, 0, g.transform.rotation.eulerAngles.z);
                g.GetComponent<Projectile>().Init();

                if (!_isReseting)
                {
                    _isReseting = true;
                    Invoke(nameof(ResetShot), timeBetweenShots);
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
