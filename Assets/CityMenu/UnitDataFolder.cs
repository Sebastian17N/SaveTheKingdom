using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitDataFolder : MonoBehaviour, 
    IPointerDownHandler, IPointerExitHandler //IPointerClickHandler //IBeginDragHandler
{
    public UnitScriptableObject UnitScriptableObject;
    public GameObject UnitDataFolderPrefab;
    GameObject UnitDragged;
    public Sprite Sprite;

    public bool IsUnitChecked = false;
    

    public void OnPointerDown(PointerEventData eventData)
    {
        
        UnitDragged = Instantiate(UnitDataFolderPrefab);
        UnitDragged.GetComponent<Image>().sprite = UnitScriptableObject.Icon;

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = new Vector3(1, 1, 1);
        IsUnitChecked = false;
    }    
    private void OnMouseDrag()
    {
     
    }

    
}
