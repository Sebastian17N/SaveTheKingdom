using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EscapeButton : MonoBehaviour
{
    public Button Escape;
    void Start()
    {
        Escape.onClick.AddListener(CloseMenu);
    }

    private void CloseMenu()
    {
        GetComponentInParent<CityGameManager>().SetActive(true);
        Destroy(transform.parent.transform.parent.gameObject);
    }      
}
