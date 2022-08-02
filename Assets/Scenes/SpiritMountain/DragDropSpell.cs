using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scenes.SpiritMountain
{
	public class DragDropSpell : MonoBehaviour, 
		IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
	{
		public Canvas canvas;
		private RectTransform _rectTransform;
		private CanvasGroup _canvasGroup;

		GameObject SpellDragged;
		GameObject SpellPrefab;

		private void Awake()
		{
			//_rectTransform = GetComponent<RectTransform>();
			//_canvasGroup = GetComponent<CanvasGroup>();
		}
		public void OnBeginDrag(PointerEventData eventData)
		{
			//_canvasGroup.alpha = 0.6f;
			//_canvasGroup.blocksRaycasts = false;
		}

		public void OnDrag(PointerEventData eventData)
		{
			var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			_rectTransform.transform.position = new Vector3(position.x, position.y, -9);
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			//_canvasGroup.alpha = 1;
			//_canvasGroup.blocksRaycasts = true;
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			SpellDragged = Instantiate(SpellPrefab);
		}        
	}
}
