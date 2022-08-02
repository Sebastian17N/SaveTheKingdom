using UnityEngine;

namespace Assets.Map.Scripts
{
	public class MoonStone : MonoBehaviour
	{   
		private void OnMouseDown()
		{
			FindObjectOfType<PointsCounter>().IncrementPoints();

			Destroy(gameObject);
		}
	}
}
