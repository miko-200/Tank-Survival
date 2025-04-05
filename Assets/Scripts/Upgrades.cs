using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Upgrades : MonoBehaviour
{
    public PauseGame PauseScriptHolder;
    public Multipliers MultiplierScriptHolder;
    [FormerlySerializedAs("TowerScriptHolder")] public PlayerStats playerStatsScriptHolder;
    public Shooter ShooterScriptHolder;
    public Projectile ProjectileScriptHolder;
    
    public GameObject CanvasShadow;
    public GameObject UpgradePanel1;
    public GameObject UpgradePanel2;
    public GameObject UpgradePanel3;
    
    public TextMeshProUGUI UpgradePanel1Text;
    public TextMeshProUGUI UpgradePanel2Text;
    public TextMeshProUGUI UpgradePanel3Text;
    
    [SerializeField] public Sprite[] UpgradePanelImage;
    
    public Image UpgradePanel1Image;
    public Image UpgradePanel2Image;
    public Image UpgradePanel3Image;
    
    private List<string> upgradesText = new List<string>() {"Get 10% more health", "Get 20% more damage", "Get 10% more speed", "Get 10% more health regen", "Get 10% more Bullet Duration", "Get 10% more fire rate", "Get a second gun behind the 1st one", "Get a second gun next to the 1st gun", "Get two guns from both sides", "Get 2 guns from behind", "Get additional 4 guns from all corners", "Get additional guns from both sides"};
    
    private int randomUpgrade1;
    private int randomUpgrade2;
    private int randomUpgrade3;

    public void ShowUpgrades(int count)
    {
        CanvasShadow.SetActive(true);
        if (count == 1)
        {
            UpgradePanel1.SetActive(true);

            if (ShooterScriptHolder.gunPath == 1)
            {
                if (playerStatsScriptHolder.level == 30)
                {
                    randomUpgrade1 = upgradesText.Count - 4;
                }
                else if(playerStatsScriptHolder.level == 45)
                {
                    randomUpgrade1 = upgradesText.Count - 2;
                }
            }
            else if (ShooterScriptHolder.gunPath == 2)
            {
                if (playerStatsScriptHolder.level == 30)
                {
                    randomUpgrade1 = upgradesText.Count - 3;
                } 
                else if (playerStatsScriptHolder.level == 45)
                {
                    randomUpgrade1 = upgradesText.Count - 1;
                }
            }

            UpgradePanel1Text.text = upgradesText[randomUpgrade1];
            
            UpgradePanel1Image.sprite = UpgradePanelImage[randomUpgrade1];
        }
        else if (count == 2)
        {
            UpgradePanel1.SetActive(true);
            UpgradePanel2.SetActive(true);
            
            randomUpgrade1 = upgradesText.Count-6;
            randomUpgrade2 = upgradesText.Count-5;
            
            UpgradePanel1Text.text = upgradesText[randomUpgrade1];
            UpgradePanel2Text.text = upgradesText[randomUpgrade2];
            
            UpgradePanel1Image.sprite = UpgradePanelImage[randomUpgrade1];
            UpgradePanel2Image.sprite = UpgradePanelImage[randomUpgrade2];
        } else if (count == 3)
        {
            UpgradePanel1.SetActive(true);
            UpgradePanel2.SetActive(true);
            UpgradePanel3.SetActive(true);
            
            randomUpgrade1 = Random.Range(0, upgradesText.Count-6);
            randomUpgrade2 = Random.Range(0, upgradesText.Count-6);
            randomUpgrade3 = Random.Range(0, upgradesText.Count-6);
            
            UpgradePanel1Text.text = upgradesText[randomUpgrade1];
            UpgradePanel2Text.text = upgradesText[randomUpgrade2];
            UpgradePanel3Text.text = upgradesText[randomUpgrade3];
            
            UpgradePanel1Image.sprite = UpgradePanelImage[randomUpgrade1];
            UpgradePanel2Image.sprite = UpgradePanelImage[randomUpgrade2];
            UpgradePanel3Image.sprite = UpgradePanelImage[randomUpgrade3];
        }
        PauseScriptHolder.TogglePause();
    }

    public void Upgrade1()
    {
        ChooseUpgrade(1);
    }

    public void Upgrade2()
    {
        ChooseUpgrade(2);
    }

    public void Upgrade3()
    {
        ChooseUpgrade(3);
    }
    
    public void ChooseUpgrade(int upgrade)
    {
        int randomUpgrade = 0;
        if (upgrade == 1)
        {
            randomUpgrade = randomUpgrade1;
        }
        else if (upgrade == 2)
        {
            randomUpgrade = randomUpgrade2;
        }
        else if (upgrade == 3)
        {
            randomUpgrade = randomUpgrade3;
        }
        switch (randomUpgrade)
        {
            case 0:
            {
                MultiplierScriptHolder.healthMultiplier += 0.1f;
                break;
            }
            case 1:
            {
                MultiplierScriptHolder.damageMultiplier += 0.2f;
                break;
            }
            case 2:
            {
                MultiplierScriptHolder.moveSpeedMultiplier += 0.1f;
                break;
            }
            case 3:
            {
                MultiplierScriptHolder.healthRegenMultiplier += 0.1f;
                break;
            }
            case 4:
            {
                MultiplierScriptHolder.bulletLifeTime += 0.1f;
                break;
            }
            case 5:
            {
                MultiplierScriptHolder.fireRateMultiplier += 0.1f;
                break;
            }
            case 6:
            {
                ShooterScriptHolder.gunPath = 1;
                ShooterScriptHolder.GunVariant = ShooterScriptHolder.GunPath_1[1];
                break;
            }
            case 7:
            {
                ShooterScriptHolder.gunPath = 2;
                ShooterScriptHolder.GunVariant = ShooterScriptHolder.GunPath_2[1];
                break;
            }
            case 8:
            {
                ShooterScriptHolder.GunVariant = ShooterScriptHolder.GunPath_1[2];
                break;
            }
            case 9:
            {
                ShooterScriptHolder.GunVariant = ShooterScriptHolder.GunPath_2[2];
                break;
            }
            case 10:
            {
                ShooterScriptHolder.GunVariant = ShooterScriptHolder.GunPath_1[3];
                break;
            }
            case 11:
            {
                ShooterScriptHolder.GunVariant = ShooterScriptHolder.GunPath_2[3];
                break;
            }
        }
        HideUpgrades();
        playerStatsScriptHolder.OnLevelUp();
        //ProjectileScriptHolder.OnLevelUp();
    }

    private void HideUpgrades()
    {
        UpgradePanel1.SetActive(false);
        UpgradePanel2.SetActive(false);
        UpgradePanel3.SetActive(false);
        
        PauseScriptHolder.TogglePause();
    }
}