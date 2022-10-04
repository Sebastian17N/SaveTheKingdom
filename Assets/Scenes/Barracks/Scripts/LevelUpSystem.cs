using Assets.Scenes.Barracks.Scripts;
using UnityEngine;

public class LevelUpSystem : MonoBehaviour
{
    public UpgradeButton UnitToUpgrade;
    public void LevelUpUnit()
    {
	    var scriptableObject = transform.parent.Find("UnitDataFolder")
		    .GetComponent<UnitDataFolder>().UnitScriptableObject;

	    scriptableObject.Health += 5;
    }
}
