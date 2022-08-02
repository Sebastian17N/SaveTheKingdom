using Assets.Scenes.SpiritMountain;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Map.Scripts
{
	public class UseSpell : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
	{
		//create and drag spell
		public SpellScriptableObject SpellScriptableObject;
		GameObject SpellDragged;
		public Sprite Sprite;
		public GameObject SpellPrefab;

		//move over field and drop
		public bool IsOverCollider = false;
		public FieldManager Collider;
		private FieldManager LastCollider;

		public void OnPointerDown(PointerEventData eventData)
		{
			SpellDragged = Instantiate(SpellPrefab, transform.position, Quaternion.identity); 
			SpellDragged.GetComponent<SpriteRenderer>().sprite = Sprite;       
		}
  
		public void OnDrag(PointerEventData eventData)
		{
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
				SpellDragged.transform.position = new Vector3(position.x, position.y, 0);
			}
			else
			{
				// Unit should be snapped to the field.
				SpellDragged.transform.position = Collider.transform.position + new Vector3(0, 0.25f, 0);
			}
		}
    
		public void OnPointerUp(PointerEventData eventData)
		{
			if (Collider == null || Collider.IsAssigned)
			{
				Destroy(SpellDragged);
				return;
			}
		}
	}
}
