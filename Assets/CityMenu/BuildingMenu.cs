using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMenu : MonoBehaviour
{
    public GameObject PrefabMenu;
    public GameObject UnitFolderPrefab;
    public bool CanClickOnBuilding = true;
    public Vector2 UnitDataFolderSpawnPoint;

    private void OnMouseDown()
    {

        OpenMenu();
        CreateUnitFolder();
    }

    private void OpenMenu()
    {
        var menu = Instantiate(PrefabMenu);
        //var otherBuildings = GetComponent<ChangingBuildingsViews>().IsActive = false;
    }
    private void CreateUnitFolder()
    {
        var folder = Instantiate(UnitFolderPrefab);
        folder.transform.position = transform.position + transform.rotation * (Vector2)UnitDataFolderSpawnPoint;
    }
}
