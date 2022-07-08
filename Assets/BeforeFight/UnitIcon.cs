using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitIcon : MonoBehaviour, IPointerClickHandler
{
    public Sprite Sprite;
    public bool IsUnitChecked = false;
    public Transform CanvasTransform;  
    private GameObject UnitSlot;
    public bool IsAssignedToSlotAlready = false;
    private bool _isAlreadyChosen = false;
    public float Speed;

    //public bool IsInfoVisible = false;
    //private float _pointerDownTime;
    //private const float _pointerClickDetailsTop = 0.2f;
       

    void Update()
    {
        AssignedToUnitSlot();
    }
    public void AssignedToUnitSlot()
    {
        if (IsAssignedToSlotAlready)
            return;

        // Unit icon is higher or equal than slot.
        if (UnitSlot != null && Vector3.Distance(transform.position, UnitSlot.transform.position) < 10f)
        {
            IsAssignedToSlotAlready = true;
            transform.SetParent(UnitSlot.transform);
            GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 100);
            GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100);
            GetComponent<RectTransform>().anchoredPosition = new Vector3(40, 70, 0);
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {  
        if (!IsAssignedToSlotAlready && !_isAlreadyChosen)
        {
            PlaceUnitInFirstEmptySlot();
            return;
        }
    }

    protected void PlaceUnitInFirstEmptySlot()
	{
        _isAlreadyChosen = true;
        transform.GetComponent<Image>().color = Color.grey;

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
        animObject.GetComponent<UnitIcon>().UnitSlot = foundSlot;
        animObject.GetComponent<Image>().color = Color.white;
        animObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 100);
        animObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100);
        animObject.transform.position = transform.position;        

        var destination = foundSlot.transform.position - animObject.transform.position;
        animObject.GetComponent<Rigidbody2D>().velocity = destination.normalized * Speed;
    }

    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    _pointerDownTime = Time.time;
    //}

    //   private void ChangeUnitDetailsVisibility()
    //{
    //       if (!IsUnitChecked)
    //       {
    //           transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    //           IsUnitChecked = true;
    //           ChangeVisibilityInfoButton(true);
    //       }
    //       else
    //       {
    //           transform.localScale = new Vector3(1, 1, 1);
    //           IsUnitChecked = false;
    //           ChangeVisibilityInfoButton(false);
    //       }
    //   }

    //public void ChangeVisibilityInfoButton(bool isInfoButtonVisible)
    //{
    //    var infoButton = transform.Find("InfoButton").gameObject;   
    //    infoButton.SetActive(isInfoButtonVisible); 
    //}

    //      if (Time.time - _pointerDownTime < _pointerClickDetailsTop)
    //{
    //          ChangeUnitDetailsVisibility();
    //          return;
    //}
}
