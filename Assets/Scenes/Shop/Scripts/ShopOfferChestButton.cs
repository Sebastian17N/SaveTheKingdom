using Assets.Common.Enums;
using Assets.Common.JsonModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopOfferChestButton : MonoBehaviour
{
    public ChestClassification ChestClassification;
    public OpenChestManager OpenChestManager;
    //TO DO: dlaczego ten zapis poni¿ej nie dzia³a do zapisywania w json a tylko dzia³a do odczytywania? dzia³a tylko w przypadku odczytywania danych, NIE przechowuje obiektu
    //public ChestOfferJsonModel ChestOfferJson { get; set; }
    public bool WatchAdd;
    private void Start()
    {
        //ChestOfferJson = ShopJsonLoader.LoadChestOfferJsonModel();
        OpenChestManager = FindObjectOfType<OpenChestManager>();
    }
    public void PickChest()
    {
        OpenChestManager.ChosenChest = ChestClassification;
        var chestOfferJson = ShopJsonLoader.LoadChestOfferJsonModel();
        var totalMoonstones = PlayerPreferences.LoadResourceByType("MoonStones");

        switch (ChestClassification)
        {
            case ChestClassification.Wooden:
                WatchAdd = true;
                
                if (chestOfferJson.NumberOfUsedWoodenChest >= 5)
                    return;

                chestOfferJson.NumberOfUsedWoodenChest++;
                chestOfferJson.ChestClassification = ChestClassification;
                ShopJsonLoader.SaveChestOfferJsonModel(chestOfferJson);

                break;

            case ChestClassification.Silver:
                if (totalMoonstones < 100)
                    return;

                if (chestOfferJson.NumberOfUsedSilverChest >= 5)
                    return;

                chestOfferJson.NumberOfUsedSilverChest++;
                chestOfferJson.ChestClassification = ChestClassification;
                ShopJsonLoader.SaveChestOfferJsonModel(chestOfferJson);
                ResourcesMasterController.AddAndUpdateResources(RewardType.MoonStones, -100);

                break;
            case ChestClassification.Golden:
                if (totalMoonstones < 300)
                    return;

                if (chestOfferJson.NumberOfUsedGoldenChest >= 5)
                    return;

                chestOfferJson.NumberOfUsedGoldenChest++;
                chestOfferJson.ChestClassification = ChestClassification;
                ShopJsonLoader.SaveChestOfferJsonModel(chestOfferJson);
                ResourcesMasterController.AddAndUpdateResources(RewardType.MoonStones, -300);

                break;

            default:
                break;
        }

        SceneManager.LoadScene("OpeningChests");
    }
}
