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
    public Texture Icon;

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
        GameObject spell = Instantiate(Prefab);
        Icon = spellScriptableObject.Sprite;
        spell.GetComponentInChildren<RawImage>().texture = Icon;

        return spell;
    }

    void Update()
    {
        
    }
}
