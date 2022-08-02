using Assets.Units.Defenses.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scenes.CityMenu.Scripts
{
	public class Castle : MonoBehaviour
	{
		[Header("MenuElements")] public GameObject PrefabMenu;
		public Button CastleButton;
		public GameObject PrefabEscapeButton;

		//[Header("CreatedObjects")]
		private GameObject _menu;
		private GameObject WindowGrid => _menu.transform.Find("WindowGrid").gameObject;

		[Header("UnitFolder")] public GameObject PrefabUnitCard;
		public UnitScriptableObject[] ScriptableObjects;

		void Start()
		{
			CastleButton.onClick.AddListener(OpenMenu);
		}

		private void OpenMenu()
		{
			_menu = Instantiate(PrefabMenu, transform.parent.transform);

			// Load all unitFolders.
			foreach (var scriptableObject in ScriptableObjects)
			{
				CreateUnitFolder(scriptableObject);
			}

			CreateEscapeButton();
		}

		private void CreateUnitFolder(UnitScriptableObject scriptableObject)
		{
			var grid = WindowGrid.transform.Find("UnitStatisticsGrid").gameObject;
			var unit = Instantiate(PrefabUnitCard, grid.transform);
		}

		private void CreateEscapeButton()
		{
			var grid = WindowGrid.transform.Find("UnitStatisticsGrid").gameObject;

			var escapeButton = Instantiate(PrefabEscapeButton, WindowGrid.transform);
			escapeButton.GetComponent<RectTransform>().anchoredPosition =
				new Vector2(WindowGrid.GetComponent<RectTransform>().rect.width / 2,
					WindowGrid.GetComponent<RectTransform>().rect.height / 2);
		}
	}
}