using Assets.Common.Enums;
using Assets.Common.JsonModel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopOfferMoonStoneButton : MonoBehaviour
{
    public GameObject SoldOutCover;
    public int OrdinalNumber;
    public bool isOfferSold;
    private MoonStoneOfferJsonModel MoonStoneOfferJsonModel;
    
    private void Awake()
    {
        MoonStoneOfferJsonModel = ShopJsonLoader.LoadOfferMoonStoneJsonModel();
        RefreshMoonStoneOffer();
    }
    void Update()
    {
        if (isOfferSold == true)
            SoldOutCover.SetActive(true);
        else if (isOfferSold == false)
            SoldOutCover.SetActive(false);
    }
    private void RefreshMoonStoneOffer()
    {
        for (int i = 0; i < MoonStoneOfferJsonModel.MoonStoneOfferList.Count; i++)
        {
            if (MoonStoneOfferJsonModel.OfferDate != DateTime.Today.ToString("dd-MM-yyyy"))
            {
                if (MoonStoneOfferJsonModel.MoonStoneOfferList[i].OrdinalNumber == OrdinalNumber)
                {
                    MoonStoneOfferJsonModel.MoonStoneOfferList[i].isOfferSold = false;
                    isOfferSold = false;
                }
            }
            else
            {
                if (MoonStoneOfferJsonModel.MoonStoneOfferList[i].OrdinalNumber == OrdinalNumber)
                {
                    isOfferSold = MoonStoneOfferJsonModel.MoonStoneOfferList[i].isOfferSold;
                }
            }
        }
        ShopJsonLoader.SaveOfferMoonStoneJsonModel(MoonStoneOfferJsonModel);
    }
    public void BuyShopOfferMoonStone()
    {
        MoonStoneOfferJsonModel = ShopJsonLoader.LoadOfferMoonStoneJsonModel();
        for (int i = 0; i < MoonStoneOfferJsonModel.MoonStoneOfferList.Count; i++)
        {
            if (MoonStoneOfferJsonModel.MoonStoneOfferList[i].OrdinalNumber == OrdinalNumber)
            {
                MoonStoneOfferJsonModel.MoonStoneOfferList[i].isOfferSold = true;
                ResourcesMasterController.AddAndUpdateResources(RewardType.MoonStones, MoonStoneOfferJsonModel.MoonStoneOfferList[i].OrdinalNumber);
            }
        }

        ShopJsonLoader.SaveOfferMoonStoneJsonModel(MoonStoneOfferJsonModel);
        
        isOfferSold = true;
    }
}
