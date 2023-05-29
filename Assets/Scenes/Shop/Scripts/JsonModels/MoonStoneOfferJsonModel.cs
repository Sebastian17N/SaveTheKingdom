using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MoonStoneOfferJsonModel
{
    public string OfferDate;
    public List<MoonStoneSingleJsonModel> MoonStoneOfferList = new List<MoonStoneSingleJsonModel>();
}
