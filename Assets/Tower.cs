using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float speed;
    public GameObject rotator;
    public List<GameObject> _enemies = new();

    //private GameObject enemy;
    private Rigidbody2D _rb;
    private Vector2 _input;
    private List<GameObject> _projectiles = new();

    private GameObject _pickedUpWeapon;

    private void Start()
    {
        _rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
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
        if (_enemies.Count > 0)
        {
            Vector2 dirToEnemy = (Vector2)_enemies[0].transform.position - (Vector2)transform.position;
            float angle = Mathf.Atan2(dirToEnemy.y, dirToEnemy.x) * Mathf.Rad2Deg - 90;
            rotator.transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        _input.x = Input.GetAxisRaw("Horizontal");
        _input.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        _rb.AddForce(_input * speed);
    }

    private void OnTriggerEnter2D(Collider2D other)
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

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && _enemies.Contains(other.gameObject))
        {
            _enemies.Remove(other.gameObject);
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