using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scenes.SpiritMountain
{
	public class SpellDraggedManager : MonoBehaviour, IPointerUpHandler, IDragHandler
	{
		public GameObject SpellPrefab;
		public GameObject SpellSlot;

		public void OnPointerUp(PointerEventData eventData)
		{
			if (SpellSlot == null)
			{
				Destroy(gameObject);
				return;
			}
		}

		public void OnDrag(PointerEventData eventData)
		{
			transform.SetParent(transform.parent);

			var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			transform.position = new Vector3(position.x, position.y, -9);
        
			var rectTransform = GetComponent<RectTransform>();
			rectTransform.sizeDelta = new Vector2(80, 80);
		}
	}
}
