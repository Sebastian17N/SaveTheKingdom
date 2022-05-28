using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpellDescriptionBackground : MonoBehaviour, IPointerClickHandler
{
    //public SpellScriptableObject SpellScriptableObject;
    //public Sprite Sprite;
    //public GameObject SpellPrefab;
    //public Transform CanvasTransform;
    public void OnPointerClick(PointerEventData eventData)
    {
        Destroy(gameObject);
    }
        
}
