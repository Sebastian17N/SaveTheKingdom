using UnityEngine;

namespace Assets.Scenes.Barracks
{
	public class ClosingButton : MonoBehaviour
	{
		public void CloseTab()
		{
			Destroy(transform.parent.transform.parent.gameObject);
		}
	}
}
