using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ColorCustomization : MonoBehaviour
{
    public GameObject chC;
    public Image imgBody;
    public Image imgRotator;
    public Image imgShell;
    
    public SpriteRenderer bodyRenderer;  // Sprite Renderer for prefab
    public SpriteRenderer rotatorRenderer;
    public SpriteRenderer shellRenderer;
    public TrailRenderer trailRenderer;
    
    private List<string> Colors = new List<string>() {"#0095FF","#FF0000", "#00FF00",  "#FFFF00", "#FFA500", "#9000FF", "#FF0FC6", "#FFFFFF", "#4D4D4D" };
    public int bodyColor;
    public int enemyBodyColor;
    public int rotatorColor;
    public int enemyRotatorColor;
    public int shellColor;
    public int enemyShellColor;
    public int trailColor;
    public int enemyTrailColor;
    private List<string> ColorNames = new List<string>() { "Blue", "Red", "Green", "Yellow", "Orange", "Purple", "Pink", "White", "Black" };
    
    public bool _isEnemy = false;

    private void Start()
    {
        if (ColorManager.Instance == null)
        {
            Debug.LogError("ColorManager not found!");
            return;
        }

        bodyColor = ColorManager.Instance.bodyColor;
        enemyBodyColor = ColorManager.Instance.enemyBodyColor;
        rotatorColor = ColorManager.Instance.rotatorColor;
        enemyRotatorColor = ColorManager.Instance.enemyRotatorColor;
        shellColor = ColorManager.Instance.shellColor;
        enemyShellColor = ColorManager.Instance.enemyShellColor;
        trailColor = ColorManager.Instance.trailColor;
        enemyTrailColor = ColorManager.Instance.enemyTrailColor;
        
        if (_isEnemy)
        {
            ChangeColorStart(enemyBodyColor, enemyRotatorColor, enemyShellColor, enemyTrailColor);
        }
        else
        {
            ChangeColorStart(bodyColor, rotatorColor, shellColor, trailColor);
        }
    }
    private void ChangeColorStart(int bodyColorIndex, int rotatorColorIndex, int shellColorIndex, int trailColorIndex)
    {
        if (bodyRenderer != null && rotatorRenderer != null && shellRenderer != null)
        {
            ColorManager.Instance.ChangeColorRend(bodyColorIndex, bodyRenderer);
            ColorManager.Instance.ChangeColorRend(rotatorColorIndex, rotatorRenderer);
            Debug.Log("Color body has been changed!");
        }

        if (shellRenderer != null)
        {
            ColorManager.Instance.ChangeColorRend(shellColorIndex, shellRenderer);
            Debug.Log("Color shell has been changed!");
        }

        if (trailRenderer != null)
        {
            ChangeColorTrail(trailColorIndex, trailRenderer);
            Debug.Log("Color trail has been changed!");
        }

        if (imgBody != null && imgRotator != null)
        {
            ColorManager.Instance.ChangeColorImg(bodyColorIndex, imgBody);
            ColorManager.Instance.ChangeColorImg(rotatorColorIndex, imgRotator);
            Debug.Log("Color has been changed!");
        }

        if (imgShell != null)
        {
            ColorManager.Instance.ChangeColorImg(shellColorIndex, imgShell);
            Debug.Log("Color has been changed!");
        }
    }

    private void ChangeColorTrail(int colorIndex, TrailRenderer trail)
    {
        if (colorIndex >= 0 && colorIndex < Colors.Count)
        {
            string selectedColorHex = Colors[colorIndex];
            Color color;

            if (ColorUtility.TryParseHtmlString(selectedColorHex, out color))
            {
                trail = GetComponent<TrailRenderer>();
                trail.material = new Material(Shader.Find("Sprites/Default"));

                // A simple 2 color gradient with a fixed alpha of 1.0f.
                float alpha = 1.0f;
                Gradient gradient = new Gradient();
                gradient.SetKeys(
                    new GradientColorKey[] { new GradientColorKey(color, 0.0f), new GradientColorKey(color, 1.0f) },
                    new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
                );
                trail.colorGradient = gradient;
                Debug.Log($"Color changed to {ColorNames[colorIndex]} ({selectedColorHex})");
            }
            else
            {
                Debug.LogWarning("Invalid hex color string.");
            }
        }
        else
        {
            Debug.LogWarning("Color index out of range.");
        }
    }
}
