using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingMenu : MonoBehaviour
{
    [Header("MenuElements")]
    public GameObject PrefabMenu;
    public GameObject UnitDataFolder;   
    public Vector2 UnitDataFolderSpawnPoint;
    public GameObject Menu;
    public GameObject EscapeButton;
    
    [Header("UnitFolder")]
    public List<GameObject> UnitCards;
    public UnitScriptableObject[] ScriptableObjects;
    public Sprite Sprite;    

    private void OnMouseDown()
    {
        OpenMenu();        
        CreateEscapeButton();
    }

    private void OpenMenu()
    {
        Menu = Instantiate(PrefabMenu, transform);
        transform.parent.GetComponent<CityGameManager>().SetActive(false);

        UnitCards = new List<GameObject>();

        // Load all unitFolders.
        foreach (UnitScriptableObject scriptableObject in ScriptableObjects)
        {
            UnitCards.Add(CreateUnitFolder(scriptableObject));
        }
    }
    private void GenerateUnitFoldersBoard()
    {
        float height = 3f;
        float width = 3f;
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {

            }
                
    }
    private GameObject CreateUnitFolder(UnitScriptableObject unitScriptableObject)
    {
        var folder = Instantiate(UnitDataFolder, Menu.transform.Find("Menu").transform); //menu jest tutaj automatycznie parentem       
        //folder.transform.position = transform.position + transform.rotation * (Vector2)UnitDataFolderSpawnPoint;
        //folder.transform.position = new Vector3(folder.transform.position.x, folder.transform.position.y, -9);

        Sprite = unitScriptableObject.Sprite; //jak podpi¹æ sprite do listy folderów?

        return folder;
    }
    //private GameObject CreateUnit(UnitScriptableObject unitScriptableObject)
    //{
    //    GameObject unit = Instantiate(Prefab, UnitFolderTransform);

    //    Icon = unitScriptableObject.Icon;

    //    unit.GetComponentInChildren<RawImage>().texture = Icon;

    //    UnitCardManager manager = unit.GetComponent<UnitCardManager>();
    //    manager.UnitScriptableObject = unitScriptableObject;
    //    manager.Sprite = unitScriptableObject.Sprite;

    //    return unit;
    //}
    
    private void CreateEscapeButton()
    {
        var ecsapeButton = Instantiate(EscapeButton, Menu.transform);
    }    
}
