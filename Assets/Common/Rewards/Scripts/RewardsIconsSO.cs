using Assets.Common.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

[CreateAssetMenu(fileName = "RewardsIcon", menuName = "Scriptable Objects/Rewards Icon")]
public class RewardsIconsSO : ScriptableObject
{
    public Sprite CoinIcon;
    public Sprite SapphireIcon;
    public Sprite TopazIcon;
    public Sprite EmeraldIcon;
    public Sprite MoonStoneIcon;
        
    public Sprite GetIcon(RewardType type)
    {
        switch (type)
        {
            case RewardType.Coins:
                return CoinIcon;
            case RewardType.Sapphires:
                return SapphireIcon;
            case RewardType.Topazes:
                return TopazIcon;
            case RewardType.Emeralds:
                return EmeraldIcon;
            case RewardType.MoonStones:
                return MoonStoneIcon;
            default:
                return null;
        }
    }
}
