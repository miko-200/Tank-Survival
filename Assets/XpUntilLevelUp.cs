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
        xpText.text = "Xp until level up: " + Mathf.Round(levelUi.GetComponent<Level>().xp) + "/" + Mathf.Round(levelUi.GetComponent<Level>().xpToNextLevel);
    }
}
