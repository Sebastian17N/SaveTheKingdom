using UnityEngine;

namespace Assets.Scenes.BeforeTheBattle.Scripts
{
	public class ChangeSceneManager : Common.Managers.ChangeSceneManager
	{
		public override void ChangeScene()
		{
			SaveListOfChosenUnits();
			SaveListOfChosenSpells();

			base.ChangeScene();
		}
		public void SaveListOfChosenUnits()
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

			listOfChosenUnits = listOfChosenUnits.Remove(listOfChosenUnits.Length - 1);
			PlayerPrefs.SetString("UnitChosenToBattle", listOfChosenUnits);
		}

		public void SaveListOfChosenSpells()
        {
			var spellContainer = GameObject.Find("SpellEmptySlots");

			var listOfChosenSpells = string.Empty;

			for (var i = 0; i < spellContainer.transform.childCount; ++i)
			{
				var slot = spellContainer.transform.GetChild(i);

				if (slot.transform.childCount == 1)
					listOfChosenSpells += $"{slot.GetComponentInChildren<SpellChosen>().SpellAvailableToChoose.SpellId};";
			}

			if (listOfChosenSpells.Length == 0)
				return;

			listOfChosenSpells = listOfChosenSpells.Remove(listOfChosenSpells.Length - 1);
			PlayerPrefs.SetString("SpellChosenToBattle", listOfChosenSpells);
		}
	}
}
