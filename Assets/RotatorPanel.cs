using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class RotatorPanel : MonoBehaviour
{
    public RectTransform cannon; // The cannon (UI element)
    public Canvas canvas; // UI canvas reference
    public float rotationSpeed = 10f; // Adjust speed for smooth rotation

    void Update()
    {
        if (cannon == null || canvas == null) return;

        // Get the mouse position in screen space
        Vector2 mousePos = Input.mousePosition;

        // Convert the mouse position to local UI space inside the canvas
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            mousePos,
            canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera,
            out Vector2 localMousePos
        );

        // Get direction from cannon to mouse
        Vector2 dirToMouse = localMousePos - cannon.anchoredPosition;
        if (dirToMouse.sqrMagnitude < 1f) return; // Prevent weird small movements

        // Calculate target angle with 90-degree offset
        float targetAngle = Mathf.Atan2(dirToMouse.y, dirToMouse.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);

        // Smooth rotation using Slerp
        cannon.rotation = Quaternion.Slerp(cannon.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
