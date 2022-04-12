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
    public GameObject PrefabEscapeButton;
    private GameObject _spellMenu;
    void Start()
    {
        SpellMenuButton.onClick.AddListener(OpenSpellMenu);
    }

    private void OpenSpellMenu()
    {
        _spellMenu = Instantiate(PrefabMenu, transform.parent.transform.parent.transform);
    }        
}
