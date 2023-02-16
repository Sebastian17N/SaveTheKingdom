using System.Collections;
using UnityEngine;

namespace Assets.Scenes.FightSummary.Scripts
{
	public class ChestController : MonoBehaviour
	{
		Animator _animator;
		public GameObject AwardMasterController;

		private bool _isChestClicked;

		void Start()
		{
			_animator = GetComponent<Animator>();			
		}

		public void PlayerClickOnChest()
		{
			if (_isChestClicked) 
				return;

			_animator.SetTrigger("PlayerClick");
			StartCoroutine(ActivateAwards());
			_isChestClicked = true;
		}

		public IEnumerator ActivateAwards()
		{
			yield return new WaitForSeconds(2);
			var award = Instantiate(AwardMasterController, transform.position, Quaternion.identity);
			award.transform.SetParent(transform);
		}
	}
}