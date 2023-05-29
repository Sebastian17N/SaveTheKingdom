using Assets.Common.Enums;
using UnityEngine;
public class AllIcons 
{
    public static Sprite CoinIcon => Resources.Load<Sprite>("Icons/Coin");
    public static Sprite SapphireIcon => Resources.Load<Sprite>("Icons/Gems_2");
    public static Sprite TopazIcon => Resources.Load<Sprite>("Icons/Gems_12");
    public static Sprite EmeraldIcon => Resources.Load<Sprite>("Icons/Gems_9");
    public static Sprite MoonStoneIcon => Resources.Load<Sprite>("Icons/Gems_13");

    public static Sprite GetIcon(RewardType type)
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
