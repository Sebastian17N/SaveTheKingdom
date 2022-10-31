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
			CoinsNumber = PlayerPrefs.GetInt("coins");
			GetComponent<TMP_Text>().text = CoinsNumber.ToString();
		}
        public  void IncrementCoins(int coinsToAdd)
		{
			CoinsNumber = PlayerPrefs.GetInt("coins") + coinsToAdd;
			PlayerPrefs.SetInt("coins", CoinsNumber);
			GetComponent<TMP_Text>().text = CoinsNumber.ToString();        
		}
		public void DecrementCoins(int coinsToRemove)
		{
			CoinsNumber = PlayerPrefs.GetInt("coins") - coinsToRemove;
			PlayerPrefs.SetInt("coins", CoinsNumber);
			GetComponent<TMP_Text>().text = CoinsNumber.ToString();
		}

	}
}
