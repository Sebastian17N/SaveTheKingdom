using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMenu : MonoBehaviour
{
    public GameObject PrefabMenu;
    public bool CanClickOnBuilding = true;

    private void OnMouseDown()
    {

        OpenMenu();

    }

    private void OpenMenu()
    {
        var menu = Instantiate(PrefabMenu);
        var otherBuildings = GetComponent<ChangingBuildingsViews>().IsActive = false;
    }
}
