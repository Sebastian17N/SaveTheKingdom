using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeButton : MonoBehaviour
{    
    private void OnMouseDown()
    {
        GetComponentInParent<CityGameManager>().SetActiveButton(true);
        Destroy(transform.parent.gameObject);

    }    
}
