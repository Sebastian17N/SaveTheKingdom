using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpellCardManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    //public Canvas canvas;
    //private RectTransform _rectTransform;
    //public Transform BackgroundTransform;
    // private CanvasGroup _canvasGroup;
    
    public SpellScriptableObject SpellScriptableObject;
    public Sprite Sprite;
    public GameObject SpellPrefab;

    GameObject SpellDragged;

    public UsingSpellSlot SpellSlot;
    
    public void OnDrag(PointerEventData eventData)
    {
        SpellDragged.GetComponent<Image>().sprite = Sprite;
        var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        SpellDragged.transform.position = new Vector3(position.x, position.y, -9);        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SpellDragged = Instantiate(SpellPrefab, new Vector3(0,0,-1), Quaternion.identity);        
        SpellDragged.GetComponent<Image>().sprite = Sprite;

        var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);//konwertuje pozycje myszy na pozycjê obiektu w œwiecie
        SpellDragged.transform.position = new Vector3(position.x, position.y, -9);
    }

	public void OnPointerUp(PointerEventData eventData)
	{
        if (SpellSlot == null)
		{
            Destroy(SpellDragged);
            return;
		}

        SpellDragged.transform.SetParent(SpellSlot.transform);
        SpellDragged.transform.localPosition = new Vector3(0, 0, 0);
        
        var rectTransform = SpellDragged.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(30, 30);
    }
}

