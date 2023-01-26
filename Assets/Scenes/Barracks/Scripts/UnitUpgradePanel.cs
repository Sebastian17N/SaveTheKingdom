using Assets.Common.Enums;
using Assets.Common.JsonModel;
using Assets.Scenes.Barracks.Scripts;
using Assets.Units.Defenses.Scripts;
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
    public TextMeshProUGUI shardsOwnedText;
    public TextMeshProUGUI shardsNeededText;
    public TextMeshProUGUI coinsNeededText;
    public TextMeshProUGUI coinsHavedText;
    public Image UnitIcon;

    public RewardType Type;

    public UnitScriptableObject scriptableObject =>
        transform.GetComponentInChildren<UnitDataFolder>().UnitScriptableObject;

    public void LevelUpUnit()
    {
        var totalCoins = PlayerPreferences.LoadResourceByType("Coins");

        if (totalCoins < scriptableObject.UpgradeInitialCost)
            return;

        //if (!(scriptableObject.ShardsNumber >= 10))
        //    return;

        damageText.text = (scriptableObject.AttackDamage += scriptableObject.AttackDamageUpgrade).ToString();
        scriptableObject.AttackDamageUpgrade *= 2;
        damageUpgradeText.text = (scriptableObject.AttackDamage + scriptableObject.AttackDamageUpgrade).ToString();
       
        healthText.text = (scriptableObject.Health += scriptableObject.HealthUpgrade).ToString();
        scriptableObject.HealthUpgrade *= 2;
        healthUpgradeText.text = (scriptableObject.Health + scriptableObject.HealthUpgrade).ToString();
        
        //shardsOwnedText.text = scriptableObject.AttackDamage.ToString();
        //shardsNeededText.text = scriptableObject.AttackDamage.ToString();
        //totalCoins - scriptableObject.UpgradeInitialCost;
        FindObjectOfType<ResourcesMasterController>()._coinsController.DecrementResources((int)scriptableObject.UpgradeInitialCost, "Coins");
        FindObjectOfType<BarracksGameManager>().RefreshAllUnitsTexts();
        coinsHavedText.text = PlayerPreferences.LoadResourceByType("Coins").ToString();
    }
}
