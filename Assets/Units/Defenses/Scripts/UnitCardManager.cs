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

	// Cooldown buy logic.
	public float CooldownTime;
	private float _nextCooldownTime;
	private bool _canTakeNewUnit;

	public void OnDrag(PointerEventData eventData)
	{
		if (!_canTakeNewUnit)
			return;

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
		if (_nextCooldownTime > Time.time)
			return;

		_canTakeNewUnit = true;

		UnitDragged = Instantiate(Prefab, new Vector3(0, 0, -1), Quaternion.identity);
		UnitDragged.GetComponent<SpriteRenderer>().sprite = Sprite;

		var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		UnitDragged.transform.position = new Vector3(position.x, position.y, -9);
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (!_canTakeNewUnit)
			return;

		if (Collider == null || (!UnitScriptableObject.IsRange && Collider.IsAssigned))
		{
			Destroy(UnitDragged);
			return;
		}

		UnitDragged.tag = "Untagged";

		if (UnitScriptableObject.IsRange)
		{
			Collider.IsAssigned = true;
		}

		UnitDragged.transform.SetParent(Collider.transform);
		UnitDragged.transform.localPosition = new Vector3(0, 0.25f, -1);

		var unitManager = UnitDragged.GetComponent<UnitBasic>();
		unitManager.IsDragged = false;
		unitManager.BulletType = UnitScriptableObject.BulletType;
		unitManager.IsRange = UnitScriptableObject.IsRange;

		unitManager.Health = UnitScriptableObject.Health;
		unitManager.Speed = UnitScriptableObject.Speed;
		unitManager.AttackSpeed = UnitScriptableObject.AttackSpeed;
		unitManager.AttackDamage = UnitScriptableObject.AttackDamage;
			
		if (!UnitScriptableObject.IsRange)
		{
			unitManager.BulletPrefab = null;			
		}

		var animator = UnitDragged.GetComponent<Animator>();
		animator.runtimeAnimatorController = UnitScriptableObject.Animator;
		animator.SetFloat("AttackSpeed", UnitScriptableObject.AttackSpeed);
		
		_nextCooldownTime = Time.time + CooldownTime;
		_canTakeNewUnit = false;
	}
}
