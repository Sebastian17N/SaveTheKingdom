using System.Collections;
using System.Collections.Generic;
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

        //var Icon = scriptableObject.Icon;
        //var Damege = scriptableObject.BulletType.Damage;
        //unit.GetComponentInChildren<RawImage>().texture = Icon;
        //unit.GetComponentInChildren<TMP_Text>().text = $"Damage: {Damege}";
    }
}
