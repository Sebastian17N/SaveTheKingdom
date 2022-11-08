using Assets.Common.JsonModel;
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
			CoinsNumber = PlayerPreferences.Load().Coins;
			GetComponent<TMP_Text>().text = CoinsNumber.ToString();
		}
        
		public  void IncrementCoins(int coinsToAdd)
        {
			CoinsNumber = PlayerPreferences.Load().Coins += coinsToAdd;
			
			GetComponent<TMP_Text>().text = CoinsNumber.ToString();        
		}
		
        public void DecrementCoins(int coinsToRemove)
		{
			IncrementCoins(-coinsToRemove);
		}
	}
}
