using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellMenu : MonoBehaviour
{
    [Header("MenuElements")]
    public GameObject PrefabMenu;
    public Button SpellMenuButton;
    private GameObject _spellMenu;

    public List<GameObject> SpellDescriptionCards;
    public SpellScriptableObject[] ScriptableObjects;
    public GameObject Prefab;
    public Transform SpellDescriptionMenuTransform;
    public Transform CanvasTransform;

    void Start()
    {
        SpellMenuButton.onClick.AddListener(OpenSpellMenu);

        SpellDescriptionCards = new List<GameObject>();
        foreach (SpellScriptableObject spellDescriptionScriptableObject in ScriptableObjects)
        {
            SpellDescriptionCards.Add(CreateSpell(spellDescriptionScriptableObject));
        }
    }

    private void OpenSpellMenu()
    {
        _spellMenu = Instantiate(PrefabMenu, transform.parent.transform.parent.transform);
    }

    public GameObject CreateSpell(SpellScriptableObject spellScriptableObject)
    {

        GameObject spell = Instantiate(Prefab);
        spell.transform.SetParent(transform.Find("SpellDescriptionBackground(Clone)"));
        spell.GetComponentInChildren<Image>().sprite = spellScriptableObject.Sprite;
        spell.GetComponentInChildren<Image>().type = Image.Type.Filled; //sprite ma wype³nic przestrzeæ image

        //SpellDescription manager = spell.GetComponent<SpellDescription>();
        //manager.Sprite = spellScriptableObject.Sprite;
        //manager.CanvasTransform = CanvasTransform;
        //manager.IsFromMenu = true;

        return spell;
    }
}
