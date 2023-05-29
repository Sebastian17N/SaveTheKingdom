using Assets.Common;
using Assets.Common.JsonModel;
using Assets.Common.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChestContent : MonoBehaviour
{
    public GameObject ScriptableObjectManager;
    public bool REWARD;
    public List<Shards> ShardsRewardList = new List<Shards>();
    public List<Shards> AllShardsList;
    private ChestContentJsonModel chestContentJsonModel;
    void Start()
    {
        chestContentJsonModel = JsonLoader.LoadChestContentConfig();
        AllShardsList = PlayerPreferences.Load().Shards;
        GetRandomWalue();
        HowManyRewards();
    }
        
    private void HowManyRewards()
    {
        switch (GetRandomWalue())
        {
            case int n when (n >= 1 && n <= 80):
                ShardsRewardList.Add(GenerateShardReward(2, 5));
                REWARD = false;
                break;

            case int n when (n >= 81 && n <= 100):
                for (int i = 1; i < 2; i++)
                {
                ShardsRewardList.Add(GenerateShardReward(2, 5));
                }
                REWARD = true;
                break;
            default:
                break;
        }
    }
    private int GetRandomWalue()
    {
        var random = new System.Random();
        int randomValue = random.Next(1, 101);
        return randomValue;
    }
    private Shards GenerateShardReward(int shardId, int shardAmount)
    {
        var shard = new Shards(shardId, shardAmount);

        return shard;
    }
    //private Shards GetRandomShardFromShardsList()
    //{
    //    var unitsList = ScriptableObjectManager.GetComponent<ScriptableObjectManager>().UnitsScriptableObjects.
    //        Where(unit => unit.Classification == UnitClassification.Common).ToList();

    //    var random = new System.Random();
    //    var randomValue = random.Next(unitsList.Count());
    //    var randomShard = unitsList[randomValue];

    //    foreach (var item in collection)
    //    {

    //    }

    //    return randomShard;
    //}
}
