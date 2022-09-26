using Assets.Scenes.Barracks.Scripts;
using Assets.Units.Defenses.Scripts;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{
    public List<UnitScriptableObject> UnitScriptableObject;
    private BarracksGameManager _barracksGameManager;
    private int SelectedUnit;
    void Start()
    {
        _barracksGameManager = GetComponent<BarracksGameManager>();
    }
    private void UpgradeUnit()
    {
        _barracksGameManager.GetUnitByIndex(SelectedUnit);
        
    }
}
