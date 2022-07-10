using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.BeforeFight
{
	public class UnitAvailableToChoose : MonoBehaviour, IPointerClickHandler
	{
		public bool IsAssignedToSlotAlready = false;

		public Sprite Sprite;
		public Transform CanvasTransform;
		public bool IsAlreadyChosen;

		public GameObject UnitChosenPrefab;

		public float Speed;

		public void OnPointerClick(PointerEventData eventData)
		{
			if (!IsAssignedToSlotAlready && !IsAlreadyChosen)
			{
				PlaceUnitInFirstEmptySlot();
				return;
			}
		}

		protected void PlaceUnitInFirstEmptySlot()
		{
			IsAlreadyChosen = true;
			transform.GetComponent<Image>().color = Color.grey;

			var unitEmptySlots = GameObject.Find("UnitEmptySlots");
			GameObject foundSlot = null;

			for (var childIndex = 0; childIndex < unitEmptySlots.transform.childCount; ++childIndex)
			{
				var slot = unitEmptySlots.transform.GetChild(childIndex);

				if (slot.Find("UnitIcon(Clone)") != null)
					continue;

				foundSlot = slot.gameObject;
				break;
			}

			if (foundSlot == null)
			{
				IsAlreadyChosen = false;
				return;
			}

			var animObject = Instantiate(UnitChosenPrefab, CanvasTransform);
			animObject.name = name;
			animObject.GetComponent<UnitChosen>().Slot = foundSlot;
			animObject.GetComponent<Image>().sprite = Sprite;
			animObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 100);
			animObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100);
			animObject.transform.position = transform.position;

			var destination = foundSlot.transform.position - animObject.transform.position;
			animObject.GetComponent<Rigidbody2D>().velocity = destination.normalized * Speed;

			animObject.GetComponent<UnitChosen>().UnitAvailableToChoose = this;
		}
    }
}
