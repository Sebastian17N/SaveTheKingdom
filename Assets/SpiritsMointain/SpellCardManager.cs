using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpellCardManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{        
    public SpellScriptableObject SpellScriptableObject;
    public Sprite Sprite;
    public GameObject SpellPrefab;

    GameObject SpellDragged;

    public UsingSpellSlot SpellSlot;
    public Transform CanvasTransform;

    public bool IsFromMenu;
    
    public void OnDrag(PointerEventData eventData)
    {        
        SpellDragged.GetComponent<Image>().sprite = Sprite;
        var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        SpellDragged.transform.position = new Vector3(position.x, position.y, -9);        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SpellDragged = Instantiate(SpellPrefab, new Vector3(0,0,-4), Quaternion.identity);        
        SpellDragged.GetComponent<Image>().sprite = Sprite;

        var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);//konwertuje pozycje myszy na pozycjê obiektu w œwiecie
        SpellDragged.transform.position = new Vector3(position.x, position.y, -9);
        SpellDragged.transform.SetParent(CanvasTransform);

        SpellDragged.GetComponent<SpellDraggedManager>().SpellPrefab = transform.gameObject;
        //transform.localScale = new Vector3(2f, 2f, 2f);      
    }

	public void OnPointerUp(PointerEventData eventData)
	{
        if (SpellSlot == null || IsSpellAlreadyUse(SpellSlot))
		{
            Destroy(SpellDragged);
            return;
		}

        if (SpellSlot.transform.childCount > 0 && SpellSlot.transform.GetChild(0)?.gameObject != null)
        {
            Destroy(SpellSlot.transform.GetChild(0).gameObject);
        }

        var chosenSpell = Instantiate(transform.gameObject, SpellSlot.transform);
        chosenSpell.transform.name = transform.name;
        chosenSpell.transform.localPosition = new Vector3(0, 0, 0);
        chosenSpell.GetComponent<SpellCardManager>().IsFromMenu = false;

        Destroy(SpellDragged);

        var rectTransform = chosenSpell.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(30, 30);

        if (!IsFromMenu)
            Destroy(transform.gameObject);
    }

    public bool IsSpellAlreadyUse(UsingSpellSlot spellSlot)
    {
        var spellContainer = spellSlot.transform.parent;
        var alreadyUsedSprites = new List<Sprite>();

        for (var i = 0; i < 3; i++)
        {
            var slot = spellContainer.transform.Find($"Slot{i + 1}");

            if (slot.transform.gameObject == transform.parent.gameObject)
                continue;

            var spellUsed = slot.transform.Find("SpellPrefab(Clone)");

            if (spellUsed == null)
                continue;

            var spriteUsed = spellUsed.GetComponentInChildren<Image>().sprite;
            alreadyUsedSprites.Add(spriteUsed);
        }

        return alreadyUsedSprites.Contains(SpellDragged.GetComponent<Image>().sprite);
    }
}

