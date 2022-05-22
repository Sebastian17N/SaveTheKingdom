using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpellIcon : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerExitHandler
{
    public GameObject PrefabSpellCard;
    GameObject SpellIconDragged;
    public Sprite Sprite;
    public bool IsSpellChecked = false;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!IsSpellChecked)
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            IsSpellChecked = true;
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            IsSpellChecked = false;
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = new Vector3(1, 1, 1);
        IsSpellChecked = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SpellIconDragged = Instantiate(PrefabSpellCard, new Vector3(1, 1, 1), Quaternion.identity);
        SpellIconDragged.transform.SetParent(transform.parent.transform.parent.transform);
        SpellIconDragged.GetComponent<Image>().sprite = Sprite;
    }
}
