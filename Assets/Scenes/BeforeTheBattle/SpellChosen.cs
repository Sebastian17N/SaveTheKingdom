using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scenes.BeforeTheBattle
{
	public class SpellChosen : MonoBehaviour, IPointerClickHandler
	{
		public GameObject Slot;
		public SpellAvailableToChoose SpellAvailableToChoose;
		private bool _isAssignedToSlotAlready;

		void Update()
		{
			if (_isAssignedToSlotAlready)
				return;

			// Spell icon is higher or equal than slot.
			if (Slot != null && Vector3.Distance(transform.position, Slot.transform.position) < 10f)
			{
				transform.SetParent(Slot.transform);
				GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 60);
				GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 60);
				GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

				_isAssignedToSlotAlready = true;
			}
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			RemoveSpellFromSlot();
		}

		protected void RemoveSpellFromSlot()
		{
			SpellAvailableToChoose.IsAssignedToSlotAlready = false;
			SpellAvailableToChoose.IsAlreadyChosen = false;
			Destroy(transform.gameObject);
			transform.parent.GetComponent<UsingSpellIconSlot>().IsSlotAvailable = true;
		}
	}
}
