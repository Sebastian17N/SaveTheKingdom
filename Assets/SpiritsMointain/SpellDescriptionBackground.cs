using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpellDescriptionBackground : MonoBehaviour, IPointerClickHandler
{    
    public void OnPointerClick(PointerEventData eventData)
    {
        Destroy(gameObject);

        var SpiritButton = GameObject.Find("FrozenSpiritButton");
            SpiritButton.transform.Find("SpellMenu").gameObject.SetActive(true);
    }
        
}
