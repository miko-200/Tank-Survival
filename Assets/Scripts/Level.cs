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
    public PlayerStats pStatsS;
    private int level = 1;
    public float xp = 0f;
    public float xpToNextLevel;
    
    private void Start()
    {
        LevelText = GetComponent<Text>();
        pStatsS = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        level = pStatsS.level;
        xpToNextLevel = pStatsS.xpToNextLevel;
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
            else if (level < 30 && level >= 15)
            {
                xpToNextLevel *= 1.2f;
            } 
            else if (level < 45 && level >= 30)
            {
                xpToNextLevel *= 1.3f;
            }
            else
            {
                xpToNextLevel *= 1.5f;
            }
            pStatsS.xpToNextLevel = xpToNextLevel;
        }
    }
    private void LevelUp()
    {
        pStatsS.level++;
        level = pStatsS.level;
        if (level == 15)
        {
            UpgradesScript.ShowUpgrades(2);
        } else if (level == 30)
        {
            UpgradesScript.ShowUpgrades(1);
        } else if (level == 45)
        {
            UpgradesScript.ShowUpgrades(1);
        }
        else
        {
            UpgradesScript.ShowUpgrades(3);
        }
        
        LevelText.text = "Level: " + level;
    }
}
