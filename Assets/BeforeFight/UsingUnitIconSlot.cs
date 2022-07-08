using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UsingUnitIconSlot : MonoBehaviour, IPointerClickHandler//, IDropHandler
{

    public void OnPointerClick(PointerEventData eventData)
    {
		Debug.Log("Click");
				
	}
	private void OnMouseDown()
	{
        var unitIconClone = transform.Find("UnitIcon(Clone)");
        Destroy(unitIconClone);

        var unitIconOryginal = GameObject.FindObjectOfType<UnitIcon>();
        unitIconOryginal.transform.GetComponent<Image>().color = Color.white;
        unitIconOryginal.IsAssignedToSlotAlready = false;
    }


}
