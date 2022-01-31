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

	public Transform BackgroundTransform;

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

		if (!IsOverCollider)
		{
			// If you do not hover above field, Unit should be stick to your mouse pointer.
			var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			UnitDragged.transform.position = new Vector3(position.x, position.y, -9);
		}
		else
		{
			// Unit should be snapped to the field.
			UnitDragged.transform.position = Collider.transform.position + new Vector3(0, 0.25f, 0);
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		UnitDragged = Instantiate(Prefab, new Vector3(0, 0, -1), Quaternion.identity);
		UnitDragged.GetComponent<SpriteRenderer>().sprite = Sprite;

		UnitDragged.transform.SetParent(BackgroundTransform);

		var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		UnitDragged.transform.position = new Vector3(position.x, position.y, -9);
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (Collider != null && !Collider.IsAssigned)
		{
			Collider.IsAssigned = true;

			UnitDragged.tag = "Untagged";
			UnitDragged.transform.SetParent(Collider.transform);
			UnitDragged.transform.localPosition = new Vector3(0, 0.25f, -1);
			var unitManager = UnitDragged.GetComponent<UnitManager>();
			unitManager.IsDragged = false;
			unitManager.BulletType.Sprite = UnitScriptableObject.Bullet;
			unitManager.AttackSpeed = UnitScriptableObject.AttackSpeed;

			var animator = UnitDragged.GetComponent<Animator>();
			animator.runtimeAnimatorController = UnitScriptableObject.Animator;
			animator.SetFloat("AttackSpeed", UnitScriptableObject.AttackSpeed);

			Collider.IsAssigned = true;
		}
		else
			Destroy(UnitDragged);
	}
}
