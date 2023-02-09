using Assets.Common.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CalendarRewardJsonModel 
{
    public string EventName;
    public string EventStartDateTime;
    public string EventEndDateTime;
    public List<CalendarReward> CalendarRewards;
}
