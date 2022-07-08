using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UsingSpellIconSlot : MonoBehaviour, IPointerClickHandler//, IDropHandler
{
    //public void OnDrop(PointerEventData eventData)
    //{
    //	if (eventData.pointerEnter != null)
    //	{
    //		eventData.pointerEnter.GetComponent<RectTransform>().anchoredPosition =
    //			GetComponent<RectTransform>().anchoredPosition;
    //	}

    //	transform.Find("SpellIcon(Clone)(Clone)").transform.localScale = new Vector3(2f, 2f, 2f);
    //}
    public void OnPointerClick(PointerEventData eventData)
    {
        var spellIconClone = transform.Find("SpellIcon(Clone)");
        Destroy(spellIconClone);
    }
}
