using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    public SpellScriptableObject SpellScriptableObject;
    public GameObject UpdatePanelPrefab;
    
    public void CreateUpdatePanel()
    {
        var canvas = FindObjectOfType<Canvas>();
        var updatePanel = Instantiate(UpdatePanelPrefab, canvas.transform);

        var unitDataFolder = transform.parent.GetComponent<Image>().sprite;
        updatePanel.transform.Find("UpDateUnitPanel/UnitDataFolder")
            .GetComponent<Image>().sprite = unitDataFolder;

        var damageObject = updatePanel.transform.Find("Damage");
        //damageObject.transform.Find("DamageNumber").GetComponent<TMP_Text>().text = SpellScriptableObject.AttackDamage;
        var healthObject = updatePanel.transform.Find("Health");
        //healthObject.transform.Find("HealthNumber").GetComponent<TMP_Text>().text = scriptableObject.Health;
    }
}
