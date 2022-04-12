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
    private GameObject _windowGrid => _menu.transform.Find("WindowGrid").gameObject;

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
        var grid = _windowGrid.transform.Find("UnitStatisticsGrid").gameObject;
        
        GameObject unit = Instantiate(PrefabUnitCard, grid.transform);

        var Icon = scriptableObject.Icon;
        var Damege = scriptableObject.BulletType.Damage;

        unit.GetComponentInChildren<RawImage>().texture = Icon;
        unit.GetComponentInChildren<TMP_Text>().text = $"Damage: {Damege}";
    }
    private void CreateEscapeButton()
    {
        var grid = _windowGrid.transform.Find("UnitStatisticsGrid").gameObject;

        var escapebutton = Instantiate(PrefabEscapeButton, _windowGrid.transform);
        escapebutton.GetComponent<RectTransform>().anchoredPosition =
            new Vector2(_windowGrid.GetComponent<RectTransform>().rect.width / 2,
                        _windowGrid.GetComponent<RectTransform>().rect.height / 2);
        
    }
}
