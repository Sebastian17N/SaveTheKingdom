using UnityEngine;

namespace Assets.Scenes.SpiritMountain.Scripts
{
	public class StoreController : MonoBehaviour
	{
		public static Vector3 NewPose;
		public float NewWidth;
		public static bool SelectMove;
		public Transform StoreContainer;
		public float LerpTime;
		private GameObject _lastSpirit;
		public string FirstObjectToExpand;

		public void Start()
		{
			SelectSpirit(StoreContainer.Find(FirstObjectToExpand).gameObject);
		}

		public void Update()
		{
			if (StoreContainer.position != NewPose && SelectMove)
			{
				StoreContainer.position = Vector3.Lerp(StoreContainer.position, NewPose, LerpTime * Time.deltaTime);
			}

			if (Vector3.Distance(StoreContainer.position, NewPose) < 0.1f)
			{
				StoreContainer.position = NewPose;
				SelectMove = false;
			}
		}

		public void SelectSpirit(GameObject spirit)
		{
			if (_lastSpirit != null)
			{
				_lastSpirit.transform.localScale = new Vector3(1, 1, -2);
				_lastSpirit.GetComponent<StoreButton>().CloseDetails();
			}

			_lastSpirit = spirit;
			spirit.transform.localScale = new Vector3(NewWidth, 1.5f, 1.5f);
			spirit.GetComponentInChildren<StoreButton>().ShowDetails();
		}
	}
}