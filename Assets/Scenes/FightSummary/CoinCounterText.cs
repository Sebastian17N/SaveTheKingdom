using TMPro;
using UnityEngine;

namespace Assets.Scenes.FightSummary
{
	public class CoinCounterText : MonoBehaviour
	{
		public int CoinsNumber;
		void Start()
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
    
	}
}
