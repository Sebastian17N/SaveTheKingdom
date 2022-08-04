using UnityEngine;

namespace Assets.Scenes.BeforeTheBattle.Scripts
{
	public class ChangeSceneManager : Assets.Common.ChangeSceneManager
	{
		public override void ChangeScene()
		{
			var slotsContainer = GameObject.Find("UnitEmptySlots");

			var listOfChosenUnits = string.Empty;

			for (var i = 0; i < slotsContainer.transform.childCount; ++i)
			{
				var slot = slotsContainer.transform.GetChild(i);

				if (slot.transform.childCount == 1)
					listOfChosenUnits += $"{slot.GetComponentInChildren<UnitChosen>().UnitAvailableToChoose.UnitId};";
			}

			if (listOfChosenUnits.Length == 0)
				return;

			listOfChosenUnits.Remove(listOfChosenUnits.Length - 1);
			PlayerPrefs.SetString("UnitChosenToBattle", listOfChosenUnits);

			base.ChangeScene();
		}
	}
}
