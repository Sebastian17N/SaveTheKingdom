﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assets.Common.Enums;
using Assets.Common.Managers;
using Assets.Common.Models;
using Assets.Scenes.Quests.Scripts;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace Assets.Common.JsonModel
{
    [Serializable]
    public class PlayerPreferences
    {
        /// <summary>
        /// DO NOT USE IT DIRECTLY
        /// </summary>
        public Reward CoinsJson;
        public Reward Coins
        {
            get => CoinsJson = Load().CoinsJson;
            set
            {
                CoinsJson = value;
                Save(this);
            }
        }

        /// <summary>
        /// DO NOT USE IT DIRECTLY
        /// </summary>
        public Reward EmeraldsJson = new();
        public Reward Emeralds
        {
            get => EmeraldsJson = Load().EmeraldsJson;
            set
            {
                EmeraldsJson = value;
                Save(this);
            }
        }

        /// <summary>
        /// DO NOT USE IT DIRECTLY
        /// </summary>
        public Reward SapphiresJson = new();
        public Reward Sapphires
        {
            get => SapphiresJson = Load().SapphiresJson;
            set
            {
                SapphiresJson = value;
                Save(this);
            }
        }

        /// <summary>
        /// DO NOT USE IT DIRECTLY
        /// </summary>
        public Reward TopazesJson = new();
        public Reward Topazes
        {
            get => TopazesJson = Load().TopazesJson;
            set
            {
                TopazesJson = value;
                Save(this);
            }
        }

        /// <summary>
        /// DO NOT USE IT DIRECTLY
        /// </summary>
        public Reward MoonStoneJson = new();
        public Reward MoonStones
        {
            get => MoonStoneJson = Load().MoonStoneJson;
            set
            {
                MoonStoneJson = value;
                Save(this);
            }
        }

        public Reward AddReward
        {
            set
            {
                var reward = value.Type switch
                {
                    RewardType.Coins => Coins,
                    RewardType.Emeralds => Emeralds,
                    RewardType.Sapphires => Sapphires,
                    RewardType.Topazes => Topazes,
                    RewardType.MoonStones => MoonStones,
                    _ => null
                };

                if (reward == null)
                    return;

                reward.Amount += value.Amount;
                reward.Type = value.Type;

                Save(this);
            }
        }

        /// <summary>
        /// DO NOT USE IT DIRECTLY
        /// </summary>
        public List<Shards> ShardsJson = new();
        public List<Shards> Shards
        {
            get => ShardsJson = Load().ShardsJson;
            set
            {
                ShardsJson = value;
                Save(this);
            }
        }

        public Shards AddShards
        {
            set
            {
                if (Shards.Any(shard => shard.ShardId == value.ShardId))
                    Shards.Single(shard => shard.ShardId == value.ShardId).Amount += value.Amount;
                else
                {
                    var shardsCopy = Shards.ToList();
                    shardsCopy.Add(new Shards(value.ShardId, value.Amount));
                    Shards = shardsCopy.ToList();
                }

                Save(this);
            }
        }

        public List<PlayerAchievement> PlayersAchievements = new();

        private static readonly string fileName = "Assets/Configuration/PlayerPreferences/PlayerPreferences.json";

        public void RefreshOneDateQuest()
        {
            foreach (var achievement in PlayersAchievements.Where(achievement => achievement.OneDayQuest))
            {
                if (achievement.ActivityDate == DateTime.Today.ToString("dd-MM-yyyy"))
                {
                    return;
                }

                achievement.AmountGathered = 0;
                achievement.ActivityDate = DateTime.Today.ToString("dd-MM-yyyy");

                var directoryInfo = new DirectoryInfo($"Assets/Scenes/Quests/Data/Daily");
                var files = directoryInfo.GetFiles("*.json");

                foreach (var file in files)
                {
                    var fileData = File.ReadAllText(file.FullName);
                    var quest = JsonUtility.FromJson<Quest>(fileData);
                    quest.RewardState = RewardState.Inactive;
                    QuestsManager.SaveQuest(quest);
                }
            }

            Save(this);
        }

        public static PlayerPreferences Load()
        {
            if (!File.Exists(fileName))
                return null;

            var fileData = File.ReadAllText(fileName);
            var prefs = JsonUtility.FromJson<PlayerPreferences>(fileData) ?? new PlayerPreferences();
            return prefs;
        }

        private static void Save(PlayerPreferences playerPreferences)
        {
            File.WriteAllText(fileName, JsonUtility.ToJson(playerPreferences));
        }

        public static int LoadResourceByType(string type)
        {
            Enum.TryParse(type, out RewardType converted);
            return LoadResourceByType(converted);
        }

        public static int LoadResourceByType(RewardType returnType)
        {
            switch (returnType)
            {
                case RewardType.Coins:
                    return Load().Coins.Amount;
                case RewardType.Emeralds:
                    return Load().Emeralds.Amount;
                case RewardType.Sapphires:
                    return Load().Sapphires.Amount;
                case RewardType.Topazes:
                    return Load().Topazes.Amount;
                case RewardType.MoonStones:
                    return Load().MoonStones.Amount;
            }

            return 0;
        }

        /// <summary>
        /// Method log gather amount of achievement (for example damage dealt during one battle)
        /// </summary>
        /// <param name="amountGather">Damage dealt.</param>
        public static void LogGatherAchievements(float amountGather, QuestType questType) // 
        {
            var playerPreferences = Load();
            var achievements = playerPreferences.PlayersAchievements.Where(achievement => achievement.QuestType == questType);

            playerPreferences.RefreshOneDateQuest();

            var oneDayAchievement = achievements.SingleOrDefault(achievement => achievement.OneDayQuest);
            var permanentAchievement = achievements.SingleOrDefault(achievement => !achievement.OneDayQuest);

            if (oneDayAchievement != null)
            {
                oneDayAchievement.AmountGathered += amountGather;
                oneDayAchievement.OneDayQuest = true;
                oneDayAchievement.QuestType = questType;
                oneDayAchievement.ActivityDate = DateTime.Today.ToString("dd-MM-yyyy");
            }
            else if (permanentAchievement != null)
            {
                permanentAchievement.AmountGathered += amountGather;
                permanentAchievement.OneDayQuest = false;
                permanentAchievement.QuestType = questType;
                permanentAchievement.ActivityDate = DateTime.Today.ToString("dd-MM-yyyy");
            }

            Save(playerPreferences);
        }

        public static void UpdateStatusOfAllDailyQuestHaveBenTaken()
        {
            var playerPreferences = Load();
            playerPreferences.RefreshOneDateQuest();

            var dailyQuestList = LoadDailyQuestList();

            bool allQuestPassed = true; //jeśli true to wszyskie questy są ready

            foreach (var quest in dailyQuestList.Where(quest => !quest.Description.Equals("End all daily quest")))
            {
                if (quest.RewardState != RewardState.Taken)
                {
                    allQuestPassed = false;

                    break;
                }
                else
                {
                    allQuestPassed = true;
                }
            }

            if (allQuestPassed)
            {
                var allQuestDone = dailyQuestList.SingleOrDefault(quest => !quest.Description.Equals("End all daily quest"));
                allQuestDone.RewardState = RewardState.Active;
                QuestsManager.SaveQuest(allQuestDone);
            }

            Save(playerPreferences);
        }

        protected static List<Quest> LoadDailyQuestList()
        {
            var directoryInfo = new DirectoryInfo($"Assets/Scenes/Quests/Data/Daily");
            var files = directoryInfo.GetFiles("*.json");

            var questList = new List<Quest>();

            foreach (var file in files)
            {
                var fileData = File.ReadAllText(file.FullName);

                questList.Add(JsonUtility.FromJson<Quest>(fileData));
            }

            return questList;
        }
    }
}