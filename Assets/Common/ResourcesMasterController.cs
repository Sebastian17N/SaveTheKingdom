using Assets.Common.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesMasterController : MonoBehaviour
{
	public GameObject CoinsNumber;
	public ResourcesController _coinsController => CoinsNumber.transform.GetComponent<ResourcesController>();
	
    public GameObject SapphireNumber;
    public ResourcesController _sapphireController => SapphireNumber.transform.GetComponent<ResourcesController>();
	
    public GameObject TopazNumber;
    public ResourcesController _topazController => TopazNumber.transform.GetComponent<ResourcesController>();
   
    public GameObject EmeraldNumber;
    public ResourcesController _emeraldController => EmeraldNumber.transform.GetComponent<ResourcesController>();
    
    public GameObject MoonStoneNumber;
    public ResourcesController _moonStoneController => MoonStoneNumber.transform.GetComponent<ResourcesController>();

    public static void AddAndUpdateResources(RewardType type, int amount)
    {
        var controller = FindObjectOfType<ResourcesMasterController>();

        switch (type)
        {
            case RewardType.Coins:
                controller._coinsController.IncrementResources(amount, type.ToString());
                break;
            case RewardType.Sapphires:
                controller._sapphireController.IncrementResources(amount, type.ToString());
                break;
            case RewardType.Topazes:
                controller._topazController.IncrementResources(amount, type.ToString());
                break;
            case RewardType.Emeralds:
                controller._emeraldController.IncrementResources(amount, type.ToString());
                break;
            case RewardType.MoonStones:
                controller._moonStoneController.IncrementResources(amount, type.ToString());
                break;
        }
    }
    public static void RemoveAndUpdateResources(RewardType type, int amount)
    {
        var controller = FindObjectOfType<ResourcesMasterController>();

        switch (type)
        {
            case RewardType.Coins:
                controller._coinsController.DecrementResources(amount, type.ToString());
                break;
            case RewardType.Sapphires:
                controller._sapphireController.DecrementResources(amount, type.ToString());
                break;
            case RewardType.Topazes:
                controller._topazController.DecrementResources(amount, type.ToString());
                break;
            case RewardType.Emeralds:
                controller._emeraldController.DecrementResources(amount, type.ToString());
                break;
            case RewardType.MoonStones:
                controller._moonStoneController.DecrementResources(amount, type.ToString());
                break;
        }
    }
    private void DefineResources(RewardType type, int amount)
    {
        var controller = FindObjectOfType<ResourcesMasterController>();

        switch (type)
        {
            case RewardType.Coins:
                controller._coinsController.DecrementResources(amount, type.ToString());
                break;
            case RewardType.Sapphires:
                controller._sapphireController.DecrementResources(amount, type.ToString());
                break;
            case RewardType.Topazes:
                controller._topazController.DecrementResources(amount, type.ToString());
                break;
            case RewardType.Emeralds:
                controller._emeraldController.DecrementResources(amount, type.ToString());
                break;
            case RewardType.MoonStones:
                controller._moonStoneController.DecrementResources(amount, type.ToString());
                break;
        }
    }
}
