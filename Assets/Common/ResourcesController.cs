using Assets.Common.Enums;
using Assets.Common.JsonModel;
using TMPro;
using UnityEngine;

public class ResourcesController : MonoBehaviour
{
    public int Quantity;
    public ResourcesTypeEnum Type;
    
    public ResourcesController(string resouresType, int quantity)
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

        AssignmentResoures();
        ShowResoures($"{Type}");
    }
    
    public void AssignmentResoures()
    {
        Quantity = PlayerPreferences.LoadResourceByType(Type);
    }
    
    public void ShowResoures(string resouresName)
    {
        AssignmentResoures();
        transform.Find("NumberText").GetComponent<TMP_Text>().text = Quantity.ToString();
    }
    
    public void IncrementResoures(int resouresToAdd, string resouresType)
    {
        Quantity = PlayerPreferences.LoadResourceByType(resouresType) + resouresToAdd;
        PlayerPreferences.SaveResourceByType(resouresType, Quantity);

        ShowResoures(resouresType);
    }
    
    public void DecrementResoures(int resouresToRemove, string resouresType)
    {
        IncrementResoures(-resouresToRemove, resouresType);
    }
}
