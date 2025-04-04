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
    
    private bool _timerActive = false;
    private float multiplier = 1.0f;
    private TimeSpan timeSpan;
    private bool _timeout = false;
    private void Start()
    {
        GameObject enemy = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>().enemy;
        health = enemy.GetComponent<Enemy>().health;
        damage = enemy.GetComponent<Enemy>().damage;
        xpAmount = enemy.GetComponent<Enemy>().xpAmount;
        _timerActive = true;
    }

    private void Update()
    {
        if (_timerActive)
        {
            time += Time.deltaTime;
            timeSpan = TimeSpan.FromSeconds(time);
            timerText.text = string.Format("{0:D2} : {1:D2}", timeSpan.Minutes, timeSpan.Seconds);
            if (Math.Round(time) % 20 == 0 && Math.Round(time) != 0)
            {
                GameObject enemy = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>().enemy;
                xpAmount = enemy.GetComponent<Enemy>().FixedXPAmount * multiplier;
                health = enemy.GetComponent<Enemy>().FixedHealth * multiplier;
                damage = enemy.GetComponent<Enemy>().FixedDamage * multiplier;
            }
            if (Math.Round(time) % 60 == 0 && Math.Round(time) != 0 && !_timeout)
            {
                _timeout = true;
                multiplier += 0.1f;
                Invoke(nameof(Timeout), 1);
            }
            if (Math.Round(time) % 5 == 0)
            {
                if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>()._isRegenerating == false)
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Regenerate();
                }
                
            }

            timeSpan = TimeSpan.FromSeconds(time);
            timerText.text = string.Format("{0:D2} : {1:D2}", timeSpan.Minutes, timeSpan.Seconds);
            //Debug.Log("Tick: " + time);
        }
        
    }

    private void Timeout()
    {
        _timeout = false;
    }
}
