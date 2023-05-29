using Assets.Common.Enums;
using Assets.Common.JsonModel;
using Assets.Units.Defenses.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.UI.CanvasScaler;

public class ShopManager : MonoBehaviour
{
    public GameObject ShardPack;
    public GameObject OfferDetailsObject;
    public GameObject OfferShardsArea;
    public GameObject OfferShardsButton;
    public GameObject WoodenChestButton;
    public GameObject SilverChestButton;
    public GameObject GoldenChestButton;
    public GameObject MoonStoneButton;
    public GameObject CoinsButton;
    public ShopPacksConfigJsonModel ShardPackJson => ShopJsonLoader.LoadShopPackJsonModel("LaganatShardPacks");
    public List<UnitScriptableObject> RandomUnits = new List<UnitScriptableObject>();
    public DailyOfferShardsJsonModel DailyOfferShardsJsonModel;
    public List<GameObject> OfferShardsButtonsList;

    public GameObject ScriptableObjectManager;

    private void Awake()
    {
        Instantiate(ScriptableObjectManager);
    }

    void Start()
    {
        FillShardPack();
        FillUnitScriptableObjectArray();
        ChangeDailyShardsOffer();
        ManageMoonStoneOffer();
    }
    private void FillShardPack()
    {
        var shardPackObject = ShardPack.GetComponent<ShopPackButton>();
        
        shardPackObject.GetComponent<Image>().sprite = Resources.Load<Sprite>($"PacksBackgrounds/{ShardPackJson.BackgroundPath}");
        shardPackObject.OfferName.text = ShardPackJson.Name;
        shardPackObject.PackCost.text = $"BUY {ShardPackJson.Cost} $";
        shardPackObject.isOfferSold = ShardPackJson.IsPackSold;

        foreach (var singleAwardShards in ShardPackJson.AwardShards)
        {
            var singleOfferDetailsObject = Instantiate(OfferDetailsObject, shardPackObject.OfferDetailsSpawnPoint);

            foreach (var singleUnitScriptableObject in ScriptableObjectManager.GetComponent<ScriptableObjectManager>().UnitsScriptableObjects)
            {
                if (singleUnitScriptableObject.UnitId == singleAwardShards.UnitId)
                {
                    singleOfferDetailsObject.GetComponent<Image>().sprite = singleUnitScriptableObject.Icon;
                    singleOfferDetailsObject.GetComponentInChildren<TextMeshProUGUI>().text = singleAwardShards.MinRange[0].ToString();
                }
            }
        }
    }
    public void BuyPackOffer()
    {
        ShardPack.GetComponent<ShopPackButton>().isOfferSold = true;
        PackOfferIsSold();
    }
    
    private void PackOfferIsSold()
    {
        ShardPackJson.IsPackSold = true;
    }

    //TODO: funkcja mo¿e przyjmowac liste elementów do wylosowania, je¿eli podana liczba jest wiêksza lub równa ca³ej tablicy to j¹ zwróc od razu bez losowania
    private void FillUnitScriptableObjectArray()
    {
        RandomUnits.Clear();

        var random = new System.Random();
        var allElements = ScriptableObjectManager.GetComponent<ScriptableObjectManager>().UnitsScriptableObjects;

        while (RandomUnits.Count <6)
        {
            var index = random.Next(allElements.Count);
            if (RandomUnits.Contains(allElements[index]))
                continue;

            RandomUnits.Add(allElements[index]);
        }
    }
    private void ChangeDailyShardsOffer()
    {
        DailyOfferShardsJsonModel = ShopJsonLoader.LoadDailyShardJsonModel();
        
        var dailyOffer = DateTime.Today.ToString("dd-MM-yyyy");
        var dailyShardManager = DailyOfferShardsJsonModel;

        if (dailyOffer == dailyShardManager.OfferDate)
        {
            FillDailyShardOfferButtons();
        }
        else 
        {
            FillDailyOfferShardsWithUnitScriptableObjectArray();
        }

        dailyShardManager.OfferDate = dailyOffer;
        ShopJsonLoader.SaveDailyShardJsonModel(dailyShardManager);
    }
   
    private void FillDailyOfferShardsWithUnitScriptableObjectArray()
    {
        DailyOfferShardsJsonModel.DailyOfferShards.Clear();
        var dailyOfferShardsJsonModel = DailyOfferShardsJsonModel;

        foreach (var unitScriptableObject in RandomUnits)
        {
            dailyOfferShardsJsonModel.DailyOfferShards.Add(new DailySingleShardJsonModel
            {
                IsOfferSold = false,
                ShardOffer = new AwardShardJsonModel { UnitId = unitScriptableObject.UnitId }
            });
        }

        dailyOfferShardsJsonModel.OfferDate = DateTime.Today.ToString("dd-MM-yyyy");

        ShopJsonLoader.SaveDailyShardJsonModel(dailyOfferShardsJsonModel);

        FillDailyShardOfferButtons();
    }
    private void FillDailyShardOfferButtons()
    {
        var offerShardsAreaObject = OfferShardsArea.GetComponent<ShopOfferShardsArea>();
        var dailyOfferShardsJsonModel = ShopJsonLoader.LoadDailyShardJsonModel();

        int i = 0;

        foreach (var dailyOfferShards in dailyOfferShardsJsonModel.DailyOfferShards)
        {
            var shardButton = Instantiate(OfferShardsButton, offerShardsAreaObject.ShardsButtonsArea);
            var singleShardButton = shardButton.GetComponent<ShopOfferShardsButton>();

            foreach (var singleUnitScriptableObject in ScriptableObjectManager.GetComponent<ScriptableObjectManager>().UnitsScriptableObjects)
            {
                if (singleUnitScriptableObject.UnitId == dailyOfferShards.ShardOffer.UnitId)
                {
                    singleShardButton.ShardNameText.text = singleUnitScriptableObject.Name;
                    singleShardButton.ShardImage.GetComponent<Image>().sprite = singleUnitScriptableObject.Icon;
                    singleShardButton.ShardAmountText.text = $"x {TakeShardAmount(singleUnitScriptableObject.Classification)}";
                    singleShardButton.BuyButton.GetComponentInChildren<TextMeshProUGUI>().text = $"BUY {SetShardCost(singleUnitScriptableObject.Classification)} $";
                    singleShardButton.isOfferSold = dailyOfferShards.IsOfferSold;
                    singleShardButton.OrdinalNumber = i;
                    i++;
                }
            }

            OfferShardsButtonsList.Add(shardButton);
        }
    }
    public void RefreshDailyShardOfferButtons()
    {
        var totalMoonstones = PlayerPreferences.LoadResourceByType("MoonStones");

        if (totalMoonstones < 100)
            return;

        ResourcesMasterController.AddAndUpdateResources(RewardType.MoonStones, -100);

        foreach (var shardsButton in OfferShardsButtonsList)
        {
            Destroy(shardsButton.gameObject);
        }

        OfferShardsButtonsList.Clear();

        FillUnitScriptableObjectArray();
        FillDailyOfferShardsWithUnitScriptableObjectArray();
    }
    private void ManageMoonStoneOffer()
    {
        var offerMoonStoneJsonModel = ShopJsonLoader.LoadOfferMoonStoneJsonModel();
        offerMoonStoneJsonModel.OfferDate = DateTime.Today.ToString("dd-MM-yyyy");

        ShopJsonLoader.SaveOfferMoonStoneJsonModel(offerMoonStoneJsonModel);
    }
    private int TakeShardAmount(UnitClassification classification)
    {
        int amount = 0;

        switch (classification)
        {
            case UnitClassification.Common:
                amount = 100;
                break;
            case UnitClassification.Epic:
                amount = 50;
                break;
            case UnitClassification.Legandary:
                amount = 20;
                break;
            default:
                break;
        }

        return amount;
    }
    private int SetShardCost(UnitClassification classification)
    {
        int amount = 0;

        switch (classification)
        {
            case UnitClassification.Common:
                amount = 20;
                break;
            case UnitClassification.Epic:
                amount = 30;
                break;
            case UnitClassification.Legandary:
                amount = 50;
                break;
            default:
                break;
        }

        return amount;
    }
}
