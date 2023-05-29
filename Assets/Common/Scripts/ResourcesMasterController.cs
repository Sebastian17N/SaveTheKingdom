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
    
    /// <summary>
    /// funkja s³u¿¹ca do dowawania i odejmowania zasobów 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="amount">wartoœæ mo¿e byæ ujemna, wtedy zasób zostanie odjêty</param>
    public static void AddAndUpdateResources(RewardType type, int amount)
    {
        var masterController = FindObjectOfType<ResourcesMasterController>();
        ResourcesController controller = null;

        switch (type)
        {
            case RewardType.Coins:
                controller = masterController._coinsController;
                break;
            case RewardType.Sapphires:
                controller = masterController._sapphireController;
                break;
            case RewardType.Topazes:
                controller = masterController._topazController;
                break;
            case RewardType.Emeralds:
                controller = masterController._emeraldController;
                break;
            case RewardType.MoonStones:
                controller = masterController._moonStoneController;
                break;
        }

        if (controller == null)
            return;
        
        controller.IncrementResources(amount, type.ToString());
    }
}
