using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityGameManager : MonoBehaviour
{
    public List<GameObject> UnitDataFolder;
    public UnitScriptableObject[] UnitScriptableObject;
    public Sprite Sprite;
    public GameObject Prefab;

    void Start()
    {
        
    }
    public void SetActiveButton(bool active)
    {        
        foreach (var item in GetComponentsInChildren<ChangingBuildingsViews>())
        {
            item.IsActive = active;
        }
    }
    

}