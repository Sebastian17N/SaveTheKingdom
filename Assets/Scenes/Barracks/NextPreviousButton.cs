using UnityEngine;

namespace Assets.Scenes.Barracks
{
	public class NextPreviousButton : MonoBehaviour
	{
		public void NextUnit()
		{
			GameObject.FindObjectOfType<BarracksGameManager>().NextUnit();
		}
		public void PreviousUnit()
		{
			GameObject.FindObjectOfType<BarracksGameManager>().PreviousUnit();
		}
	}
}
