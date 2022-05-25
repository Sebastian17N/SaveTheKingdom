using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UsingUnitIconSlot : MonoBehaviour, IDropHandler
{
	public void OnDrop(PointerEventData eventData)
	{
		if (eventData.pointerEnter != null)
		{
			eventData.pointerEnter.GetComponent<RectTransform>().anchoredPosition =
				GetComponent<RectTransform>().anchoredPosition;
		}

		transform.Find("UnitIcon(Clone)(Clone)").transform.localScale = new Vector3(2f, 2f, 2f);
	}
}
