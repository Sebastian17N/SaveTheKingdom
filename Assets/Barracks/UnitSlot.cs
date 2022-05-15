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
        unit.GetComponentInChildren<TMP_Text>().text = $"Damage: {scriptableObject.AttackDamage}";
        //unit.GetComponentInChildren<TMP_Text>().text = $"Health: {scriptableObject.Health}";

        //var Icon = scriptableObject.Icon;
        //var Damege = scriptableObject.BulletType.Damage;
        //unit.GetComponentInChildren<RawImage>().texture = Icon;

    }
}
