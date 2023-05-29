using Assets.Common.JsonModel;
using Assets.Common.Managers;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ShopJsonLoader
{
    public static ShopPacksConfigJsonModel LoadShopPackJsonModel(string packName)
    {
        if (!File.Exists($"Assets/Scenes/Shop/Configuration/{packName}.json"))
            return null;

        var fileData = File.ReadAllText($"Assets/Scenes/Shop/Configuration/{packName}.json");

        return JsonUtility.FromJson<ShopPacksConfigJsonModel>(fileData);
    }
    public static void SaveShopPackJsonModel(ShopPacksConfigJsonModel shopPacks, string packName)
    {
        File.WriteAllText($"Assets/Scenes/Shop/Configuration/{packName}.json", JsonUtility.ToJson(shopPacks));
    }
    public static DailyOfferShardsJsonModel LoadDailyShardJsonModel()
    {
        if (!File.Exists($"Assets/Scenes/Shop/Configuration/DailyOfferShards.json"))
            return null;

        var fileData = File.ReadAllText($"Assets/Scenes/Shop/Configuration/DailyOfferShards.json");

        return JsonUtility.FromJson<DailyOfferShardsJsonModel>(fileData);
    }
    public static void SaveDailyShardJsonModel(DailyOfferShardsJsonModel dailyOffer)
    {
        File.WriteAllText($"Assets/Scenes/Shop/Configuration/DailyOfferShards.json", JsonUtility.ToJson(dailyOffer));
    }
    public static ChestOfferJsonModel LoadChestOfferJsonModel()
    {
        if (!File.Exists($"Assets/Scenes/Shop/Configuration/ChestOffer.json"))
            return null;

        var fileData = File.ReadAllText($"Assets/Scenes/Shop/Configuration/ChestOffer.json");

        return JsonUtility.FromJson<ChestOfferJsonModel>(fileData);
    }
    public static void SaveChestOfferJsonModel(ChestOfferJsonModel chestDailyOffer)
    {
        File.WriteAllText($"Assets/Scenes/Shop/Configuration/ChestOffer.json", JsonUtility.ToJson(chestDailyOffer));
    }
    public static MoonStoneOfferJsonModel LoadOfferMoonStoneJsonModel()
    {
        if (!File.Exists($"Assets/Scenes/Shop/Configuration/MoonStoneOffer.json"))
            return null;

        var fileData = File.ReadAllText($"Assets/Scenes/Shop/Configuration/MoonStoneOffer.json");

        return JsonUtility.FromJson<MoonStoneOfferJsonModel>(fileData);
    }
    public static void SaveOfferMoonStoneJsonModel(MoonStoneOfferJsonModel moonStoneOffer)
    {
        File.WriteAllText($"Assets/Scenes/Shop/Configuration/MoonStoneOffer.json", JsonUtility.ToJson(moonStoneOffer));
    }
}
