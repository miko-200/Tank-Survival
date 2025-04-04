using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerStats : MonoBehaviour
{
    public TextMeshProUGUI HPText;
    public TextMeshProUGUI HPMaxText;
    public TextMeshProUGUI HRText;
    public TextMeshProUGUI DMGText;
    public TextMeshProUGUI DMGMulText;
    public TextMeshProUGUI SpeedText;
    public TextMeshProUGUI FRText;
    public TextMeshProUGUI BulDurText;
    
    public float healthMaxDefault = 10f;
    public float healthRegenDefault = 1;
    [HideInInspector]public bool _isRegenerating = false;
    
    public float damageDefault = 1f;
    public float bodyDamageDefault = 2f;
    public float speedDefault = 8f;
    public float rangeDefault = 6f;
    public float fireRateDefault = 1f;
    public float bulletLifeTimeDefault = 0.6f;
    
    [HideInInspector]public float healthMax = 1;
    [HideInInspector]public float health = 1;
    [HideInInspector]public float healthRegen = 1;
    [HideInInspector]public float damage = 1f;
    [HideInInspector]public float bodyDamage = 1f;
    [HideInInspector]public float speed = 1;
    [HideInInspector]public float range = 1f;
    [HideInInspector]public float firerate = 1f;
    [HideInInspector]public float bulletLifeTime;
    

    //private GameObject enemy;

    private void Start()
    {
        gameObject.SetActive(true);
        Multipliers multipliersScript = this.GetComponent<Multipliers>();
        
        bodyDamageDefault = bodyDamageDefault * multipliersScript.damageMultiplier;
        healthMax = healthMaxDefault * multipliersScript.healthMultiplier;
        healthRegen = healthRegenDefault * multipliersScript.healthMultiplier;
        damage = damageDefault * multipliersScript.damageMultiplier;
        health = healthMaxDefault;
        speed = speedDefault * multipliersScript.moveSpeedMultiplier;
        range = rangeDefault;
        firerate = fireRateDefault / multipliersScript.fireRateMultiplier;
        bulletLifeTime = bulletLifeTimeDefault + multipliersScript.bulletLifeTime;
        
        
        HPMaxText.text = healthMaxDefault.ToString();
        HPText.text = health.ToString();
        HRText.text = healthRegen.ToString();
        DMGText.text = damage.ToString();
        DMGMulText.text = multipliersScript.damageMultiplier.ToString();
        SpeedText.text = speed.ToString();
        FRText.text = Math.Round(firerate, 2).ToString();
        BulDurText.text = Math.Round(bulletLifeTime, 2).ToString();
    }

    private void Update()
    {
        if (_isRegenerating)
        {
            Invoke(nameof(ResetRegenerate), 1);
        }
    }

    private void ResetRegenerate()
    {
        _isRegenerating = false;
    }
    public void Regenerate()
    {
        _isRegenerating = true;
        health += healthRegen;
        if (health > healthMax) health = healthMax;
        HPText.text = Math.Round(health, 2).ToString();
    }

    public void OnLevelUp()
    {
        Multipliers multipliersScript = this.GetComponent<Multipliers>();
        
        bodyDamageDefault *= multipliersScript.damageMultiplier;
        healthMax = healthMaxDefault * multipliersScript.healthMultiplier;
        healthRegen = healthRegenDefault * multipliersScript.healthRegenMultiplier;
        damage = damageDefault * multipliersScript.damageMultiplier;
        speed = speedDefault * multipliersScript.moveSpeedMultiplier;
        firerate = fireRateDefault / multipliersScript.fireRateMultiplier;
        bulletLifeTime = bulletLifeTimeDefault + multipliersScript.bulletLifeTime;
        
        HPMaxText.text = Math.Round(healthMaxDefault, 2).ToString();
        HRText.text = Math.Round(healthRegen, 2).ToString();
        DMGText.text = Math.Round(damage, 2).ToString();
        DMGMulText.text = multipliersScript.damageMultiplier.ToString();
        SpeedText.text = Math.Round(speed, 2).ToString();
        FRText.text = Math.Round(firerate, 2).ToString();
        BulDurText.text = Math.Round(bulletLifeTime, 2).ToString();
    } 
    
    
}