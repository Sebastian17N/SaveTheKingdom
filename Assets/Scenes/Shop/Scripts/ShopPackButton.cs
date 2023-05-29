using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopPackButton : MonoBehaviour
{
    public TextMeshProUGUI OfferName;
    public TextMeshProUGUI PackCost;
    public Transform OfferDetailsSpawnPoint;
    public GameObject OfferDetailsImage;
    public GameObject SoldOutCover;
    public bool isOfferSold = false;

    private void Update()
    {
        if (isOfferSold == true)
            SoldOutCover.SetActive(true);
        else if(isOfferSold == false)
            SoldOutCover.SetActive(false);
    }

}
