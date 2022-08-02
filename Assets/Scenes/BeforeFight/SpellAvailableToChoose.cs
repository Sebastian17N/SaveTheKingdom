using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpellAvailableToChoose : MonoBehaviour, IPointerClickHandler
{
	public bool IsAssignedToSlotAlready = false;

	public Sprite Sprite;
	public Transform CanvasTransform;
	public GameObject SpellChosenPrefab;
	public float Speed;

	private bool _isAlreadyChosen;
	public bool IsAlreadyChosen
	{
		get => _isAlreadyChosen;
		set
		{
			_isAlreadyChosen = value;
			transform.GetComponent<Image>().color = value ? Color.grey : Color.white;
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (!IsAssignedToSlotAlready && !IsAlreadyChosen)
		{
			PlaceSpellInFirstEmptySlot();
			return;
		}
	}

	protected void PlaceSpellInFirstEmptySlot()
	{
		IsAlreadyChosen = true;

		var spellEmptySlots = GameObject.Find("SpellEmptySlots");
		GameObject foundSlot = null;

		for (var childIndex = 0; childIndex < spellEmptySlots.transform.childCount; ++childIndex)
		{
			var slot = spellEmptySlots.transform.GetChild(childIndex);

			if (slot.GetComponent<UsingSpellIconSlot>().IsSlotAvailable == false)
				continue;

			foundSlot = slot.gameObject;
			slot.GetComponent<UsingSpellIconSlot>().IsSlotAvailable = false;

			break;
		}

		if (foundSlot == null)
		{
			IsAlreadyChosen = false;
			return;
		}

		var animObject = Instantiate(SpellChosenPrefab, CanvasTransform);
		animObject.name = name;
		animObject.GetComponent<SpellChosen>().Slot = foundSlot;
		animObject.GetComponent<Image>().sprite = Sprite;
		animObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 60);
		animObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 60);
		animObject.transform.position = transform.position;

		var destination = foundSlot.transform.position - animObject.transform.position;
		animObject.GetComponent<Rigidbody2D>().velocity = destination.normalized * Speed;

		animObject.GetComponent<SpellChosen>().SpellAvailableToChoose = this;
	}
}

