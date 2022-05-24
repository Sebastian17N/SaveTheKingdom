using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitSlot : MonoBehaviour
{
    public GameObject PrefabUnitCard;
    public UnitScriptableObject[] ScriptableObjects;

    void Start()
    {
        foreach (UnitScriptableObject scriptableObject in ScriptableObjects)
        {
            CreateUnitFolder(scriptableObject);
        }
    }

    private void CreateUnitFolder(UnitScriptableObject scriptableObject)
    {
        GameObject unit = Instantiate(PrefabUnitCard);
        unit.transform.SetParent(transform);
        unit.GetComponentInChildren<Image>().sprite = scriptableObject.Sprite;
        unit.GetComponentInChildren<Image>().type = Image.Type.Filled;
        var damageObject = unit.transform.Find("Damage");
        damageObject.transform.Find("DamageNumber").GetComponent<TMP_Text>().text = $"Damage: {scriptableObject.AttackDamage}";
        var healthObject = unit.transform.Find("Health");
        healthObject.transform.Find("HealthNumber").GetComponent<TMP_Text>().text = $"Health: {scriptableObject.Health}";
     
        //var Icon = scriptableObject.Icon;
        //var Damege = scriptableObject.BulletType.Damage;
        //unit.GetComponentInChildren<RawImage>().texture = Icon;

    }
}
