using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public Text LevelText;
    public Upgrades UpgradesScript;
    public int level = 1;
    public float xp = 0f;
    public float xpToNextLevel = 10;
    
    private void Start()
    {
        LevelText = GetComponent<Text>();
    }

    private void Update()
    {
        if (xp >= xpToNextLevel)
        {
            LevelUp();
            xp -= xpToNextLevel;
            if (level < 15)
            {
                xpToNextLevel *= 1.1f;
            } 
            else if (level < 20 && level >= 15)
            {
                xpToNextLevel *= 1.2f;
            } 
            else if (level < 30 && level >= 20)
            {
                xpToNextLevel *= 1.3f;
            } 
            else if (level < 45 && level >= 30)
            {
                xpToNextLevel *= 1.4f;
            }
            else
            {
                xpToNextLevel *= 1.5f;
            }
        }
    }
    private void LevelUp()
    {
        level++;
        if (level == 15)
        {
            UpgradesScript.ShowUpgrades(2);
        } 
        else
        {
            UpgradesScript.ShowUpgrades(3);
        }
        
        LevelText.text = "Level: " + level;
    }
}
