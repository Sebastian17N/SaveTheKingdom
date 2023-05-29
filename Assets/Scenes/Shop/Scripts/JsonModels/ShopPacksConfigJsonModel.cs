using Assets.Common.JsonModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPacksConfigJsonModel 
{
    public int Id;
    public string BackgroundPath;
    public string Name;
    public int Cost;
    public bool IsPackSold = false;

    public AwardShardJsonModel[] AwardShards;
}
