using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectedSpell : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    //public Canvas Canvas;
    private RectTransform rectTransform;
    public UsingSpellSlot SpellSlot;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
        //var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnPointerUp(PointerEventData eventData)
    {

        if (SpellSlot == null)
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
        

        var rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(80, 80);

    }

}
