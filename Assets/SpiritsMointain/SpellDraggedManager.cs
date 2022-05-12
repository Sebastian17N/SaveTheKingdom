using UnityEngine;
using UnityEngine.EventSystems;

public class SpellDraggedManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    //public Canvas Canvas;
    public GameObject SpellPrefab;
    public GameObject SpellSlot;


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
