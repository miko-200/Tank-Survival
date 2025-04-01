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
    public TextMeshProUGUI trailColorName;
    public Image imgBody;
    public Image imgRotator;
    public Image imgShell;
    
    public SpriteRenderer bodyRenderer;  // Sprite Renderer for prefab
    public SpriteRenderer rotatorRenderer;
    public SpriteRenderer shellRenderer;

    public GameObject shell;

    public bool _isEnemy = false;
    public bool _isChangeable = false;

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
            if (tankName.text != null)
            {
                tankName.text = "You";
            }
            ChangeColorStart(bodyColor, rotatorColor, shellColor,  trailColor);
        }
    }

    private void ChangeColorStart(int bodyColorIndex, int rotatorColorIndex, int shellColorIndex, int trailColorIndex)
    {
        if (bodyRenderer != null && rotatorRenderer != null && shellRenderer != null)
        {
            ColorManager.Instance.ChangeColorRend(bodyColorIndex, bodyRenderer);
            ColorManager.Instance.ChangeColorRend(rotatorColorIndex, rotatorRenderer);
            ColorManager.Instance.ChangeColorRend(shellColorIndex, shellRenderer);
        }

        if (imgBody != null && imgRotator != null)
        {
            ColorManager.Instance.ChangeColorImg(bodyColorIndex, imgBody);
            ColorManager.Instance.ChangeColorImg(rotatorColorIndex, imgRotator);
        }

        if (imgShell != null)
        {
            ColorManager.Instance.ChangeColorImg(shellColorIndex, imgShell);
        }

        if (bodyColorName != null && rotatorColorName != null && shellColorName != null)
        {
            ChangeColorName(bodyColorIndex, bodyColorName);
            ChangeColorName(rotatorColorIndex, rotatorColorName);
            ChangeColorName(shellColorIndex, shellColorName);
            ChangeColorName(trailColorIndex, trailColorName);
        }
    }

    public void ChangeTankForward()
    {
        if (_isEnemy)
        {
            if (_isChangeable)
            {
                this.gameObject.SetActive(false);
                shell.SetActive(false);
                _isChangeable = false;
                if (tankName.text != null)
                {
                    tankName.text = "You";
                }
            }
            else
            {
                ChangeColorStart(enemyBodyColor, enemyRotatorColor, enemyShellColor, enemyTrailColor);
                this.gameObject.SetActive(true);
                shell.SetActive(true);
                _isChangeable = true;
                if (tankName.text != null)
                {
                    tankName.text = "Enemy";
                }
            }
        }
        else
        {
            if (_isChangeable)
            {
                this.gameObject.SetActive(false);
                shell.SetActive(false);
                _isChangeable = false;
                if (tankName.text != null)
                {
                    tankName.text = "Enemy";
                }
            }
            else
            {
                ChangeColorStart(bodyColor, rotatorColor, shellColor, enemyTrailColor);
                this.gameObject.SetActive(true);
                shell.SetActive(true);
                _isChangeable = true;
                if (tankName.text != null)
                {
                    tankName.text = "You";
                }
            }
        }
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
    
    public void ChangeTrailColorForward()
    {
        ChangeColorButton("trail", true);
    }

    public void ChangeTrailColorBackward()
    {
        ChangeColorButton("trail", false);
    }

    
    public void ChangeColorButton(string part, bool _isForward)
    {
        if (_isChangeable)
        {
            //bool _isForward = false;
            int color = 0; // Temporary variable for color index
            Image img = imgBody;
            SpriteRenderer rend = bodyRenderer;
            TextMeshProUGUI text = bodyColorName;

            // Determine which part is being changed
            if (part == "body")
            {
                if (_isEnemy)
                {
                    color = enemyBodyColor;
                }
                else
                {
                    color = bodyColor;
                }
                img = imgBody;
                rend = bodyRenderer;
                text = bodyColorName;
            }
            else if (part == "rotator")
            {
                if (_isEnemy)
                {
                    color = enemyRotatorColor;
                }
                else
                {
                    color = rotatorColor;
                }
                img = imgRotator;
                rend = rotatorRenderer;
                text = rotatorColorName;
            }
            else if (part == "shell")
            {
                if (_isEnemy)
                {
                    color = enemyShellColor;
                }
                else
                {
                    color = shellColor;
                }
                img = imgShell;
                rend = shellRenderer;
                text = shellColorName;
            }
            else if (part == "trail")
            {
                if (_isEnemy)
                {
                    color = enemyTrailColor;
                }
                else
                {
                    color = trailColor;
                }
                text = shellColorName;
            }

            // Cycle colors forward or backward
            if (_isForward)
            {
                if (color == Colors.Count - 1)
                {
                    color = 0;
                }
                else
                {
                    color++;
                }
            }
            else
            {
                if (color == 0)
                {
                    color = Colors.Count - 1;
                }
                else
                {
                    color--;
                }
            }

            // Apply the color changes
            if (img != null && rend != null)
            {
                ColorManager.Instance.ChangeColorRend(color, rend);
                ColorManager.Instance.ChangeColorImg(color, img);
            }

            if (text != null)
            {
                ChangeColorName(color, text);
            }

            if (!_isEnemy){// Update the stored color index
                if (part == "body") bodyColor = color;
                else if (part == "rotator") rotatorColor = color;
                else if (part == "shell") shellColor = color;
                else if (part == "trail") trailColor = color;
            }
            else
            {
                if (part == "body") enemyBodyColor = color;
                else if (part == "rotator") enemyRotatorColor = color;
                else if (part == "shell") enemyShellColor = color;
                else if (part == "trail") enemyTrailColor = color;
            }
            SaveColors(part);
        }
    }
    
    void SaveColors(string part)
    {
        if (_isEnemy)
        {
            if (part == "body") ColorManager.Instance.enemyBodyColor = enemyBodyColor;
            else if (part == "rotator") ColorManager.Instance.enemyRotatorColor = enemyRotatorColor;
            else if (part == "shell") ColorManager.Instance.enemyShellColor = enemyShellColor;
            else if (part == "trail") ColorManager.Instance.enemyTrailColor = enemyTrailColor;
        }
        else
        {
            if (part == "body") ColorManager.Instance.bodyColor = bodyColor;
            else if (part == "rotator") ColorManager.Instance.rotatorColor = rotatorColor;
            else if (part == "shell") ColorManager.Instance.shellColor = shellColor;
            else if (part == "trail") ColorManager.Instance.trailColor = trailColor;
        }
    }

    private void ChangeColorName(int colorIndex, TextMeshProUGUI text)
    {
        text.text = ColorNames[colorIndex];
    }
}
