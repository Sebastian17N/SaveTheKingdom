using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMenu : MonoBehaviour
{
    public GameObject PrefabMenu;
    public GameObject UnitFolderPrefab;
    public bool CanClickOnBuilding = true;
    public Vector2 UnitDataFolderSpawnPoint;
    public GameObject Menu;

    private void OnMouseDown()
    {

        OpenMenu();
        CreateUnitFolder();
    }

    private void OpenMenu()
    {
        Menu = Instantiate(PrefabMenu);
        
        GetComponentInChildren<ChangingBuildingsViews>().IsActive = false;
    }
    private void CreateUnitFolder()
    {
        var folder = Instantiate(UnitFolderPrefab, Menu.transform); //menu jest tutaj automatycznie parentem
        folder.transform.position = transform.position + transform.rotation * (Vector2)UnitDataFolderSpawnPoint;
        folder.transform.position = new Vector3(folder.transform.position.x, folder.transform.position.y, -2);
    }

    //X ma byæ Instantiate i dopiero przypisaæ mu logike
}
