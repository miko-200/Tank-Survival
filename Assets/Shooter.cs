using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject projectile;
    public Transform shootpoint;
    public float timeBetweenShots = 1f;

    private float _shotTimer;
    private bool _canShoot = true;
    private bool _isReseting;
    private bool _isFacingRightDirection = false;
    private void Update()
    {
        Tower tower = GetComponentInParent<Tower>();
        if (tower._enemies.Count > 0 && _canShoot)
        {
            Invoke(nameof(FacingRightDirection), (float)0.1);
            if (_isFacingRightDirection)
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

    private void FacingRightDirection()
    {
        Tower tower = GetComponentInParent<Tower>();
        if (tower == null)
        {
            Debug.LogError("Tower component is missing in the parent!");
            return;
        }
        if (tower._enemies.Count > 0)
        {
            if (transform.parent == null)
            {
                Debug.LogError("This object doesn't have a parent!");
                return;
            }
            if (transform.parent.parent == null)
            {
                Debug.LogError("This object doesn't have a parent parent!");
                return;
            }
            Vector2 dirToEnemy = (Vector2)tower._enemies[0].transform.parent.parent.position - (Vector2)transform.parent.parent.position;
            float angle = Mathf.Atan2(dirToEnemy.y, dirToEnemy.x) * Mathf.Rad2Deg - 90;
            if (transform.parent.rotation == Quaternion.Euler(0, 0, angle))
            {
                _isFacingRightDirection = true;
            }
            else
            {
                _isFacingRightDirection = false;
            }
        }
    }
    private void ResetShot()
    {
        _isReseting = false;
        _canShoot = true;
    }
}
