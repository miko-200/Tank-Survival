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
    public Material trailMaterial;
    
    private List<string> Colors = new List<string>() {"#0095FF","#FF0000", "#00FF00",  "#FFFF00", "#FFA500", "#9000FF", "#FF0FC6", "#FFFFFF", "#4D4D4D", "RGB" };
    public int bodyColor;
    public int enemyBodyColor;
    public int rotatorColor;
    public int enemyRotatorColor;
    public int shellColor;
    public int enemyShellColor;
    public int trailColor;
    public int enemyTrailColor;
    private List<string> ColorNames = new List<string>() { "Blue", "Red", "Green", "Yellow", "Orange", "Purple", "Pink", "White", "Black", "RGB" };
    
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
        if (colorIndex >= 0 && colorIndex < Colors.Count-1)
        {
            string selectedColorHex = Colors[colorIndex];
            Color color;

            if (ColorUtility.TryParseHtmlString(selectedColorHex, out color))
            {
                trail.material = new Material(Shader.Find("Sprites/Default"));

                // A simple 2 color gradient with a fixed alpha of 1.0f.
                Gradient gradient = new Gradient();
                gradient.SetKeys(
                    new GradientColorKey[] { new GradientColorKey(color, 0.0f), new GradientColorKey(color, 1.0f) },
                    new GradientAlphaKey[] {
                        new GradientAlphaKey(1.0f, 0.0f), // Fully visible at start
                        new GradientAlphaKey(0.6f, 0.4f), // Slight fade in the middle
                        new GradientAlphaKey(0.3f, 0.7f),
                        new GradientAlphaKey(0f, 1f)}
                );
                trail.colorGradient = gradient;
                Debug.Log($"Color changed to {ColorNames[colorIndex]} ({selectedColorHex})");
            }
            else
            {
                Debug.LogWarning("Invalid hex color string.");
            }
        }
        else if (colorIndex == Colors.Count-1)
        {
            if (trailMaterial != null)
            {
                trail.material = trailMaterial;
            }
            else
            {
                Debug.LogError("Trail Material is missing! Assign one in the Inspector.");
            }
            // A simple 2 color gradient with a fixed alpha of 1.0f.
            Gradient gradient = new Gradient();
            gradient.SetKeys(
                new GradientColorKey[]
                {
                    new GradientColorKey(Color.red, 0.0f), // Start red
                    new GradientColorKey(Color.yellow, 0.3f), // Mid yellow
                    new GradientColorKey(Color.green, 0.5f), // Mid green
                    new GradientColorKey(Color.blue, 0.7f),
                    new GradientColorKey(Color.magenta, 1.0f) // End blue
                },
                new GradientAlphaKey[]
                {
                    new GradientAlphaKey(1.0f, 0.0f), // Fully visible at start
                    new GradientAlphaKey(0.6f, 0.4f), // Slight fade in the middle
                    new GradientAlphaKey(0.3f, 0.7f),
                    new GradientAlphaKey(0f, 1f)// Fully transparent at the end
                }
            );
            trail.colorGradient = gradient;
            Debug.Log($"Color changed to {ColorNames[colorIndex]}");
        }
        else
        {
            Debug.LogWarning("Color index out of range.");
        }
    }
}
