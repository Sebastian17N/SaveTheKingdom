using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitIconDragged : MonoBehaviour, IDragHandler 
{
    public GameObject UnitIconPrefab;    
    public GameObject UnitIconSlot;

    public void OnPointerUp(PointerEventData eventData)
    {
        if (UnitIconSlot == null)
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
    }

}
