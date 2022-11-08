using UnityEngine;

namespace Assets.Scenes.Barracks.Scripts
{
	public class UpgradeButton : MonoBehaviour
	{
		public void CreateUpdatePanel()
		{
			var scriptableObject =
				transform
					.parent
					.GetComponent<UnitDataFolder>()
					.UnitScriptableObject;

			var gameManager =
				GameObject
					.Find("BarracksGameManager")
					.gameObject
					.GetComponent<BarracksGameManager>();

			gameManager.CreateUpdatePanel(scriptableObject);
		}
	}
}