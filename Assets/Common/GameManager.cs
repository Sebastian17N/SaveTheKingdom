using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Attributes")]
    // Object template to get attributes from.
    public List<GameObject> UnitCards;
    public int Cost;
    public Texture Icon;

    [Header("Units")]
    public UnitScriptableObject[] ScriptableObjects;
    public GameObject Prefab;
	public Transform CardHolderTransform;
    public Transform BackgroundTransform;

	private void Start()
    {
        UnitCards = new List<GameObject>();

        // Load all units.
        foreach (UnitScriptableObject scriptableObject in ScriptableObjects)
        {
            UnitCards.Add(CreateUnit(scriptableObject));
        }
    }

    /// <summary>
    /// Creates an unit card on UI.
    /// </summary>
    /// <param name="unitScriptableObject">Scripable object template.</param>
    /// <returns>Created game object.</returns>
    private GameObject CreateUnit(UnitScriptableObject unitScriptableObject)
    {
        GameObject unit = Instantiate(Prefab, CardHolderTransform);
        
        Icon = unitScriptableObject.Icon;
        Cost = unitScriptableObject.Cost;
        
        unit.GetComponentInChildren<RawImage>().texture = Icon;
        unit.GetComponentInChildren<TMP_Text>().text = $"{Cost}";

        UnitCardManager manager = unit.GetComponent<UnitCardManager>();
        manager.UnitScriptableObject = unitScriptableObject;
        manager.Sprite = unitScriptableObject.Sprite;
        //manager.BackgroundTransform = BackgroundTransform;

        return unit;
    }
}
