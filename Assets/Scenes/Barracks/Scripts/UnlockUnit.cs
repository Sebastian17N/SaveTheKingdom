using Assets.Common.JsonModel;
using Assets.Scenes.Barracks.Scripts;
using Assets.Units.Defenses.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UnlockUnit : MonoBehaviour
{
    public Button UnlockButton;
    void Start()
    {
        ActivateUnlockButton();
    }

    void Update()
    {
        if (FindParentUnit().Unlocked == true)
        {
            Destroy(gameObject);
        }
    }
    private void ActivateUnlockButton()
    {
        var unitScriptableObject = FindParentUnit();

        var unitShardsNumber = PlayerPreferences.Load().Shards.FirstOrDefault(shard => shard.ShardId == unitScriptableObject.UnitId)?.Amount;

        if (unitScriptableObject.ShardsNeededToUpgrade <= unitShardsNumber)
        {
            UnlockButton.gameObject.SetActive(true);
        }
    }
    public void ActivateUnit()
    {
        FindParentUnit().Unlocked = true;
    }
    private UnitScriptableObject FindParentUnit()
    {
        var unitScriptableObject = transform.parent.GetComponent<UnitDataFolder>().UnitScriptableObject;

        return(unitScriptableObject);   
    }

}
