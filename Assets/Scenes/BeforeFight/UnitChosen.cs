using Assets.BeforeFight;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitChosen : MonoBehaviour, IPointerClickHandler
{
    public GameObject Slot;
    public UnitAvailableToChoose UnitAvailableToChoose;
	private bool _isAssignedToSlotAlready;

    void Update()
    {
	    if (_isAssignedToSlotAlready)
		    return;

	    // Unit icon is higher or equal than slot.
	    if (Slot != null && Vector3.Distance(transform.position, Slot.transform.position) < 10f)
	    {
		    transform.SetParent(Slot.transform);
		    GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 100);
		    GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100);
		    GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 20, 0);
		    GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

		    _isAssignedToSlotAlready = true;
	    }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
	    RemoveUnitFromSlot();
    }

    protected void RemoveUnitFromSlot()
	{
        UnitAvailableToChoose.IsAssignedToSlotAlready = false;
        UnitAvailableToChoose.IsAlreadyChosen = false;
        Destroy(transform.gameObject);
		transform.parent.GetComponent<UsingUnitIconSlot>().IsSlotAvailable = true;
	}
}
