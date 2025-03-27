using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorEnemy : MonoBehaviour
{
    public GameObject target;

    public void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (target != null)
        {
            Vector2 dirToTarget = (Vector2)target.transform.position - (Vector2)transform.position;
            float angle = Mathf.Atan2(dirToTarget.y, dirToTarget.x) * Mathf.Rad2Deg - 90;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
    
}
