using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpellDescription : MonoBehaviour, IPointerClickHandler
{
    //public GameObject SpellDescriptionPrefab;

    public void OnPointerClick(PointerEventData eventData)
    {
        Destroy(gameObject);
    }
        
}
