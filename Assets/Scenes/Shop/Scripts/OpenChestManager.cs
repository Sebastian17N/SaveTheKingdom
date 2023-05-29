using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OpenChestManager : MonoBehaviour
{
    public List<ShopOfferChestButton> Chests = new List<ShopOfferChestButton>();
    public ChestClassification ChosenChest;
    public ChestOfferJsonModel ChestOfferJson => ShopJsonLoader.LoadChestOfferJsonModel();
    public TextMeshProUGUI AmounOfPurchasedChest1;
    public TextMeshProUGUI AmounOfPurchasedChest2;
    public TextMeshProUGUI AmounOfPurchasedChest3;

    void Start()
    {
        RefreshChestOffer();
        FillAmounOfPurchased();
    }
    private void RefreshChestOffer()
    {
        var chestOfferJson = ShopJsonLoader.LoadChestOfferJsonModel();
        var dateToday = DateTime.Today.ToString("dd-MM-yyyy");

        if (chestOfferJson.ChestOfferDate != dateToday)
        {
            chestOfferJson.NumberOfUsedWoodenChest = 0;
            chestOfferJson.NumberOfUsedSilverChest = 0;
            chestOfferJson.NumberOfUsedGoldenChest = 0;

            chestOfferJson.ChestOfferDate = dateToday;
        }

        ShopJsonLoader.SaveChestOfferJsonModel(chestOfferJson);
    }
    public void FillAmounOfPurchased()
    {
        AmounOfPurchasedChest1.text = $"{ChestOfferJson.NumberOfUsedWoodenChest} / 5";
        AmounOfPurchasedChest2.text = $"{ChestOfferJson.NumberOfUsedSilverChest} / 5"; 
        AmounOfPurchasedChest3.text = $"{ChestOfferJson.NumberOfUsedGoldenChest} / 5"; 
    }


}
