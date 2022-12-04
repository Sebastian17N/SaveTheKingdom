using Assets.Common.Enums;
using Assets.Common.JsonModel;
using TMPro;
using UnityEngine;

public class ResourcesController : MonoBehaviour
{
    public int Quantity;
    public RewardType Type;
    
    public ResourcesController(string resourcesType, int quantity)
    {
        Quantity = quantity;
    }
    
    private void Start()
    {
        //PlayerPrefs.SetInt("Coins", 100000);
        //PlayerPrefs.SetInt("Sapphires", 20000);
        //PlayerPrefs.SetInt("Topazes", 30000);
        //PlayerPrefs.SetInt("Emeralds", 40000);
        //PlayerPrefs.SetInt("MoonStones", 50000);

        AssignmentResources();
        ShowResources($"{Type}");
    }
    
    public void AssignmentResources()
    {
        Quantity = PlayerPreferences.LoadResourceByType(Type);
    }
    
    public void ShowResources(string resourcesName)
    {
        AssignmentResources();
        transform.Find("NumberText").GetComponent<TMP_Text>().text = Quantity.ToString();
    }
    
    public void IncrementResources(int resourcesToAdd, string resourcesType)
    {
        Quantity = PlayerPreferences.LoadResourceByType(resourcesType) + resourcesToAdd;
        PlayerPreferences.SaveResourceByType(resourcesType, Quantity);

        ShowResources(resourcesType);
    }
    
    public void DecrementResources(int resourcesToRemove, string resourcesType)
    {
        IncrementResources(-resourcesToRemove, resourcesType);
    }
}
