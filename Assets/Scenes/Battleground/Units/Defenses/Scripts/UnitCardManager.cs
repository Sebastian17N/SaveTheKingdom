using Assets.Map.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Units.Defenses.Scripts
{
	public class UnitCardManager : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
	{
		public UnitScriptableObject UnitScriptableObject;
		public Sprite Sprite;
		public GameObject Prefab;

		GameObject _unitDragged;

		public bool IsOverCollider = false;
		public FieldManager Collider;
		private FieldManager _lastCollider;

		// Cooldown buy logic.
		public float CooldownTime;
		private float _nextCooldownTime;
		private bool _canTakeNewUnit;

		public Image CooldownImage;
		public Image CooldownReadyImage;

		public void Update()
		{
			CooldownImage.fillAmount = (CooldownTime - (_nextCooldownTime - Time.time)) / CooldownTime;
			CooldownReadyImage.enabled = CooldownImage.fillAmount >= 1;
		}

		public void OnDrag(PointerEventData eventData)
		{
			if (!_canTakeNewUnit)
				return;

			_unitDragged.GetComponent<SpriteRenderer>().sprite = Sprite;

			if (_lastCollider != Collider || _lastCollider == null)
			{
				IsOverCollider = false;

				if (_lastCollider != null)
					_lastCollider.Unit = null;

				_lastCollider = Collider;
			}

			// If you do not hover above field, Unit should be stick to your mouse pointer.
			if (!IsOverCollider)
			{
				// If you do not hover above field, Unit should be stick to your mouse pointer.
				var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				_unitDragged.transform.position = new Vector3(position.x, position.y, -9);
			}
			else
			{
				// Unit should be snapped to the field.
				_unitDragged.transform.position = Collider.transform.position + new Vector3(0, 0.25f, 0);
			}
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			if (_nextCooldownTime > Time.time)
				return;

			_canTakeNewUnit = true;

			_unitDragged = Instantiate(Prefab, new Vector3(0, 0, -1), Quaternion.identity);
			_unitDragged.GetComponent<SpriteRenderer>().sprite = Sprite;

			var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			_unitDragged.transform.position = new Vector3(position.x, position.y, -9);
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			if (!_canTakeNewUnit)
				return;

			if (Collider == null || (!UnitScriptableObject.IsRange && Collider.IsAssigned))
			{
				Destroy(_unitDragged);
				return;
			}

			_unitDragged.tag = "Untagged";

			if (UnitScriptableObject.IsRange)
			{
				Collider.IsAssigned = true;
			}

			_unitDragged.transform.SetParent(Collider.transform);
			_unitDragged.transform.localPosition = new Vector3(0, 0.25f, -1);

			var unitManager = _unitDragged.GetComponent<UnitBasic>();
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

			var animator = _unitDragged.GetComponent<Animator>();
			animator.runtimeAnimatorController = UnitScriptableObject.Animator;
			//animator.SetFloat("AttackSpeed", UnitScriptableObject.AttackSpeed);
		
			_nextCooldownTime = Time.time + CooldownTime;

			_canTakeNewUnit = false;
		}
	}
}
