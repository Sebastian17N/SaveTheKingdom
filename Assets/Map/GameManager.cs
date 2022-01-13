using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject DraggingObject;
    public GameObject CurrentContainer;

    public static GameManager Instance;

	private void Awake()
	{
		Instance = this;
	}

	public void PlaceUnitOnBattlefield(UnitCard card)
	{
		if(DraggingObject != null && CurrentContainer != null)
		{
			Instantiate(card.Unit, CurrentContainer.transform);
			CurrentContainer.GetComponent<Field>().IsAssigned = true;
		}
	}
}
