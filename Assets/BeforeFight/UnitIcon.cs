using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitIcon : MonoBehaviour, IPointerDownHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameObject PrefabUnitCard;    
    GameObject UnitIconDragged;
    public Sprite Sprite;
    public bool IsUnitChecked = false;
    public Transform CanvasTransform;
    public bool IsInfoVisible = false;

    private bool IsAssignedToSlotAlready = false;

    void Start()
    {
        HideInfoButton(false);
    }

    void Update()
	{
        if (!IsAssignedToSlotAlready)
		{
            // Przesuń ikonę do slotu.

            // Jeśli pozycja ikony i slotu jest identyczna, to zmień IsAssignedToSlotAlready = true;
		}
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!IsUnitChecked)
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            IsUnitChecked = true;
            HideInfoButton(true);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            IsUnitChecked = false;
            HideInfoButton(false);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        UnitIconDragged = Instantiate(transform.gameObject, GameObject.Find("Canvas").transform);
        UnitIconDragged.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 100);
        UnitIconDragged.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100);

        var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        UnitIconDragged.transform.position = new Vector3(position.x, position.y, position.z);
    }

    public void OnPointerDragged()
	{
        // Przesuwaj ikonę za myszką.
	}

    public void OnPointerUp()
	{
        // 1. Znajdź pusty slot - czy w ogóle, taki istnieje.
        // 1.a jeśli nie to zakończ, i zniknij ikonkę - zniszcz obiekt.
        // 2. Przypisz ikonę do pustego slotu, odpal animację poruszania, lub przesuwaj ikonę na Update do czasu, aż pozycja ikony nie zrówna się z pozycją slotu.
	}

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = new Vector3(1, 1, 1);
        IsUnitChecked = false;
    }

    public void HideInfoButton(bool isInfoButtonVisible)
    {
        var infoButton = GameObject.Find("InfoButton");   
        infoButton.SetActive(isInfoButtonVisible); 
    }
}
