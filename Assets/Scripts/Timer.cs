using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float time;
    [HideInInspector]public float health;
    [HideInInspector]public float damage;
    [HideInInspector]public float xpAmount;
    public float multiplier = 1.0f;
    
    public float regenTimerInterval = 1f;
    public float multiplierTimerInterval = 40f;
    public float enemyStatsMulTimerInterval = 20f;
    [HideInInspector]public bool _canUpgrade;
    
    private bool _timerActive;
    private TimeSpan timeSpan;

    private float regenTimer;
    private float multiplierTimer;
    [HideInInspector]public float enemyStatsMulTimer;
    private void Start()
    {
        _canUpgrade = false;
        _timerActive = true;
    }

    private void Update()
    {
        if (_timerActive)
        {
            float delta = Time.deltaTime;
            time += delta;
            regenTimer += delta;
            multiplierTimer += delta;
            enemyStatsMulTimer += delta;
            timeSpan = TimeSpan.FromSeconds(time);
            timerText.text = string.Format("{0:D2} : {1:D2}", timeSpan.Minutes, timeSpan.Seconds);
            if (multiplierTimer >= multiplierTimerInterval)
            {
                multiplier += 0.1f;
                multiplierTimer -= multiplierTimerInterval;
            }
            if (regenTimer >= regenTimerInterval)
            {
                Debug.Log(enemyStatsMulTimer);
                regenTimer -= regenTimerInterval;
                if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>()._isRegenerating == false)
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Regenerate();
                }
                
            }

            if (enemyStatsMulTimer >= enemyStatsMulTimerInterval - 0.1f)
            {
                _canUpgrade = true;
                if (enemyStatsMulTimer >= enemyStatsMulTimerInterval + 0.2f)
                {
                    enemyStatsMulTimer -= enemyStatsMulTimerInterval + 0.1f;
                    _canUpgrade = false;
                }
            }

            //timeSpan = TimeSpan.FromSeconds(time);
            //timerText.text = string.Format("{0:D2} : {1:D2}", timeSpan.Minutes, timeSpan.Seconds);
            //Debug.Log("Tick: " + time);
        }
        
    }
}
