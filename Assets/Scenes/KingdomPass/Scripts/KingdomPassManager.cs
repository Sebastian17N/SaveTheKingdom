using Assets.Common.Enums;
using Assets.Common.JsonModel;
using Assets.Common.Managers;
using Assets.Common.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class KingdomPassManager : MonoBehaviour
{
    public GameObject KingdomPassRewarsButtonsPrefab;
    public List<GameObject> KingdomPassAwardsButtonsList = new List<GameObject>();
    public Transform KingdomPassAwardsButtonsPrefabSpawnPoint;
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
            var singlePassObject = kingdomPassAwards.GetComponent<KingdomPassRewardsButtons>();
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
        var directoryInfo = new DirectoryInfo("Assets/Configuration/KingdomPass"); //pobiera wszystkie pliki z folderu o konkretnej œcie¿ce
        var files = directoryInfo.GetFiles("*.json"); //pobiera pliki o rozszerzeniu json

        KingdomPassJsonModel rewards = null;

        foreach (var file in files)
        {
            var rewardsFile = RewardEventManager.LoadKingdomPassRewards(file.FullName);

            if (DateTime.Today >= DateTime.Parse(rewardsFile.EventStartDateTime) && DateTime.Today <= DateTime.Parse(rewardsFile.EventEndDateTime))
            {
                rewards = rewardsFile;
                break;
            }
        }

        foreach (var reward in rewards.KingdomPassRewards)
        {
            var kingdomPassRewardsButton = Instantiate(KingdomPassRewarsButtonsPrefab, KingdomPassAwardsButtonsPrefabSpawnPoint);
            KingdomPassAwardsButtonsList.Add(kingdomPassRewardsButton);
            var singleKingdomPassRewardsButton = kingdomPassRewardsButton.GetComponent<KingdomPassRewardsButtons>();
            singleKingdomPassRewardsButton.ordinalNumberText.text = reward.Level.ToString();
            singleKingdomPassRewardsButton.passPointsRequiredToActivateAward = (reward.Level * 100);
           
            singleKingdomPassRewardsButton.freeAwardAmountText.text = reward.RegularReward.Amount.ToString();
            singleKingdomPassRewardsButton.freeAwardImage.sprite = AllIcons.GetIcon(reward.RegularReward.Type);
            singleKingdomPassRewardsButton.RegularRewardType = reward.RegularReward;
            
            singleKingdomPassRewardsButton.premiumAwardAmountText.text = reward.PremiumReward.Amount.ToString();
            singleKingdomPassRewardsButton.premiumAwardImage.sprite = AllIcons.GetIcon(reward.PremiumReward.Type);
            singleKingdomPassRewardsButton.PremiumRewardType = reward.PremiumReward;
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
