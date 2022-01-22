using UnityEngine;
using UnityEngine.EventSystems;

public class UnitCardManager : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
	public UnitScriptableObject UnitScriptableObject;
	public Sprite Sprite;
	public GameObject Prefab;

	GameObject UnitDragged;

	public bool IsOverCollider = false;
	public FieldManager Collider;
	private FieldManager LastCollider;

	public void OnDrag(PointerEventData eventData)
	{
		UnitDragged.GetComponent<SpriteRenderer>().sprite = Sprite;

		if (LastCollider != Collider || LastCollider == null)
		{
			IsOverCollider = false;

			if (LastCollider != null)
				LastCollider.Unit = null;

			LastCollider = Collider;
		}

		// If you do not hover above field, Unit should be stick to your mouse pointer.
		if (!IsOverCollider)
			UnitDragged.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		else
			UnitDragged.transform.position = Collider.transform.position;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		UnitDragged = Instantiate(Prefab, new Vector3(0, 0, -1), Quaternion.identity);
		UnitDragged.GetComponent<SpriteRenderer>().sprite = Sprite;

		UnitDragged.transform.localPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (Collider != null && !Collider.IsAssigned)
		{
			UnitDragged.tag = "Untagged";
			UnitDragged.transform.SetParent(Collider.transform);
			UnitDragged.transform.position = new Vector3(0, 0, -1);
			UnitDragged.transform.localPosition = new Vector3(0, 0, -1);

			Collider.IsAssigned = true;
		}
		else
			Destroy(UnitDragged);
	}
}
