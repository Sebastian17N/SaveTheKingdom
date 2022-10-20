using TMPro;
using UnityEngine;

namespace Assets.Scenes.FightSummary.Scripts
{
	public class AwardController : MonoBehaviour
	{
		public Animator Animator;

		public float ShowingSpeed;
		public float GettingAwardsSpeed;

		public AwardTypeEnum Type;
		public int Quantity;

		public AwardController(string type, int quantity)
		{
			Quantity = quantity;
		}

		private void Start()
		{
			Animator = GetComponent<Animator>();
			Animator.enabled = false;
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

			if (Mathf.Abs(awardsSlot.transform.position.y - transform.position.y) < 10f)
			{
				GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				transform.SetParent(awardsSlot.transform);

				ShowAward();
				return;
			}

			var destination = awardsSlot.transform.position * Vector2.up - transform.position * Vector2.up;
			GetComponent<Rigidbody2D>().velocity = destination.normalized * Vector2.up * ShowingSpeed;
		}

		public void ShowAward()
		{
			GetComponentInChildren<TMP_Text>().text = Quantity.ToString();
			FindObjectOfType<CoinCounterText>().IncrementCoins(Quantity);
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