using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject target;
    public float smoothing;

    private void FixedUpdate()
    {
        Vector3 targetPosition = new Vector3(target.transform.position.x, target.transform.position.y, -10f); // Lock Z at -10
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
    }
}
