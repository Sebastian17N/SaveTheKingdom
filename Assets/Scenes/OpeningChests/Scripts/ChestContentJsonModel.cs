using Assets.Common.JsonModel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ChestContentJsonModel
{
    public int Level;
    public AwardShardJsonModel[] AwardShards;
    //public AwardJsonModel[] Awards;
}
