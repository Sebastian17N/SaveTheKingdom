using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitIcon : MonoBehaviour, IPointerDownHandler, IPointerExitHandler, IPointerClickHandler
{
    //public GameObject PrefabUnitCard;    
    GameObject UnitIconDragged;
    public Sprite Sprite;
    public bool IsUnitChecked = false;
    public Transform CanvasTransform;
    public bool IsInfoVisible = false;

    private bool IsAssignedToSlotAlready = false;

    void Start()
    {
        ChangeVisibilityInfoButton(false);
    }

    void Update()
	{
        if (!IsAssignedToSlotAlready)
		{
            // Przesu� ikon� do slotu.

            // Je�li pozycja ikony i slotu jest identyczna, to zmie� IsAssignedToSlotAlready = true;
		}
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!IsUnitChecked)
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            IsUnitChecked = true;
            ChangeVisibilityInfoButton(true);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            IsUnitChecked = false;
            ChangeVisibilityInfoButton(false);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        UnitIconDragged = Instantiate(transform.gameObject, new Vector3(0, 0, -4), Quaternion.identity);// GameObject.Find("Canvas").transform);
        UnitIconDragged.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 100);
        UnitIconDragged.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100);
        
        var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        UnitIconDragged.transform.position = new Vector3(position.x, position.y, 1);
        UnitIconDragged.transform.SetParent(CanvasTransform);


    }

    public void OnPointerDragged()
	{
        // Przesuwaj ikon� za myszk�.
	}

    public void OnPointerUp()
	{
        // 1. Znajd� pusty slot - czy w og�le, taki istnieje.
        // 1.a je�li nie to zako�cz, i zniknij ikonk� - zniszcz obiekt.
        // 2. Przypisz ikon� do pustego slotu, odpal animacj� poruszania, lub przesuwaj ikon� na Update do czasu, a� pozycja ikony nie zr�wna si� z pozycj� slotu.
	}

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = new Vector3(1, 1, 1);
        IsUnitChecked = false;
        ChangeVisibilityInfoButton(false);
    }
    public void ChangeVisibilityInfoButton(bool isInfoButtonVisible)
    {
        var infoButton = transform.Find("InfoButton").gameObject;   
        infoButton.SetActive(isInfoButtonVisible); 
    }
}
