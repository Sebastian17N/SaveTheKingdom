using UnityEngine;

namespace Assets.Scenes.Barracks
{
	public class UpgradeButton : MonoBehaviour
	{   
		public GameObject UpdatePanelPrefab;
    
		public void CreateUpdatePanel()
		{
			var scriptableObject = transform.parent.GetComponent<UnitDataFolder>().UnitScriptableObject;
			var gameManager = GameObject.Find("BarracksGameManager").
				gameObject.GetComponent<BarracksGameManager>();

			gameManager.CreateUpdatePanel(scriptableObject);
		}

	}
}
