using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementsArrangement : MonoBehaviour
{
    private float TimeToAdMenuElements = 1f;
    void Start()
    {
        StartCoroutine(AdMenuElements());
    }
    IEnumerator AdMenuElements()
    {
        yield return new WaitForSeconds(TimeToAdMenuElements);
        
    }
}
