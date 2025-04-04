using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailOffset : MonoBehaviour
{
    public Transform projectile;
    public float offsetDistance = 0.5f;

    void Update()
    {
        if (projectile != null)
        {
            Vector2 moveDirection = projectile.up; // Adjust if needed
            transform.position = (Vector2)projectile.position - (moveDirection * offsetDistance);
        }
    }
}
