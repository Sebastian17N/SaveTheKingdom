using TMPro;
using UnityEngine;

namespace Assets.Scenes.FightSummary.Scripts
{
	public class Award : MonoBehaviour
	{
		public Animator CoinsAnimator;
		public Animator GemsAnimator;
		public Animator ShardsAnimator;
		public float ShowingSpeed;
		public float GettingAwardsSpeed;
		public int Coins = 0;

		private void Start()
		{
			CoinsAnimator = transform.Find("CoinsAward").GetComponent<Animator>();
			GemsAnimator = transform.Find("GemsAward").GetComponent<Animator>();
			ShardsAnimator = transform.Find("ShardsAward").GetComponent<Animator>();
			CoinsAnimator.enabled = false;
			GemsAnimator.enabled = false;
			ShardsAnimator.enabled = false;

			Coins = GameObject.Find("FightSummaryGameManager").
				GetComponent<FightSummaryGameManager>().CoinsAward;
		}

		void Update()
		{
			ShowAwards();
		}

		void ShowAwards()
		{
			var awardsSlot = GameObject.Find("AwardsSlot");

			if (transform.parent == awardsSlot.transform)
				return;

			if (Mathf.Abs(awardsSlot.transform.position.y - transform.Find("CoinsAward").position.y) < 10f)
			{
				transform.Find("CoinsAward").GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				transform.Find("CoinsAward").SetParent(awardsSlot.transform);

				ShowCoinsAmount();
				return;
			}

			var destination = awardsSlot.transform.position - transform.Find("CoinsAward").position;
			transform.Find("CoinsAward").GetComponent<Rigidbody2D>().velocity = destination.normalized * Vector2.up * ShowingSpeed;
		}

		public void ShowCoinsAmount()
		{
			transform.Find("CoinsAward").GetComponentInChildren<TMP_Text>().text = Coins.ToString();
			FindObjectOfType<CoinCounterText>().IncrementCoins(Coins);
		}

		public void GettingAwards()
		{
			var coinCounter = GameObject.Find("CoinCounter");

			if (transform.parent == coinCounter.transform)
				return;

			var destination = coinCounter.transform.position - transform.Find("CoinsAward").position;
			transform.Find("CoinsAward").GetComponent<Rigidbody2D>().velocity = destination.normalized * GettingAwardsSpeed;

			CoinsAnimator.enabled = true;
			GemsAnimator.enabled = true;
			ShardsAnimator.enabled = true;

			Destroy(gameObject, 2);
		}
		
		public void DestroyAward()
		{
			Destroy(gameObject);
		}

		public void ActivateButtons()
		{
			FindObjectOfType<FightSummaryGameManager>().ActivateButton();
		}
	}
}