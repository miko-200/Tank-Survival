using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    public GameObject CanvasShadow;
    public GameObject UpgradePanel1;
    public GameObject UpgradePanel2;
    public GameObject UpgradePanel3;
    
    public TextMeshProUGUI UpgradePanel1Text;
    public TextMeshProUGUI UpgradePanel2Text;
    public TextMeshProUGUI UpgradePanel3Text;
    
    private List<string> upgradesText = new List<string>() {"Get 10% more health", "Get 20% more damage", "Get 10% more speed", "Get 10% more health regen", "Get 10% more Bullet Duration", "Get 10% more fire rate"};
    private int randomUpgrade1;
    private int randomUpgrade2;
    private int randomUpgrade3;

    public void ShowUpgrades(int count)
    {
        CanvasShadow.SetActive(true);
        if (count == 1)
        {
            UpgradePanel1.SetActive(true);
        }
        else if (count == 2)
        {
            UpgradePanel2.SetActive(true);
            UpgradePanel2.SetActive(true);
        } else if (count == 3)
        {
            UpgradePanel1.SetActive(true);
            UpgradePanel2.SetActive(true);
            UpgradePanel3.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
