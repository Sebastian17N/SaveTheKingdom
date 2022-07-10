using System;
using System.Collections;
using System.Collections.Generic;
using Assets.BeforeFight;
using UnityEngine;
using UnityEngine.UI;

public class BeforeBattleUnitGameManager : MonoBehaviour
{
    public List<GameObject> UnitDataFolders;
    public UnitScriptableObject[] UnitScriptableObjects;   
    public GameObject UnitAvailableToChosePrefab;
    public Transform UnitCardUnitTransform;

    public List<GameObject> SpellDataFolders;
    public SpellScriptableObject[] SpellScriptableObjects;
    public GameObject SpellAvailableToChosePrefab;
    public Transform SpellCardUnitTransform;

    public Transform CanvasTransform;
    void Start()
    {
        UnitDataFolders = new List<GameObject>();
        foreach (UnitScriptableObject scriptableObject in UnitScriptableObjects)
        {
            UnitDataFolders.Add(CreateUnitFolder(scriptableObject));
        }

        SpellDataFolders = new List<GameObject>();
        foreach (SpellScriptableObject scriptableObject in SpellScriptableObjects)
        {
            SpellDataFolders.Add(CreateSpellFolder(scriptableObject));
        }
    }       

    private GameObject CreateUnitFolder(UnitScriptableObject unitScriptableObject)
    {
        GameObject unit = Instantiate(UnitAvailableToChosePrefab, UnitCardUnitTransform);
        unit.GetComponentInChildren<Image>().sprite = unitScriptableObject.Icon;
        unit.GetComponentInChildren<Image>().sprite = unitScriptableObject.Sprite;
        unit.GetComponentInChildren<Image>().type = Image.Type.Filled;

        var manager = unit.GetComponent<UnitAvailableToChoose>();
        manager.Sprite = unitScriptableObject.Sprite;
        manager.CanvasTransform = CanvasTransform;

        return unit;
    }

    private GameObject CreateSpellFolder(SpellScriptableObject spellScriptableObject)
    {
        GameObject spell = Instantiate(SpellAvailableToChosePrefab, SpellCardUnitTransform);
        spell.transform.SetParent(SpellCardUnitTransform);        
        spell.GetComponentInChildren<Image>().sprite = spellScriptableObject.Sprite;
        spell.GetComponentInChildren<Image>().type = Image.Type.Filled;

        var manager = spell.GetComponent<SpellAvailableToChoose>();
        manager.Sprite = spellScriptableObject.Sprite;
        manager.CanvasTransform = CanvasTransform;

        return spell;
    }
}
