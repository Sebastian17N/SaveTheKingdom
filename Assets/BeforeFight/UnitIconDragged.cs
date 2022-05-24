using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitIconDragged : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(transform.parent);
        Debug.Log("OnDrag");
        var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(position.x, position.y, position.z);
    }

    public void OnDrag(PointerEventData eventData)
    {
       
    }

}
