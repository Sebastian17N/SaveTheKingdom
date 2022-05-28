using UnityEngine;
using UnityEngine.EventSystems;

public class UnitIcon : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    //public GameObject PrefabUnitCard;    
    GameObject UnitIconDragged;
    public Sprite Sprite;
    public bool IsUnitChecked = false;
    public Transform CanvasTransform;
    public bool IsInfoVisible = false;

    private bool _isAssignedToSlotAlready = false;

    private float _pointerDownTime;
    private const float _pointerClickDetailsTop = 0.3f;

    void Start()
    {
        ChangeVisibilityInfoButton(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _pointerDownTime = Time.time;
    }

    public void OnPointerUp(PointerEventData eventData)
	{
        // Do not perform any action if unit is already assigned.
        // TODO: add removing unit from a slot if is assigned.
        if (_isAssignedToSlotAlready)
            return;

        if (Time.time - _pointerDownTime > _pointerClickDetailsTop)
		{
            PutNewUnitIntoSlot();
            return;
		}

        ChangeUnitDetailsVisibility();
        
        // 1. Znajd� pusty slot - czy w og�le, taki istnieje.
        // 1.a je�li nie to zako�cz, i zniknij ikonk� - zniszcz obiekt.
        // 2. Przypisz ikon� do pustego slotu, odpal animacj� poruszania, lub przesuwaj ikon� na Update do czasu, a� pozycja ikony nie zr�wna si� z pozycj� slotu.
	}

    private void PutNewUnitIntoSlot()
	{
        var unitEmptySlots = GameObject.Find("UnitEmptySlots");

        for (var childIndex = 0; childIndex < unitEmptySlots.transform.childCount; ++childIndex)
		{
            var slot = unitEmptySlots.transform.GetChild(childIndex);

            if (slot.Find("UnitIcon(Clone)") != null)
                continue;

            var newUnit = Instantiate(this, slot.transform);
            newUnit.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 100);
            newUnit.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100);
            newUnit.GetComponent<RectTransform>().anchoredPosition = new Vector3(40, 70, 0);
            newUnit.name = this.name;
            break;
        }
    }

    private void ChangeUnitDetailsVisibility()
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
