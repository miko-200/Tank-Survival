using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class XpUntilLevelUp : MonoBehaviour
{
    public Text xpText;
    public GameObject levelUi;

    private void Start()
    {
        xpText = GetComponent<Text>();
        levelUi = GameObject.FindGameObjectWithTag("Level");
        Debug.Log(levelUi);
    }
    public void Update()
    {
        xpText.text = "Xp: " + Math.Round(levelUi.GetComponent<Level>().xp, 1) + "/" + Math.Round(levelUi.GetComponent<Level>().xpToNextLevel, 1);
    }
}
