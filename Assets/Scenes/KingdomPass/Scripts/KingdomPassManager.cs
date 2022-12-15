using Assets.Common.Enums;
using Assets.Common.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingdomPassManager : MonoBehaviour
{
    public GameObject KingdomPassAwardsButtonsPrefab;
    public List<GameObject> KingdomPassAwardsButtonsList = new List<GameObject>();
    public Transform KingdomPassAwardsButtonsPrefabSpawnPoint;
    public RewardsIconsSO RewardsIconSO;
    public bool isKingdomPassActivated = false;
    public int passPoints = 0;
    private void Awake()
    {
        SpawnKingdomPassAwardsButtons();
    }
    void Update()
    {
        WorkOnKingdomPassAwardsButtons();

    }
    public void WorkOnKingdomPassAwardsButtons()
    {
        foreach (var kingdomPassAwards in KingdomPassAwardsButtonsList)
        {
            var singlePassObject = kingdomPassAwards.GetComponent<KingdomPassAwardsButtons>();
            singlePassObject.AwardActive();
            singlePassObject.KingdomPassActivated();

            if (singlePassObject.RegularRewardType.State == RewardState.Taken)
            {
                singlePassObject.FreeAwardTaked();
            }

            if (singlePassObject.PremiumRewardType.State == RewardState.Taken)
            {
                singlePassObject.PremiumAwardTaked();
            }

            if (passPoints >= singlePassObject.passPointsRequiredToActivateAward &&
                singlePassObject.RegularRewardType.State != RewardState.Taken &&
                singlePassObject.PremiumRewardType.State != RewardState.Taken)
            {
                singlePassObject.ActivateAward();
            }
        }
    }
   
    public void SpawnKingdomPassAwardsButtons()
    {
        var fileName = "Assets/Configuration/KingdomPass/KingdomPassReward.json";
        var rewards = RewardEventManager.LoadKingdomPassRewards(fileName);

        foreach (var reward in rewards)
        {
            var kingdomPassAwardsButton = Instantiate(KingdomPassAwardsButtonsPrefab, KingdomPassAwardsButtonsPrefabSpawnPoint);
            KingdomPassAwardsButtonsList.Add(kingdomPassAwardsButton);
            var singleKingdomPassAwardsButton = kingdomPassAwardsButton.GetComponent<KingdomPassAwardsButtons>();
            singleKingdomPassAwardsButton.ordinalNumberText.text = reward.Level.ToString();
            singleKingdomPassAwardsButton.passPointsRequiredToActivateAward = (reward.Level * 100);
           
            singleKingdomPassAwardsButton.freeAwardAmountText.text = reward.RegularReward.Amount.ToString();
            singleKingdomPassAwardsButton.freeAwardImage.sprite = RewardsIconSO.GetIcon(reward.RegularReward.Type);
            singleKingdomPassAwardsButton.RegularRewardType.Type = reward.RegularReward.Type;
            singleKingdomPassAwardsButton.RegularRewardType.Amount = reward.RegularReward.Amount;
            singleKingdomPassAwardsButton.RegularRewardType.State = reward.RegularReward.State;
            
            singleKingdomPassAwardsButton.premiumAwardAmountText.text = reward.PremiumReward.Amount.ToString();
            singleKingdomPassAwardsButton.premiumAwardImage.sprite = RewardsIconSO.GetIcon(reward.PremiumReward.Type);
            singleKingdomPassAwardsButton.PremiumRewardType.Type = reward.PremiumReward.Type;
            singleKingdomPassAwardsButton.PremiumRewardType.Amount = reward.PremiumReward.Amount;
            singleKingdomPassAwardsButton.PremiumRewardType.State = reward.PremiumReward.State;

        }
    }
    public void ActivateKingdomPass()
    {
        isKingdomPassActivated = true;
    }
    public void IncrementPassPoints()
    {

    }
}
