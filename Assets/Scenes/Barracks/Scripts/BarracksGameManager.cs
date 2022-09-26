using System.Collections.Generic;
using System.Linq;
using Assets.Scenes.Battleground.Units.Defenses.Scripts;
using Assets.Units.Defenses.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.WebCam;

namespace Assets.Scenes.Barracks.Scripts
{
	public class BarracksGameManager : MonoBehaviour
	{
		//UnitCard
		public GameObject PrefabUnitCard;
		public List<UnitScriptableObject> ScriptableObjects;

		public Transform GhagharUnitSlot;
		public Transform LaganatUnitSlot;
		public Transform HaikoUnitSlot;

		public Transform[] UnitSlots;

		//UpdatePanel
		private readonly List<GameObject> _units = new();
		public int SelectedUnit;
		private GameObject _updatePanel;
		public GameObject UpdatePanelPrefab;

		public void Start()
		{
			GenerateUnitFoldersPerOrigin(UnitOrigin.Ghagar, GhagharUnitSlot);
			GenerateUnitFoldersPerOrigin(UnitOrigin.Laganat, LaganatUnitSlot);
			GenerateUnitFoldersPerOrigin(UnitOrigin.Haiko, HaikoUnitSlot);
		}

		private void GenerateUnitFoldersPerOrigin(UnitOrigin origin, Transform unitSlot)
		{
			var unitsForOrigin = ScriptableObjects.Where(unit => unit.Origin == origin).ToList();
			
			for (var i = 0; i < unitsForOrigin.Count; i++)
			{
				CreateUnitFolder(unitsForOrigin[i], i, unitSlot);
			}
		}

		private void CreateUnitFolder(UnitScriptableObject scriptableObject, int unitIndex, Transform unitSlot)
		{
			var unit = Instantiate(PrefabUnitCard, unitSlot);
			unit.GetComponentInChildren<Image>().sprite = scriptableObject.Sprite;
			unit.GetComponentInChildren<Image>().type = Image.Type.Filled;
			unit.GetComponent<UnitDataFolder>().UnitScriptableObject = scriptableObject;
			unit.GetComponent<UnitDataFolder>().UnitIndex = unitIndex;
			SelectedUnit = unitIndex;

			var damageObject = unit.transform.Find("Damage");
			damageObject.transform.Find("DamageNumber").GetComponent<TMP_Text>().text =
				$"Damage: {scriptableObject.AttackDamage}";

			var healthObject = unit.transform.Find("Health");
			healthObject.transform.Find("HealthNumber").GetComponent<TMP_Text>().text =
				$"Health: {scriptableObject.Health}";

			_units.Add(unit);
		}

		public void CreateUpdatePanel(UnitScriptableObject scriptableObject)
		{
			var canvas = FindObjectOfType<Canvas>();
			_updatePanel = Instantiate(UpdatePanelPrefab, canvas.transform);
			LoadUpdatePanel(_updatePanel, scriptableObject);
		}

		public GameObject GetUnitByIndex(int unitIndex)
			=> _units.Find(x => x.GetComponent<UnitDataFolder>().UnitIndex == unitIndex);

		public void NextUnit()
		{
			SelectedUnit++;
			if (SelectedUnit == ScriptableObjects.Count)
			{
				SelectedUnit = 0;
			}

			LoadUpdatePanel(_updatePanel,
				GetUnitByIndex(SelectedUnit)
					.GetComponent<UnitDataFolder>()
					.UnitScriptableObject);
		}

		public void PreviousUnit()
		{
			SelectedUnit--;
			if (SelectedUnit < 0)
			{
				SelectedUnit = ScriptableObjects.Count - 1;
			}

			LoadUpdatePanel(_updatePanel,
				GetUnitByIndex(SelectedUnit)
					.GetComponent<UnitDataFolder>()
					.UnitScriptableObject);
		}

		private void LoadUpdatePanel(GameObject updatePanel, UnitScriptableObject scriptableObject)
		{
			updatePanel.transform.Find("UpDateUnitPanel/UnitDataFolder")
				.GetComponent<Image>().sprite = scriptableObject.Sprite;

			var damageObject = updatePanel.transform.Find("UpDateUnitPanel/UnitDataFolder/Damage");
			damageObject.transform.Find("DamageNumber").GetComponent<TMP_Text>().text =
				scriptableObject.AttackDamage.ToString();

			var healthObject = updatePanel.transform.Find("UpDateUnitPanel/UnitDataFolder/Health");
			healthObject.transform.Find("HealthNumber").GetComponent<TMP_Text>().text =
				scriptableObject.Health.ToString();

			var coinObject = updatePanel.transform.Find("UpDateUnitPanel/CoinIcon/CoinsText");
			coinObject.transform.Find("CoinsHavedText").GetComponent<TMP_Text>().text =
				PlayerPrefs.GetInt("coins").ToString();
		}
	}
}