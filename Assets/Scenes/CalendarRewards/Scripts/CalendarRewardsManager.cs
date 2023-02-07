using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Assets.Common.Enums;
using Assets.Common.JsonModel;
using Assets.Common.Managers;
using Assets.Common.Models;
using Assets.Scenes.Quests.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scenes.CalendarRewards.Scripts
{
	public class CalendarRewardsManager : MonoBehaviour
	{
		public GameObject RewardPrefab;
		public Transform RewardPrefabSpawnPoint;
		public TextMeshProUGUI EventName;
		public GameObject EventButton1;
		public GameObject EventButton2;
		public GameObject EventButton3;
		#region Events Parameters
		private readonly DateTime _startEventDate;
		private readonly DateTime _endEventDate;
		private readonly List<GameObject> _calendarRewardList = new();
		private readonly List<GameObject> _eventRewardList = new();
		#endregion

		void Start()
		{
			SpawnCalendarReward();
			FillButtonsNames();
        }

		void Update()
		{
			WorkOnCalendarReward();
			WorkOnEventReward();
		}

		public void WorkOnCalendarReward()
		{
			var presentDay = (int)DateTime.Now.Day;

			if (_calendarRewardList.Count == 0)
				return;

			foreach (var calendarReward in _calendarRewardList)
			{
				var singleCalendarReward = calendarReward.GetComponent<CalendarRewardButton>();
            
				if ((presentDay == singleCalendarReward.Id) && (singleCalendarReward.RewardType.State != RewardState.Taken))
				{
					singleCalendarReward.RewardType.State = RewardState.Active;
					singleCalendarReward.AwardActivated();
				}
				else if ((presentDay != singleCalendarReward.Id) && (presentDay < singleCalendarReward.Id))
				{
					singleCalendarReward.RewardType.State = RewardState.Inactive;
					singleCalendarReward.AwardActivated();
				}
				else if((presentDay != singleCalendarReward.Id) && (presentDay > singleCalendarReward.Id) && 
				        (singleCalendarReward.RewardType.State != RewardState.Taken))
				{
					singleCalendarReward.RewardType.State = RewardState.Loosed;
					singleCalendarReward.AwardLoosed();
				}
				else
				{
					singleCalendarReward.AwardTaked();
				}
			}
		}

		public void WorkOnEventReward()
		{
			if (_eventRewardList.Count == 0)
				return;

			for (int i = 0; i < _eventRewardList.Count; i++)
			{
				var singleEventReward = _eventRewardList[i].GetComponent<CalendarRewardButton>();

				if (i == 0 && singleEventReward.RewardType.State != RewardState.Taken)
				{
					singleEventReward.RewardType.State = RewardState.Active;
					singleEventReward.AwardActivated();
				}
				else if(i > 0 && _eventRewardList[i - 1].GetComponent<CalendarRewardButton>().RewardType.State == RewardState.Taken &&
					_eventRewardList[i - 1].GetComponent<CalendarRewardButton>().RewardType.ReceivingDate != DateTime.Today.ToString("dd-MM-yyyy") &&
					singleEventReward.RewardType.State == RewardState.Inactive)
				{
					singleEventReward.RewardType.State = RewardState.Active;
					singleEventReward.AwardActivated();
				}
				else if (singleEventReward.RewardType.State == RewardState.Inactive)
				{
					singleEventReward.AwardActivated();
				}
				else if (singleEventReward.RewardType.State == RewardState.Taken)
				{
					singleEventReward.RewardType.State = RewardState.Taken;
					singleEventReward.AwardTaked();
				}
			}
		}

		public void SpawnCalendarReward()
		{
			var filePath = "Assets/Configuration/CallendarReward";
            
			SpawnReward(_calendarRewardList, _eventRewardList, filePath);
		}

		public void SpawnEventReward()
		{
			var filePath = "Assets/Configuration/CalendarRewardsEvents";
			
			SpawnReward(_eventRewardList, _calendarRewardList, filePath);
		}

		private void SpawnReward(ICollection<GameObject> listToFill, ICollection<GameObject> listToEmpty, string folderName)
		{
            var directoryInfo = new DirectoryInfo(folderName); //pobiera wszystkie pliki z folderu o konkretnej œcie¿ce
            var files = directoryInfo.GetFiles("*.json"); //pobiera pliki o rozszerzeniu json

			CalendarRewardJsonModel rewards = null ;

			foreach (var file in files)
			{
                var rewardsFile = RewardEventManager.LoadCalendarRewards(file.FullName);

				if (DateTime.Today >= DateTime.Parse(rewardsFile.EventStartDateTime) && DateTime.Today <= DateTime.Parse(rewardsFile.EventEndDateTime))
				{
					rewards = rewardsFile;
					break;
                }
            }

			if (rewards == null)
				return;


            if (listToFill.Count != 0)
				return;

			foreach (var calendarReward in listToEmpty)
			{
				Destroy(calendarReward);
			}
			listToEmpty.Clear();

			foreach (var reward in rewards.CalendarRewards)
			{
				var spawnEventReward = Instantiate(RewardPrefab, RewardPrefabSpawnPoint);
				listToFill.Add(spawnEventReward);

				var singleSpawnEventReward = spawnEventReward.GetComponent<CalendarRewardButton>();
				singleSpawnEventReward.Id = reward.Day;
				singleSpawnEventReward.dayNumberText.text = $"Day {reward.Day}";
				singleSpawnEventReward.awardAmountText.text = reward.Amount.ToString();
				singleSpawnEventReward.awardImage.sprite = AllIcons.GetIcon(reward.Type); 
                singleSpawnEventReward.RewardType.Type = reward.Type;
				singleSpawnEventReward.RewardType.Amount = reward.Amount;
				singleSpawnEventReward.RewardType = reward;
				singleSpawnEventReward.RewardType.ReceivingDate = reward.ReceivingDate;
			}

            EventName.text = rewards.EventName;
        }

		public void TakeAward()
		{
			string fileName = string.Empty;
			CalendarReward rewardTaken = null;
			CalendarReward lastTakenReward;

			if (_calendarRewardList.Count > 0)
			{
				foreach (var eventReward in
				         _calendarRewardList
					         .Select(reward => reward.GetComponent<CalendarRewardButton>().RewardType)
					         .Where(reward => reward.State == RewardState.Active))
				{
					ResourcesMasterController.AddAndUpdateResources(eventReward.Type, eventReward.Amount);

                    eventReward.State = RewardState.Taken;
					rewardTaken = eventReward;
					fileName = "Assets/Configuration/CallendarReward/CallendarReward.json";
					break;
				}
			}
			else if(_eventRewardList.Count > 0)
			{
				foreach (var eventReward in 
				         _eventRewardList
					         .Select(reward => reward.GetComponent<CalendarRewardButton>().RewardType)
					         .Where(reward => reward.State == RewardState.Active))

				{
					if ((lastTakenReward = LastTakenReward()) != null &&
						lastTakenReward.ReceivingDate.Equals(DateTime.Today.ToString("dd-MM-yyyy")))
							break;

					eventReward.State = RewardState.Taken;
					eventReward.ReceivingDate = DateTime.Today.ToString("dd-MM-yyyy");

                    ResourcesMasterController.AddAndUpdateResources(eventReward.Type, eventReward.Amount);
                    rewardTaken = eventReward;
					fileName = "Assets/Configuration/CalendarRewardsEvents/ArchontEventAwards.json";
					break;
				}
			}

			if (string.IsNullOrEmpty(fileName)) 
				return;

			var manager = RewardEventManager.LoadCalendarRewards(fileName);
			var eventRewardFromFile = manager.CalendarRewards.SingleOrDefault(reward => reward.Day == rewardTaken.Day);
			eventRewardFromFile.State = RewardState.Taken;
			eventRewardFromFile.ReceivingDate = rewardTaken.ReceivingDate;
			RewardEventManager.SaveCalendarRewards(fileName, manager);
		}
		private CalendarReward LastTakenReward()
		{
			var lastReward = _eventRewardList.Select(reward => reward.GetComponent<CalendarRewardButton>().RewardType)
									.Where(reward => reward.State == RewardState.Taken)
									.OrderBy(reward => reward.Day).LastOrDefault();

			return lastReward;
		}

		private void FillButtonsNames()
		{
			var event1 = RewardEventManager.LoadCalendarRewards("Assets/Configuration/CallendarReward/CallendarReward.json");
			var event2 = RewardEventManager.LoadCalendarRewards("Assets/Configuration/CalendarRewardsEvents/ArchontEventAwards.json");
			//var event3 = RewardEventManager.LoadCalendarRewards("Assets/Configuration/CallendarReward/CallendarReward.json");

			EventButton1.GetComponentInChildren<TextMeshProUGUI>().text = event1.EventName;
			EventButton2.GetComponentInChildren<TextMeshProUGUI>().text = event2.EventName;

        }
    }
}
