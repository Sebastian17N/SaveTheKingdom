using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Castle : MonoBehaviour
{
    [Header("MenuElements")]
    public GameObject PrefabMenu;
    public Button CastleButton;
    public GameObject PrefabEscapeButton;

    //[Header("CreatedObjects")]
    private GameObject _menu;

    [Header("UnitFolder")]
    public GameObject PrefabUnitCard;
    public UnitScriptableObject[] ScriptableObjects;

    void Start()
    {
        CastleButton.onClick.AddListener(OpenMenu);
        
    }
    private void OpenMenu()
    {
        _menu = Instantiate(PrefabMenu, transform.parent.transform);
        
        transform.parent.GetComponent<CityGameManager>().SetActive(false);
        
        // Load all unitFolders.
        foreach (UnitScriptableObject scriptableObject in ScriptableObjects)
        {
            CreateUnitFolder(scriptableObject);
        }

        
        CreateEscapeButton();
    }

    private void CreateUnitFolder(UnitScriptableObject scriptableObject)
    {
        var grid = _menu.transform.Find("UnitStatisticsGrid").gameObject;
        
        GameObject unit = Instantiate(PrefabUnitCard, grid.transform);

        var Icon = scriptableObject.Icon;
        var Cost = scriptableObject.BulletType.Damage;

        unit.GetComponentInChildren<RawImage>().texture = Icon;
        unit.GetComponentInChildren<TMP_Text>().text = $"Damage: {Cost}";
    }
    private void CreateEscapeButton()
    {
        var grid = _menu.transform.Find("UnitStatisticsGrid").gameObject;

        _menu = Instantiate(PrefabEscapeButton, grid.transform);      
    }
}
