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
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!IsUnitChecked)
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            IsUnitChecked = true;
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            IsUnitChecked = false;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
        UnitIconDragged = Instantiate(PrefabUnitCard, new Vector3(1,1,1) , Quaternion.identity);
        UnitIconDragged.transform.SetParent(transform.parent.transform.parent.transform);
        UnitIconDragged.GetComponent<Image>().sprite = Sprite;

        //var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //UnitIconDragged.transform.position = new Vector3(position.x, position.y, position.z);
        //UnitIconDragged.transform.SetParent(CanvasTransform);
        //UnitIconDragged.GetComponent<SpellDraggedManager>().SpellPrefab = transform.gameObject;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = new Vector3(1, 1, 1);
        IsUnitChecked = false;
    }

}
