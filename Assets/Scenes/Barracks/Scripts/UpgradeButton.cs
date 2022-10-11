using Assets.Units.Defenses.Scripts;
using UnityEngine;

namespace Assets.Scenes.Barracks.Scripts
{
	public class UpgradeButton : MonoBehaviour
	{
		//public UnitScriptableObject ChosenUnit;
		public void CreateUpdatePanel()
		{
			var scriptableObject =
				transform
					.parent
					.GetComponent<UnitDataFolder>()
					.UnitScriptableObject;

			//ChosenUnit = scriptableObject;

			var gameManager =
				GameObject
					.Find("BarracksGameManager")
					.gameObject
					.GetComponent<BarracksGameManager>();

			gameManager.CreateUpdatePanel(scriptableObject);
		}
	}
}