using UnityEngine;
using UnityEngine.EventSystems;

public class UsingSpellSlot : MonoBehaviour, IDropHandler
{
	public GameObject UsingSpell;

	public void OnDrop(PointerEventData eventData)
    {
		
		if (eventData.pointerEnter != null)
        {
            eventData.pointerEnter.GetComponent<RectTransform>().anchoredPosition = 
                GetComponent<RectTransform>().anchoredPosition;
			
		}
		
	}

	public void OnMouseOver()
	{
		foreach (var manager in FindObjectsOfType<SpellCardManager>())
		{
			manager.SpellSlot = GetComponent<UsingSpellSlot>();
		}

		foreach (var manager in FindObjectsOfType<SelectedSpell>())
		{
			manager.SpellSlot = GetComponent<UsingSpellSlot>();
		}
	}

	public void OnMouseExit()
	{
		foreach (var manager in FindObjectsOfType<SpellCardManager>())
		{
			manager.SpellSlot = null;
		}

		foreach (var manager in FindObjectsOfType<SelectedSpell>())
		{
			manager.SpellSlot = null;
		}
	}
}
