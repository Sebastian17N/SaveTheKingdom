using Assets.Common.JsonModel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DailySingleShardJsonModel
{
    public AwardShardJsonModel ShardOffer;
    public bool IsOfferSold = false;
}
