using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public Text timerText;
    public float timerStartTime;
    
    private float minutes;
    private string under10s;
    private float multiplier = 1.1f;
    private void Start()
    {
        timerStartTime = Time.time;
        InvokeRepeating(nameof(TimerTick), 0f, 1f);
    }

    void TimerTick()
    {
        if (Mathf.Ceil(Time.time - timerStartTime) % 10 == 0 && Mathf.Ceil(Time.time - timerStartTime) != 0)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                if (enemy.GetComponent<Enemy>() != null)
                {
                    Enemy enemyScript = enemy.GetComponent<Enemy>();
                    enemyScript.xpAmount *= multiplier*multiplier;
                    enemyScript.damage *= multiplier;
                    enemyScript.health *= multiplier;
                }
            }
        }
        if (Mathf.Ceil(Time.time - timerStartTime) % 60 == 0)
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
