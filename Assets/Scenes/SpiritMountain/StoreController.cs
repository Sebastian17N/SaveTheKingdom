using UnityEngine;

namespace Assets.Scenes.SpiritMountain
{
	public class StoreController : MonoBehaviour
	{
		public static Vector3 newPose;
		public float newWidth;
		public static bool SelectMove;
		public Transform storeContainer;
		public float lerpTime;
		private GameObject _lastSpirit;
		public string FirstObjectToExpand;

		private void Start()
		{
			SelectSpirit(storeContainer.Find(FirstObjectToExpand).gameObject);        
		}
		private void Update()
		{
			if(storeContainer.position != newPose && SelectMove)
			{
				storeContainer.position = Vector3.Lerp(storeContainer.position, newPose, lerpTime * Time.deltaTime);
			}
			if(Vector3.Distance(storeContainer.position, newPose) < 0.1f)
			{
				storeContainer.position = newPose;
				SelectMove = false;
			}
		}

		public void SelectSpirit(GameObject spirit)
		{
			if(_lastSpirit != null)
			{
				_lastSpirit.transform.localScale = new Vector3(1, 1, -2);
				_lastSpirit.GetComponent<StoreButton>().CloseDetails();
			}
			_lastSpirit = spirit;
			spirit.transform.localScale = new Vector3(newWidth, 1.5f, 1.5f);
			spirit.GetComponent<StoreButton>().ShowDetails();
		}
	}
}
