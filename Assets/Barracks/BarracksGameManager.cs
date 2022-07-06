using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BarracksGameManager : MonoBehaviour
{
    //UnitCard
    public GameObject PrefabUnitCard;
    public UnitScriptableObject[] ScriptableObjects;
    public Transform LaganatUnitSlot;

    //UpdatePanel
    public GameObject UpdatePanelPrefab;
    public TMP_Text DamageNumber;
    public TMP_Text DamageUpgradeNumber;
    public TMP_Text HealthNumber;
    public TMP_Text HealthUpgradeNumber;
    public TMP_Text ShardsHaved;
    public TMP_Text ShardsNeeded;
    public TMP_Text CoinsHaved;
    public TMP_Text CoinsNeeded;   

    void Start()
    {
        foreach (UnitScriptableObject scriptableObject in ScriptableObjects)
        {
            CreateUnitFolder(scriptableObject);
        }
    }
    private void CreateUnitFolder(UnitScriptableObject scriptableObject)
    {
        GameObject unit = Instantiate(PrefabUnitCard, LaganatUnitSlot);        
        unit.GetComponentInChildren<Image>().sprite = scriptableObject.Sprite;
        unit.GetComponentInChildren<Image>().type = Image.Type.Filled;
        var damageObject = unit.transform.Find("Damage");
        damageObject.transform.Find("DamageNumber").GetComponent<TMP_Text>().text = $"Damage: {scriptableObject.AttackDamage}";
        var healthObject = unit.transform.Find("Health");
        healthObject.transform.Find("HealthNumber").GetComponent<TMP_Text>().text = $"Health: {scriptableObject.Health}";
        //var Icon = scriptableObject.Icon;        
        //unit.GetComponentInChildren<RawImage>().texture = Icon;
    }
    public void CreateUpdatePanel(UnitScriptableObject scriptableObject)
    {
        var canvas = FindObjectOfType<Canvas>();
        var updatePanel = Instantiate(UpdatePanelPrefab, canvas.transform);

        var unitDataFolder = transform.parent.GetComponent<Image>().sprite;
        updatePanel.transform.Find("UpDateUnitPanel/UnitDataFolder")
            .GetComponent<Image>().sprite = unitDataFolder;

        var damageObject = updatePanel.transform.Find("Damage");
        damageObject.transform.Find("DamageNumber").GetComponent<TMP_Text>().text = scriptableObject.AttackDamage.ToString();
        var healthObject = updatePanel.transform.Find("Health");
        healthObject.transform.Find("HealthNumber").GetComponent<TMP_Text>().text = scriptableObject.Health.ToString();
    }

    public void NextUnit()
    {

    }
    public void PreviousUnit()
    {

    }
}
