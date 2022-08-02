using UnityEngine;

namespace Assets.Common
{
	public class LifeTimeObject : MonoBehaviour
	{
		[SerializeField]
		public float LifeTime = 5f;
		public void Start()
		{
			Destroy(gameObject, LifeTime);
		}
	}
}
