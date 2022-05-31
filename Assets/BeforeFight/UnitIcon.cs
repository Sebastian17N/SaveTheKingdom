using UnityEngine;
using UnityEngine.EventSystems;

public class UnitIcon : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{        
    //GameObject UnitIconDragged;
    public Sprite Sprite;
    public bool IsUnitChecked = false;
    public Transform CanvasTransform;
    public bool IsInfoVisible = false;

    private GameObject Slot;
    private bool _isAssignedToSlotAlready = false;
    private bool _isAlreadyChosen = false;

    private float _pointerDownTime;
    private const float _pointerClickDetailsTop = 0.2f;
        
    public float Speed;

    void Start()
    {       
        ChangeVisibilityInfoButton(false);
    }    

    void Update()
	{
        if (_isAssignedToSlotAlready)
            return;

        // Unit icon is higher or equal than slot.
        if (Slot != null && Vector3.Distance(transform.position, Slot.transform.position) < 10f)
		{
            transform.SetParent(Slot.transform);
            GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 100);
            GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100);
            GetComponent<RectTransform>().anchoredPosition = new Vector3(40, 70, 0);
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

            _isAssignedToSlotAlready = true;
        }
	}

    public void OnPointerDown(PointerEventData eventData)
    {
        _pointerDownTime = Time.time;
    }

    public void OnPointerUp(PointerEventData eventData)
	{        
        if (_isAssignedToSlotAlready || _isAlreadyChosen)
            return;

        if (Time.time - _pointerDownTime > _pointerClickDetailsTop)
		{
            _isAlreadyChosen = true;

            var unitEmptySlots = GameObject.Find("UnitEmptySlots");
            GameObject foundSlot = null;
            
            for (var childIndex = 0; childIndex < unitEmptySlots.transform.childCount; ++childIndex)
            {
                var slot = unitEmptySlots.transform.GetChild(childIndex);

                if (slot.Find("UnitIcon(Clone)") != null)
                    continue;

                foundSlot = slot.gameObject;
                break;
            }

            if (foundSlot == null)
            {
                _isAlreadyChosen = false;
                return;
            }

            var animObject = Instantiate(gameObject, CanvasTransform);
            animObject.name = name;
            animObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 100);
            animObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100);
            animObject.transform.position = transform.position;
            animObject.GetComponent<UnitIcon>().Slot = foundSlot;
           
            var destination = foundSlot.transform.position - animObject.transform.position;
            animObject.GetComponent<Rigidbody2D>().velocity = destination.normalized * Speed;
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
