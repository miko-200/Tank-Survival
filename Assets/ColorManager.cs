using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorManager : MonoBehaviour
{
    public static ColorManager Instance;
    
    public int bodyColor;
    public int enemyBodyColor = 1;
    public int rotatorColor;
    public int enemyRotatorColor = 1;
    public int shellColor;
    public int enemyShellColor = 1;
    public int trailColor;
    public int enemyTrailColor = 1;
    
    private List<string> Colors = new List<string>() {"#0095FF","#FF0000", "#00FF00",  "#FFFF00", "#FFA500", "#9000FF", "#FF0FC6", "#FFFFFF", "#4D4D4D", "RGB"};
    private List<string> ColorNames = new List<string>() { "Blue", "Red", "Green", "Yellow", "Orange", "Purple", "Pink", "White", "Black", "RGB" };

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keeps this object across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void ChangeColorRend(int colorIndex, SpriteRenderer rend)
    {
        if (colorIndex >= 0 && colorIndex < Colors.Count)
        {
            string selectedColorHex = Colors[colorIndex];
            Color color;

            if (ColorUtility.TryParseHtmlString(selectedColorHex, out color))
            {
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
    public void ChangeColorImg(int colorIndex, Image img)
    {
        if (colorIndex >= 0 && colorIndex < Colors.Count)
        {
            string selectedColorHex = Colors[colorIndex];
            Color color;

            if (ColorUtility.TryParseHtmlString(selectedColorHex, out color))
            {
                // Change panel (UI) colors
                if (img != null) img.color = color;
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