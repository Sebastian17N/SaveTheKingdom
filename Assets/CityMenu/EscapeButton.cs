using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeButton : MonoBehaviour
{
    public GameObject MenuToClose;
    private void OnMouseDown()
    {        
        DestroyImmediate(MenuToClose);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
