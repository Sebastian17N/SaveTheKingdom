﻿using UnityEngine;

namespace Assets.Scenes.FightSummary.Scripts
{
	public class AwardMasterController : MonoBehaviour
	{
		public GameObject CoinsAward;
		private AwardController _coinsAwardController => CoinsAward.transform.GetComponent<AwardController>();

		public GameObject GemsAward;
		private AwardController _gemsAwardController => GemsAward.transform.GetComponent<AwardController>();

		public GameObject ShardsAward;
		private AwardController _shardsAwardController => ShardsAward.transform.GetComponent<AwardController>();

		public void Start()
		{
			var fightSummaryManager =
				GameObject.Find("FightSummaryGameManager").GetComponent<FightSummaryGameManager>();

			_coinsAwardController.Quantity = fightSummaryManager.CoinsAward;
			_gemsAwardController.Quantity = fightSummaryManager.GemsAward.Count;
			_shardsAwardController.Quantity = fightSummaryManager.ShardsAward.quantity;
		}
	}
}
