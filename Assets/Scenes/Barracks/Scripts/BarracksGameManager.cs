using System.Collections.Generic;
using System.Linq;
using Assets.Scenes.Battleground.Units.Defenses.Scripts;
using Assets.Units.Defenses.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scenes.Barracks.Scripts
{
	public class BarracksGameManager : MonoBehaviour
	{
		[Header("UnitCard")]
		public GameObject PrefabUnitCard;
		public List<UnitScriptableObject> ScriptableObjects;

		public Transform GhagharUnitSlot;
		public Transform LaganatUnitSlot;
		public Transform HaikoUnitSlot;

		public Transform[] UnitSlots;
		
		[Header("UpdatePanel")]
		private readonly List<GameObject> _units = new();
		public GameObject SelectedUnit;
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
				CreateUnitFolder(unitsForOrigin[i], unitSlot);
			}
		}

		private void CreateUnitFolder(UnitScriptableObject scriptableObject, Transform unitSlot)
		{
			var unit = Instantiate(PrefabUnitCard, unitSlot);
			unit.GetComponent<Animator>().runtimeAnimatorController = scriptableObject.AnimatorCanvas;
            //unit.GetComponentInChildren<Image>().sprite = scriptableObject.Sprite;
            unit.GetComponentInChildren<Image>().type = Image.Type.Filled;
            unit.GetComponent<UnitDataFolder>().UnitScriptableObject = scriptableObject;
			unit.GetComponent<UnitDataFolder>().UnitIndex = scriptableObject.UnitId;
			unit.GetComponent<UnitDataFolder>().RefreshStatisticsTexts();
			
			//animator.runtimeAnimatorController = scriptableObject.Animator;
			//unit.GetComponent<UnitDataFolder>().animator = scriptableObject.Animator;

			_units.Add(unit);
		}
		public void RefreshAllUnitsTexts()
        {
            foreach (var unit in _units)
            {
				unit.GetComponent<UnitDataFolder>().RefreshStatisticsTexts();
			}
        }
		public void CreateUpdatePanel(UnitScriptableObject scriptableObject)
		{
			SelectedUnit = GetUnitByIndex(scriptableObject.UnitId);

			var canvas = FindObjectOfType<Canvas>();
			_updatePanel = Instantiate(UpdatePanelPrefab, canvas.transform);
			LoadUpdatePanel(_updatePanel, scriptableObject);
		}

		public GameObject GetUnitByIndex(int unitIndex)
			=> _units.Find(x => x.GetComponent<UnitDataFolder>().UnitIndex == unitIndex);

		public GameObject GetNextUnit(int currentUnitIndex, UnitOrigin groupOrigin)
			=> _units
				.Where(unit => unit.GetComponent<UnitDataFolder>().UnitScriptableObject.Origin == groupOrigin)
				.FirstOrDefault(unit => unit.GetComponent<UnitDataFolder>().UnitIndex > currentUnitIndex)
			??
			_units
				.Where(unit => unit.GetComponent<UnitDataFolder>().UnitScriptableObject.Origin == groupOrigin)
				.OrderBy(unit => unit.GetComponent<UnitDataFolder>().UnitIndex)
				.First();

		public GameObject GetPreviousUnit(int currentUnitIndex, UnitOrigin groupOrigin)
			=> _units
				   .Where(unit => unit.GetComponent<UnitDataFolder>().UnitScriptableObject.Origin == groupOrigin)
				   .LastOrDefault(unit => unit.GetComponent<UnitDataFolder>().UnitIndex < currentUnitIndex)
			   ??
			   _units
				   .Where(unit => unit.GetComponent<UnitDataFolder>().UnitScriptableObject.Origin == groupOrigin)
				   .OrderByDescending(unit => unit.GetComponent<UnitDataFolder>().UnitIndex)
				   .First();

		public void NextUnit()
		{
			SelectedUnit = GetNextUnit(SelectedUnit.GetComponent<UnitDataFolder>().UnitIndex,
				SelectedUnit.GetComponent<UnitDataFolder>().UnitScriptableObject.Origin);

			LoadUpdatePanel(_updatePanel,
				SelectedUnit
					.GetComponent<UnitDataFolder>()
					.UnitScriptableObject);
		}

		public void PreviousUnit()
		{
			SelectedUnit = GetPreviousUnit(SelectedUnit.GetComponent<UnitDataFolder>().UnitIndex,
				SelectedUnit.GetComponent<UnitDataFolder>().UnitScriptableObject.Origin);

			LoadUpdatePanel(_updatePanel,
				SelectedUnit
					.GetComponent<UnitDataFolder>()
					.UnitScriptableObject);
		}

		private void LoadUpdatePanel(GameObject updatePanel, UnitScriptableObject scriptableObject)
		{
			var panelComponents = 
				updatePanel.transform.GetComponentInChildren<UnitUpgradePanel>();

			updatePanel.transform.Find("UpDateUnitPanel/UnitDataFolder")
				.GetComponent<UnitDataFolder>().UnitScriptableObject = scriptableObject;

			updatePanel.transform.Find("UpDateUnitPanel/UnitDataFolder")
				.GetComponent<Image>().sprite = scriptableObject.Sprite;
			
			panelComponents.damageText.text = scriptableObject.AttackDamage.ToString();
			panelComponents.damageUpgradeText.text = 
				(scriptableObject.AttackDamage + scriptableObject.AttackDamageUpgrade).ToString();			
			
			panelComponents.healthText.text = scriptableObject.Health.ToString();
			panelComponents.healthUpgradeText.text = 
				(scriptableObject.Health + scriptableObject.HealthUpgrade).ToString();
			
			panelComponents.UnitIcon.sprite = scriptableObject.Icon;
			panelComponents.shardsOwnedText.text = scriptableObject.ShardsNumber.ToString();
			panelComponents.shardsNeededText.text = scriptableObject.ShardsNumber.ToString();
			panelComponents.coinsHavedText.text = PlayerPrefs.GetInt("Coins").ToString();
			panelComponents.coinsNeededText.text = scriptableObject.UpgradeInitialCost.ToString();
		}
	}
}