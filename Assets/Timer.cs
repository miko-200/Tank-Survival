using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public Text timerText;
    public float timerStartTime;
    [HideInInspector]public float health;
    [HideInInspector]public float damage;
    [HideInInspector]public float xpAmount;
    
    private float minutes;
    private string under10s;
    private float multiplier = 1.1f;
    private void Start()
    {
        GameObject enemy = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>().enemy;
        health = enemy.GetComponent<Enemy>().health;
        damage = enemy.GetComponent<Enemy>().damage;
        xpAmount = enemy.GetComponent<Enemy>().xpAmount;
        timerStartTime = Time.time;
        InvokeRepeating(nameof(TimerTick), 0f, 1f);
    }

    void TimerTick()
    {
        if (Mathf.Ceil(Time.time - timerStartTime) % 10 == 0 && Mathf.Ceil(Time.time - timerStartTime) != 0)
        {
            xpAmount *= multiplier*multiplier;
            health *= multiplier;
            damage *= multiplier;
            
        }
        if (Mathf.Ceil(Time.time - timerStartTime) % 60 == 0 && Mathf.Ceil(Time.time - timerStartTime) != 0)
        {
            minutes++;
            multiplier += 0.1f;
            timerStartTime = Time.time;
        }

        if (Mathf.Ceil(Time.time - timerStartTime) % 60 < 10)
        {
            under10s = "0";
        }
        else
        {
            under10s = "";
        }
        timerText.text = minutes + ":" + under10s + Mathf.Ceil(Time.time - timerStartTime);
        Debug.Log("Tick: " + Time.time);
    }
}
