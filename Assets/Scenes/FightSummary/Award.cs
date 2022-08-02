using TMPro;
using UnityEngine;

namespace Assets.Scenes.FightSummary
{
	public class Award : MonoBehaviour
	{
		Animator animator;
		public float Speed;
		public float GettingAwarsSpeed;
		public int Coins = 0;
		private void Start()
		{
			animator = GetComponent<Animator>();
			animator.enabled = false;
		}
		void Update()    
		{
			ShowAwards();   
		}
		void ShowAwards()
		{    
			var awarsSlot = GameObject.Find("AwardsSlot");

			if (transform.parent == awarsSlot.transform)
				return;

			if (Vector2.Distance(awarsSlot.transform.position, transform.position) < 10f)
			{
				this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				this.transform.SetParent(awarsSlot.transform);
				ShowCoinsAmount();
				return;
			}

			var destination = awarsSlot.transform.position - this.transform.position;
			GetComponent<Rigidbody2D>().velocity = destination.normalized * Speed;              
		}
		public void ShowCoinsAmount()
		{
			GetComponentInChildren<TMP_Text>().text = Coins.ToString();
			FindObjectOfType<CoinCounterText>().IncrementCoins(Coins);

		}
		public void GettingAwars()
		{
			var coinCounter = GameObject.Find("CoinCounter");

			if (transform.parent == coinCounter.transform)
				return;

			if (Vector2.Distance(coinCounter.transform.position, transform.position) < 10f)
			{
				this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				this.transform.SetParent(coinCounter.transform);            
				return;
			}

			var destination = coinCounter.transform.position - this.transform.position;
			GetComponent<Rigidbody2D>().velocity = destination.normalized * GettingAwarsSpeed;

			animator.enabled = true;
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
