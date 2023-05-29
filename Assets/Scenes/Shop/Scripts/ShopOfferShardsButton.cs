using Assets.Common.JsonModel;
using Assets.Common.Models;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopOfferShardsButton : MonoBehaviour
{
    public TextMeshProUGUI ShardNameText;
    public GameObject ShardImage;
    public TextMeshProUGUI ShardAmountText;
    public GameObject SoldOutCover;
    public GameObject BuyButton;
    public int OrdinalNumber;
    public bool isOfferSold = false;
    
    private void Update()
    {
        if (isOfferSold == true)
            SoldOutCover.SetActive(true);
        else if (isOfferSold == false)
            SoldOutCover.SetActive(false);
    }
    //TO DO: podepnij wydawanie realnej kasy
    public void BuyShopOfferShard()
    {
        var playerPreferences = PlayerPreferences.Load();

        var shopOfferShardJson = ShopJsonLoader.LoadDailyShardJsonModel();
        shopOfferShardJson.DailyOfferShards[OrdinalNumber].IsOfferSold = true;

        playerPreferences.AddShards = new Shards(shopOfferShardJson.DailyOfferShards[OrdinalNumber].ShardOffer.UnitId, 10);
         

        ShopJsonLoader.SaveDailyShardJsonModel(shopOfferShardJson);
        isOfferSold = true;
    }
}
