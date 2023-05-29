using Assets.Common.JsonModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyOfferShardsJsonModel
{
    public string OfferDate;

    public List<DailySingleShardJsonModel> DailyOfferShards = new List<DailySingleShardJsonModel>();
}
