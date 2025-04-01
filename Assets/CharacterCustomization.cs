using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CharacterCustomization : MonoBehaviour
{
    public TextMeshProUGUI tankName;
    public TextMeshProUGUI bodyColorName;
    public TextMeshProUGUI rotatorColorName;
    public TextMeshProUGUI shellColorName;
    public Image imgBody;
    public Image imgRotator;
    public Image imgShell;
    
    public SpriteRenderer bodyRenderer;  // Sprite Renderer for prefab
    public SpriteRenderer rotatorRenderer;
    public SpriteRenderer shellRenderer;

    private List<string> Colors = new List<string>() {"#0095FF","#FF0000", "#00FF00",  "#FFFF00", "#FFA500", "#9000FF", "#FF0FC6", "#FFFFFF", "#4D4D4D" };
    private int bodyColor;
    private int rotatorColor;
    private int shellColor;
    private List<string> ColorNames = new List<string>() { "Blue", "Red", "Green", "Yellow", "Orange", "Purple", "Pink", "White", "Black" };

    private void Start()
    {
        tankName.text = "You";
        ChangeColorName(bodyColor, bodyColorName);
        ChangeColor(bodyColor, imgBody, bodyRenderer);
        ChangeColorName(rotatorColor, rotatorColorName);
        ChangeColor(rotatorColor, imgRotator, rotatorRenderer);
        ChangeColorName(shellColor, shellColorName);
        ChangeColor(shellColor, imgShell, shellRenderer);
    }
    
    public void ChangeBodyColorForward()
    {
        ChangeColorButton("body", true);
    }

    public void ChangeBodyColorBackward()
    {
        ChangeColorButton("body", false);
    }

    public void ChangeRotatorColorForward()
    {
        ChangeColorButton("rotator", true);
    }

    public void ChangeRotatorColorBackward()
    {
        ChangeColorButton("rotator", false);
    }

    public void ChangeShellColorForward()
    {
        ChangeColorButton("shell", true);
    }

    public void ChangeShellColorBackward()
    {
        ChangeColorButton("shell", false);
    }
    
    public void ChangeColorButton(string part, bool _isForward)
    {
        //bool _isForward = false;
        int color = 0; // Temporary variable for color index
        Image img = imgBody;
        SpriteRenderer rend = bodyRenderer;
        TextMeshProUGUI text = bodyColorName;

        // Determine which part is being changed
        if (part == "body")
        {
            color = bodyColor;
            img = imgBody;
            rend = bodyRenderer;
            text = bodyColorName;
        }
        else if (part == "rotator")
        {
            color = rotatorColor;
            img = imgRotator;
            rend = rotatorRenderer;
            text = rotatorColorName;
        } 
        else if (part == "shell")
        {
            color = shellColor;
            img = imgShell;
            rend = shellRenderer;
            text = shellColorName;
        }

        // Cycle colors forward or backward
        if (_isForward)
        {
            color = (color + 1) % Colors.Count;
        }
        else
        {
            color = (color - 1 + Colors.Count) % Colors.Count; // Ensures wrap-around correctly
        }

        // Apply the color changes
        if (img != null && rend != null && text != null)
        {
            ChangeColor(color, img, rend);
            ChangeColorName(color, text);
        }

        // Update the stored color index
        if (part == "body") bodyColor = color;
        else if (part == "rotator") rotatorColor = color;
        else if (part == "shell") shellColor = color;
    }

    private void ChangeColorName(int colorIndex, TextMeshProUGUI text)
    {
        text.text = ColorNames[colorIndex];
    }
    private void ChangeColor(int colorIndex, Image img, SpriteRenderer rend)
    {
        if (colorIndex >= 0 && colorIndex < Colors.Count)
        {
            string selectedColorHex = Colors[colorIndex];
            Color color;

            if (ColorUtility.TryParseHtmlString(selectedColorHex, out color))
            {
                // Change panel (UI) colors
                img.color = color;

                // Change sprite prefab colors
                if (rend != null) rend.color = color;

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
