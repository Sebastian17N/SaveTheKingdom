using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpellCardManager : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    //public Canvas canvas;
    //private RectTransform _rectTransform;
    //public Transform BackgroundTransform;
    // private CanvasGroup _canvasGroup;
    
    public SpellScriptableObject SpellScriptableObject;
    public Sprite Sprite;
    public GameObject SpellPrefab;

    GameObject SpellDragged;
    

    private void Awake()
    {
        //_rectTransform = GetComponent<RectTransform>();
        //_canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        //_canvasGroup.alpha = 0.6f;
        //_canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        SpellDragged.GetComponent<Image>().sprite = Sprite;
        var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        SpellDragged.transform.position = new Vector3(position.x, position.y, -9);
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //_canvasGroup.alpha = 1;
        //_canvasGroup.blocksRaycasts = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SpellDragged = Instantiate(SpellPrefab, new Vector3(0,0,-1), Quaternion.identity);        
        SpellDragged.GetComponent<Image>().sprite = Sprite;

        var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);//konwertuje pozycje myszy na pozycjê obiektu w œwiecie
        SpellDragged.transform.position = new Vector3(position.x, position.y, -9);
    }
}

