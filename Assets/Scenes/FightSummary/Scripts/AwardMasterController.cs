using Assets.Common.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scenes.FightSummary.Scripts
{
	public class AwardMasterController : MonoBehaviour
	{
		public GameObject ShardsAward;
		private AwardController _shardsAwardController => ShardsAward.transform.GetComponent<AwardController>();
		
		public GameObject AwardPrefab;

		public void Start()
		{
			var fightSummaryManager =
				GameObject.Find("FightSummaryGameManager").GetComponent<FightSummaryGameManager>();
			
			foreach (var award in fightSummaryManager.Awards)
			{
				var awardInstance = Instantiate(AwardPrefab, transform);
				awardInstance.GetComponent<AwardController>().Quantity = award.Amount;
				awardInstance.GetComponent<Image>().sprite = AllIcons.GetIcon(award.Type);
			}

			_shardsAwardController.Quantity = fightSummaryManager.ShardsAward.Amount;
		}
	}
}
