using Assets.Common.Enums;
using Assets.Common.JsonModel;
using Assets.Common.Models;
using TMPro;
using UnityEngine;

namespace Assets.Scenes.FightSummary.Scripts
{
	public class CoinCounterText : MonoBehaviour
	{
		public int CoinsNumber;
		void Start()
		{
			ShowCoins();
		}
        
		private void Update()
        {
			ShowCoins();
		}
		
        private void ShowCoins()
		{
			CoinsNumber = PlayerPreferences.Load().Coins.Amount;
			GetComponent<TMP_Text>().text = CoinsNumber.ToString();
		}
        
		public  void IncrementCoins(int coinsToAdd)
        {
			var coins = new Reward()
			{
				Type = RewardType.Coins,
				Amount = coinsToAdd
			};
			PlayerPreferences.Load().AddReward = coins;
			CoinsNumber = PlayerPreferences.Load().Coins.Amount; 
			GetComponent<TMP_Text>().text = CoinsNumber.ToString();        
		}
		
        public void DecrementCoins(int coinsToRemove)
		{
			IncrementCoins(-coinsToRemove);
		}
	}
}
