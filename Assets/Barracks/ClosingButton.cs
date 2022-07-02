using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosingButton : MonoBehaviour
{
    public void CloseTab()
    {
        Destroy(transform.parent.transform.parent.gameObject);
    }
}
