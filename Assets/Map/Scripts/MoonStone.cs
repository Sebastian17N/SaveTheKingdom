using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonStone : MonoBehaviour
{   
    private void OnMouseDown()
    {
        FindObjectOfType<PointsCounter>().IncrementPoints();

        Destroy(gameObject);
    }
}
