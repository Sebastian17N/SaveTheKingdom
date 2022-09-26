using TMPro;
using UnityEngine;

namespace Assets.Scenes.FightSummary.Scripts
{
	public class Award : MonoBehaviour
	{
		public Animator Animator;
		public float ShowingSpeed;
		public float GettingAwardsSpeed;
		public int Coins = 0;

		private void Start()
		{
			Animator = GetComponent<Animator>();
			Animator.enabled = false;
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

			if (Vector2.Distance(awardsSlot.transform.position, transform.position) < 10f)
			{
				GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				transform.SetParent(awardsSlot.transform);

				ShowCoinsAmount();
				return;
			}

			var destination = awardsSlot.transform.position - transform.position;
			GetComponent<Rigidbody2D>().velocity = destination.normalized * ShowingSpeed;
		}

		public void ShowCoinsAmount()
		{
			GetComponentInChildren<TMP_Text>().text = Coins.ToString();
			FindObjectOfType<CoinCounterText>().IncrementCoins(Coins);
		}

		public void GettingAwards()
		{
			var coinCounter = GameObject.Find("CoinCounter");

			if (transform.parent == coinCounter.transform)
				return;

			var destination = coinCounter.transform.position - transform.position;
			GetComponent<Rigidbody2D>().velocity = destination.normalized * GettingAwardsSpeed;

			Animator.enabled = true;
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