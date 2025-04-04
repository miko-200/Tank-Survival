using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Vector2 = System.Numerics.Vector2;

public class ShowStats : MonoBehaviour
{
    public RectTransform panel;
    private bool _isShown = false;
    private bool _isMoving = false;
    private Vector3 targetPosition;
    private float speed = 10f;
    private float threshold = 0.1f;
    private float startPosition = 1260f; 
    private float endPosition = 660f;

    void Start()
    {
        targetPosition = panel.anchoredPosition; // Store initial position
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            TogglePanel();
        }
        if (_isMoving)
        {
            // Smoothly move the panel towards the target position
            panel.anchoredPosition = Vector3.Lerp(panel.anchoredPosition, targetPosition, Time.deltaTime * speed);

            // Check if the panel is close enough to stop moving
            if (Vector3.Distance(panel.anchoredPosition, targetPosition) < threshold)
            {
                panel.anchoredPosition = targetPosition; // Snap to final position
                _isMoving = false; // Allow function calls again
            }
        }
    }

    public void TogglePanel()
    {
        _isShown = !_isShown;
        float panelPos = _isShown ? endPosition : startPosition;
        targetPosition = new Vector3(panelPos, panel.anchoredPosition.y, 0);
        _isMoving = true;
    }
}
