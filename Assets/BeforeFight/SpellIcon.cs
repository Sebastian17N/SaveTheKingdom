using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpellIcon : MonoBehaviour, IPointerClickHandler
{    
    public Sprite Sprite;
    public bool IsSpellChecked = false;
    public Transform CanvasTransform;  
    public bool IsAssignedToSlotAlready = false;
    private GameObject SpellSlot;    
    private bool _isAlreadyChosen = false;
    public float Speed;

    //private float _pointerDownTime;
    //private const float _pointerClickDetailsTop = 0.2f;
    //public GameObject PrefabSpellCard;

    void Update()
    {
        AssignedToSpellSlot();
    }
    public void AssignedToSpellSlot()
    {
        if (IsAssignedToSlotAlready)
            return;

        // Spell icon is higher or equal than slot.
        if (SpellSlot != null && Vector3.Distance(transform.position, SpellSlot.transform.position) < 10f)
        {
            IsAssignedToSlotAlready = true;
            transform.SetParent(SpellSlot.transform);
            GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 60);
            GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 60);
            GetComponent<RectTransform>().anchoredPosition = new Vector3(40, 50, 0);
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!IsAssignedToSlotAlready && !_isAlreadyChosen)
        {
            PlaceSpellInFirstEmptySlot();
            return;
        }
    }

    private void PlaceSpellInFirstEmptySlot()
    {
        _isAlreadyChosen = true;
        transform.GetComponent<Image>().color = Color.grey;

        var spellEmptySlots = GameObject.Find("SpellEmptySlots");
        GameObject foundSlot = null;

        for (var childIndex = 0; childIndex < spellEmptySlots.transform.childCount; ++childIndex)
        {
            var slot = spellEmptySlots.transform.GetChild(childIndex);

            if (slot.Find("SpellIcon(Clone)") != null)
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
        animObject.GetComponent<SpellIcon>().SpellSlot = foundSlot;
        animObject.GetComponent<Image>().color = Color.white;
        animObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 60);
        animObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 60);
        animObject.transform.position = transform.position;

        var destination = foundSlot.transform.position - animObject.transform.position;
        animObject.GetComponent<Rigidbody2D>().velocity = destination.normalized * Speed;
    }

    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    _pointerDownTime = Time.time;

    //    //SpellIconDragged = Instantiate(PrefabSpellCard, new Vector3(1, 1, 1), Quaternion.identity);
    //    //SpellIconDragged.transform.SetParent(transform.parent.transform.parent.transform);
    //    //SpellIconDragged.GetComponent<Image>().sprite = Sprite;
    //}

    //public void OnPointerUp(PointerEventData eventData)
    //{
    //    if (IsAssignedToSlotAlready)
    //        return;

    //    if (Time.time - _pointerDownTime > _pointerClickDetailsTop)
    //    {
    //        var animObject = Instantiate(this.gameObject);
    //        animObject.transform.SetParent(CanvasTransform);
    //        animObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 50);
    //        animObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 50);
    //        animObject.transform.position = transform.position;
    //        var spellEmptySlots = GameObject.Find("SpellEmptySlot").transform.position;
    //        var destination = spellEmptySlots - animObject.transform.position;
    //        animObject.GetComponent<Rigidbody2D>().velocity = destination.normalized * Speed;
    //        Destroy(animObject, 0.25f);

    //        PutNewSpellIntoSlot();
    //        return;
    //    }
    //}

    //private void PutNewSpellIntoSlot()
    //{
    //    var spellEmptySlots = GameObject.Find("SpellEmptySlots");

    //    for (var childIndex = 0; childIndex < spellEmptySlots.transform.childCount; ++childIndex)
    //    {
    //        var slot = spellEmptySlots.transform.GetChild(childIndex);

    //        if (slot.Find("SpellIcon(Clone)") != null)
    //            continue;

    //        var newSpell = Instantiate(this, slot.transform);
    //        newSpell.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 50);
    //        newSpell.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 50);
    //        newSpell.GetComponent<RectTransform>().anchoredPosition = new Vector3(40, 50, 0);
    //        newSpell.name = this.name;
    //        break;
    //    }
    //}
    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    transform.localScale = new Vector3(1, 1, 1);
    //    IsSpellChecked = false;
    //}
}
