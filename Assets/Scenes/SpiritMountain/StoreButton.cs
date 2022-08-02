using UnityEngine;

namespace Assets.Scenes.SpiritMountain
{
	public class StoreButton : MonoBehaviour
	{
		public Transform centerStore;
		public Transform storeContainer;

		public void OnClickSpirit()
		{
			float dis = centerStore.position.x - transform.position.x;
			StoreController.newPose = new Vector3
				(storeContainer.position.x + dis, storeContainer.position.y, storeContainer.position.z);
			StoreController.SelectMove = true;
			centerStore.GetComponent<StoreController>().SelectSpirit(transform.gameObject);
		}

		public void ShowDetails()
		{
			for(int i = 0; i < transform.childCount; i++)
			{
				transform.GetChild(i).transform.gameObject.SetActive(true);
			}
		}

		public void CloseDetails()
		{
			for (int i = 0; i < transform.childCount; i++)
			{
				transform.GetChild(i).transform.gameObject.SetActive(false);
			}
		}    
	}
}
