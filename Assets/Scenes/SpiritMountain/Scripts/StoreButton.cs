using UnityEngine;

namespace Assets.Scenes.SpiritMountain.Scripts
{
	public class StoreButton : MonoBehaviour
	{
		public Transform CenterStore;
		public Transform StoreContainer;

		public void OnClickSpirit()
		{
			var dis = CenterStore.position.x - transform.position.x;
			StoreController.NewPose = new Vector3
				(StoreContainer.position.x + dis, StoreContainer.position.y, StoreContainer.position.z);
			StoreController.SelectMove = true;
			CenterStore.GetComponent<StoreController>().SelectSpirit(transform.gameObject);
		}

		public void ShowDetails()
		{
			ChangeStateOfAllChildren(true);
		}

		public void CloseDetails()
		{
			ChangeStateOfAllChildren(false);
		}

		private void ChangeStateOfAllChildren(bool state)
		{
			for (var i = 0; i < transform.childCount; i++)
			{
				transform.GetChild(i).transform.gameObject.SetActive(state);
			}
		}
	}
}
