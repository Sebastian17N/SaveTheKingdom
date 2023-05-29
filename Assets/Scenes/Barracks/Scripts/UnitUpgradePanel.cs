using Assets.Common.Enums;
using Assets.Common.JsonModel;
using Assets.Scenes.Barracks.Scripts;
using Assets.Scenes.Quests.Scripts;
using Assets.Units.Defenses.Scripts;
using System;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitUpgradePanel : MonoBehaviour
{
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI damageUpgradeText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI healthUpgradeText;
    public TextMeshProUGUI shardsHavedText;
    public TextMeshProUGUI shardsNeededText;
    public TextMeshProUGUI coinsHavedText;
    public TextMeshProUGUI coinsNeededText;
    public TextMeshProUGUI gemsHavedText;
    public TextMeshProUGUI gemsNeededText;
    public TextMeshProUGUI levelText;
    public Image UnitIcon;
    public Image GemIcon;

    public UnitScriptableObject scriptableObject => transform.GetComponentInChildren<UnitDataFolder>().UnitScriptableObject;

    public void LevelUpUnit()
    {
        var totalCoins = PlayerPreferences.LoadResourceByType("Coins");
        var totalGems = PlayerPreferences.LoadResourceByType("Coins");

        if (totalCoins < scriptableObject.UpgradeCoinCost)
            return;

        if ((Int32.Parse(shardsHavedText.text) < Int32.Parse(shardsNeededText.text)))
            return;

        if ((Int32.Parse(gemsHavedText.text) < Int32.Parse(gemsNeededText.text)))
            return;

        damageText.text = (scriptableObject.AttackDamage += scriptableObject.AttackDamageUpgrade).ToString();
        scriptableObject.AttackDamageUpgrade *= 2;
        damageUpgradeText.text = (scriptableObject.AttackDamage + scriptableObject.AttackDamageUpgrade).ToString();
       
        healthText.text = (scriptableObject.Health += scriptableObject.HealthUpgrade).ToString();
        scriptableObject.HealthUpgrade *= 2;
        healthUpgradeText.text = (scriptableObject.Health + scriptableObject.HealthUpgrade).ToString();

        shardsNeededText.text = scriptableObject.ShardCostOfUpgradeBasedOnClassification().ToString();

        //shardsOwnedText.text = scriptableObject.AttackDamage.ToString();
        //shardsNeededText.text = scriptableObject.AttackDamage.ToString();
        //totalCoins - scriptableObject.UpgradeInitialCost;

        ResourcesMasterController.AddAndUpdateResources(RewardType.Coins, -(int)scriptableObject.UpgradeCoinCost);

        FindObjectOfType<BarracksGameManager>().RefreshAllUnitsTexts();
        coinsHavedText.text = totalCoins.ToString();

        PlayerPreferences.LogGatherAchievements(1, QuestType.UnitTrained4);
    }
}
