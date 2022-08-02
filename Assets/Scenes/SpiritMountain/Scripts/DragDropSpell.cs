using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scenes.SpiritMountain.Scripts
{
	public class DragDropSpell : MonoBehaviour, IPointerDownHandler, IDragHandler
	{
		private RectTransform _rectTransform;

		public GameObject SpellDragged;
		public GameObject SpellPrefab;
		
		public void OnDrag(PointerEventData eventData)
		{
			var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			_rectTransform.transform.position = new Vector3(position.x, position.y, -9);
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			SpellDragged = Instantiate(SpellPrefab);
		}        
	}
}
