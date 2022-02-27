using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMenu : MonoBehaviour
{
    public GameObject PrefabMenu;
    public GameObject UnitDataFolder;    
    public bool CanClickOnBuilding = true;
    public Vector2 UnitDataFolderSpawnPoint;
    public GameObject Menu;
    public GameObject EscapeButton;

    private void OnMouseDown()
    {
        OpenMenu();
        CreateUnitFolder();
        CreateEscapeButton();

    }

    private void OpenMenu()
    {
        Menu = Instantiate(PrefabMenu);
        Menu.transform.parent = transform.parent;
        transform.parent.GetComponent<CityGameManager>().SetActiveButton(false);
    }
    private void CreateUnitFolder()
    {
        var folder = Instantiate(UnitDataFolder, Menu.transform); //menu jest tutaj automatycznie parentem
        folder.transform.position = transform.position + transform.rotation * (Vector2)UnitDataFolderSpawnPoint;
        folder.transform.position = new Vector3(folder.transform.position.x, folder.transform.position.y, -9);
    }
    private void CreateEscapeButton()
    {
        var ecsapeButton = Instantiate(EscapeButton, Menu.transform);
    }    
}
