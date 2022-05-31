using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpellIcon : MonoBehaviour, 
    IPointerClickHandler, IPointerDownHandler, IPointerExitHandler, IPointerUpHandler
{
    public GameObject PrefabSpellCard;
    GameObject SpellIconDragged;
    public Sprite Sprite;
    public bool IsSpellChecked = false;
    public Transform CanvasTransform;

    private bool _isAssignedToSlotAlready = false;
    private float _pointerDownTime;
    private const float _pointerClickDetailsTop = 0.2f;

    public float Speed;

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
        _pointerDownTime = Time.time;

        //SpellIconDragged = Instantiate(PrefabSpellCard, new Vector3(1, 1, 1), Quaternion.identity);
        //SpellIconDragged.transform.SetParent(transform.parent.transform.parent.transform);
        //SpellIconDragged.GetComponent<Image>().sprite = Sprite;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_isAssignedToSlotAlready)
            return;

        if (Time.time - _pointerDownTime > _pointerClickDetailsTop)
        {
            var animObject = Instantiate(this.gameObject);
            animObject.transform.SetParent(CanvasTransform);
            animObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 50);
            animObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 50);
            animObject.transform.position = transform.position;
            var spellEmptySlots = GameObject.Find("SpellEmptySlot").transform.position;
            var destination = spellEmptySlots - animObject.transform.position;
            animObject.GetComponent<Rigidbody2D>().velocity = destination.normalized * Speed;
            //jak zniszczyæ clona gdy osi¹gnie okreœlony vector/punkt/miejsce
            //dodaæ colider do slota i odpaliæ destroy?
            Destroy(animObject, 0.25f);

            PutNewSpellIntoSlot();
            return;
        }
    }

    private void PutNewSpellIntoSlot()
    {
        var spellEmptySlots = GameObject.Find("SpellEmptySlots");

        for (var childIndex = 0; childIndex < spellEmptySlots.transform.childCount; ++childIndex)
        {
            var slot = spellEmptySlots.transform.GetChild(childIndex);

            if (slot.Find("SpellIcon(Clone)") != null)
                continue;

            var newSpell = Instantiate(this, slot.transform);
            newSpell.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 50);
            newSpell.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 50);
            newSpell.GetComponent<RectTransform>().anchoredPosition = new Vector3(40, 50, 0);
            newSpell.name = this.name;
            break;
        }
    }
}
