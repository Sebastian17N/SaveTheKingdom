using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpiritsMointainGameManager : MonoBehaviour
{
    public List<GameObject> SpellCards;
    public SpellScriptableObject[] ScriptableObjects;
    public GameObject Prefab;
    public Transform SpellMenuTransform;

    void Start()
    {
        SpellCards = new List<GameObject>();
        foreach (SpellScriptableObject spellScriptableObject in ScriptableObjects)
        {
            SpellCards.Add(CreateSpell(spellScriptableObject));
        }
    }

    private GameObject CreateSpell(SpellScriptableObject spellScriptableObject)
    {
        GameObject spell = Instantiate(Prefab, SpellMenuTransform);       
        spell.GetComponentInChildren<Image>().sprite = spellScriptableObject.Sprite;
        spell.GetComponentInChildren<Image>().type = Image.Type.Filled; //sprite ma wype³nic przestrzeæ image


        SpellCardManager manager = spell.GetComponent<SpellCardManager>();
        //manager.SpellScriptableObject = spellScriptableObject;
        manager.Sprite = spellScriptableObject.Sprite;

        return spell;
    }

    void Update()
    {
        
    }
}
