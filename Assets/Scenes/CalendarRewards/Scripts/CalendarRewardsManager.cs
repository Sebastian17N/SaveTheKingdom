using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Common.Enums;
using Assets.Common.JsonModel;
using Assets.Common.Managers;
using Assets.Common.Models;
using UnityEngine;

namespace Assets.Scenes.CalendarRewards.Scripts
{
	public class CalendarRewardsManager : MonoBehaviour
	{
		public GameObject RewardPrefab;
		public Transform RewardPrefabSpawnPoint;

		private readonly List<GameObject> _calendarRewardList = new();
		public RewardsIconsSO RewardsIconSO;

		#region Events Parameters
		private readonly DateTime _startEventDate = new(2022, 12, 07);
		private readonly List<GameObject> _eventRewardList = new();
		#endregion

		void Start()
		{
			SpawnCalendarReward();
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

			var presentDay = (int)DateTime.Now.Day;
			var firstDayOfEvent = _startEventDate;
        
			for (int i = 0; i < _eventRewardList.Count; i++)
			{
				var singleEventReward = _eventRewardList[i].GetComponent<CalendarRewardButton>();
            
				if (i > 0 && _eventRewardList[i - 1].GetComponent<CalendarRewardButton>().RewardType.State == RewardState.Taken &&
				    singleEventReward.RewardType.State == RewardState.Inactive)
				{
					singleEventReward.RewardType.State = RewardState.Active;
					singleEventReward.AwardActivated();
				}
				else if (singleEventReward.RewardType.State == RewardState.Inactive)
				{
					// singleEventReward.RewardType.State = RewardState.Inactive;
					singleEventReward.AwardActivated();
				}
				else if (singleEventReward.RewardType.State == RewardState.Taken)
				{
					singleEventReward.RewardType.State = RewardState.Taken;
					singleEventReward.AwardTaked();
				}
			}

			foreach(var eventReward in _eventRewardList.Skip(0)) { 
			}
		}

		public void SpawnCalendarReward()
		{
			SpawnReward(_calendarRewardList, _eventRewardList,
				"Assets/Configuration/CallendarAwardPP.json");
			;
		}

		public void SpawnEventReward()
		{
			SpawnReward(_eventRewardList, _calendarRewardList,
				"Assets/Configuration/CalendarRewardsEvents/ArchontEventAwards.json");
		}

		private void SpawnReward(ICollection<GameObject> listToFill, ICollection<GameObject> listToEmpty, string fileName)
		{
			if (listToFill.Count != 0)
				return;

			foreach (var calendarReward in listToEmpty)
			{
				Destroy(calendarReward);
			}
			listToEmpty.Clear();

			var rewards = RewardEventManager.LoadCalendarRewards(fileName);

			foreach (var reward in rewards)
			{
				var spawnEventReward = Instantiate(RewardPrefab, RewardPrefabSpawnPoint);
				listToFill.Add(spawnEventReward);

				var singleSpawnEventReward = spawnEventReward.GetComponent<CalendarRewardButton>();
				singleSpawnEventReward.Id = reward.Day;
				singleSpawnEventReward.dayNumberText.text = $"Day {reward.Day}";
				singleSpawnEventReward.awardAmountText.text = reward.Amount.ToString();
				singleSpawnEventReward.awardImage.sprite = RewardsIconSO.GetIcon(reward.Type);
				singleSpawnEventReward.RewardType.Type = reward.Type;
				singleSpawnEventReward.RewardType.Amount = reward.Amount;
				singleSpawnEventReward.RewardType = reward;
				singleSpawnEventReward.RewardType.ReceivingDate = reward.ReceivingDate;
			}
		}

		public void TakeAward()
		{
			string fileName = string.Empty;
			CalendarReward rewardTaken = null;

			if (_calendarRewardList.Count > 0)
			{
				foreach (var eventReward in
				         _calendarRewardList
					         .Select(reward =>
						         reward.GetComponent<CalendarRewardButton>().RewardType)
					         .Where(reward => reward.State == RewardState.Active))
				{

					PlayerPreferences.Load().AddReward = eventReward;
					eventReward.State = RewardState.Taken;
					rewardTaken = eventReward;
					fileName = "Assets/Configuration/CallendarAwardPP.json";
					break;
				}
			}
			else if(_eventRewardList.Count > 0)
			{
				foreach (var eventReward in 
				         _eventRewardList
					         .Select(reward => 
						         reward.GetComponent<CalendarRewardButton>().RewardType)
					         .Where(reward => reward.State == RewardState.Active))

				{
					CalendarReward lastTakenReward = LastTakenReward();

					if (lastTakenReward.ReceivingDate.Equals(DateTime.Today.ToString("dd-MM-yyyy")))
						break;

					eventReward.State = RewardState.Taken;
					eventReward.ReceivingDate = DateTime.Today.ToString("dd-MM-yyyy");

					PlayerPreferences.Load().AddReward = eventReward;
					rewardTaken = eventReward;
					fileName = "Assets/Configuration/CalendarRewardsEvents/ArchontEventAwards.json";
					break;
				}
			}

			if (string.IsNullOrEmpty(fileName)) 
				return;

			var manager = RewardEventManager.LoadCalendarRewardsManager(fileName);
			var eventRewardFromFile = manager.Rewards.SingleOrDefault(reward => reward.Day == rewardTaken.Day);
			eventRewardFromFile.State = RewardState.Taken;
			RewardEventManager.Save(fileName, manager);
		}
		private CalendarReward LastTakenReward()
		{
			return _eventRewardList.Select(reward => reward.GetComponent<CalendarRewardButton>().RewardType)
									.Where(reward => reward.State == RewardState.Taken)
									.OrderBy(reward => reward.Day).Last();
		}
	}
}
