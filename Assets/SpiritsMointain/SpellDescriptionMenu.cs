using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellDescriptionMenu : MonoBehaviour
{
    public List<GameObject> SpellDescriptionCards;
    public SpellScriptableObject[] ScriptableObjects;
    public GameObject Prefab;    
    public Transform CanvasTransform;

    void Start()
    {        
        SpellDescriptionCards = new List<GameObject>();
        foreach (SpellScriptableObject spellDescriptionScriptableObject in ScriptableObjects)
        {            
            SpellDescriptionCards.Add(CreateSpell(spellDescriptionScriptableObject));
        }
    }

    public GameObject CreateSpell(SpellScriptableObject spellScriptableObject)
    {
        GameObject spell = Instantiate(Prefab, this.transform);
        spell.GetComponentInChildren<Image>().sprite = spellScriptableObject.Sprite;
        spell.GetComponentInChildren<Image>().type = Image.Type.Filled;

        SpellDescriptionCardManager manager = spell.GetComponent<SpellDescriptionCardManager>();
        manager.Sprite = spellScriptableObject.Sprite;
        manager.CanvasTransform = CanvasTransform;
        
        return spell;
    }
            
}
