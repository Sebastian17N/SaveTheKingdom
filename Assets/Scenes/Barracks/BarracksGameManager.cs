using System.Collections.Generic;
using Assets.Units.Defenses.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scenes.Barracks
{
	public class BarracksGameManager : MonoBehaviour
	{
		//UnitCard
		public GameObject PrefabUnitCard;
		public UnitScriptableObject[] ScriptableObjects;
		public Transform LaganatUnitSlot;

		//UpdatePanel
		private readonly List<GameObject> _units = new();
		private int _selectedUnit;
		private GameObject _updatePanel;
		public GameObject UpdatePanelPrefab;

		public void Start()
		{
			for (int i = 0; i < ScriptableObjects.Length; i++)
			{
				CreateUnitFolder(ScriptableObjects[i], i);
			}
		}

		private void CreateUnitFolder(UnitScriptableObject scriptableObject, int unitIndex)
		{
			GameObject unit = Instantiate(PrefabUnitCard, LaganatUnitSlot);
			unit.GetComponentInChildren<Image>().sprite = scriptableObject.Sprite;
			unit.GetComponentInChildren<Image>().type = Image.Type.Filled;
			unit.GetComponent<UnitDataFolder>().UnitScriptableObject = scriptableObject;
			unit.GetComponent<UnitDataFolder>().UnitIndex = unitIndex;
			_selectedUnit = unitIndex;

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

		private GameObject GetUnitByIndex(int unitIndex)
			=> _units.Find(x => x.GetComponent<UnitDataFolder>().UnitIndex == unitIndex);

		public void NextUnit()
		{
			_selectedUnit++;
			if (_selectedUnit == ScriptableObjects.Length)
			{
				_selectedUnit = 0;
			}

			LoadUpdatePanel(_updatePanel,
				GetUnitByIndex(_selectedUnit)
					.GetComponent<UnitDataFolder>()
					.UnitScriptableObject);
		}

		public void PreviousUnit()
		{
			_selectedUnit--;
			if (_selectedUnit < 0)
			{
				_selectedUnit = ScriptableObjects.Length - 1;
			}

			LoadUpdatePanel(_updatePanel,
				GetUnitByIndex(_selectedUnit)
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