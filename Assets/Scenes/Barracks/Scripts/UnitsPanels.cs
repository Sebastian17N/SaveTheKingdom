using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsPanels : MonoBehaviour
{
    //move panels
    //public Transform targetNavPoint;
    //public float speedFactor;

    //Expand center panel
    private GameObject _lastPanel;
    public string FirstPanelToExpand;
    public Transform AllPanels;
    public float NewWidth;

    void Start()
    {
        ExpandPanel(AllPanels.Find("NavigationPoint3/LaganatPanel").gameObject);
        //ExpandPanel(AllPanels.Find("NavigationPoint3/LaganatScrollView/Image/LaganatUnitSlot").gameObject);
    }

    public void MovePanelsToLeft()
    {       
        // Jeœli na pierwszym z lewej miejscu jest obiekt, to oznacza, ¿e nie mo¿emy przesunaæ tego bardziej.
        if (transform.GetChild(0).childCount == 1)
            return;

        // Jeœli mamy 5 slotów, to przesuniemy 4 elementy.
        // o x x x o
        // o o x x x
        // o o o o x x x
        // o x x x x x o
        for (var i = 1; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).childCount == 0)
                continue;

            transform.GetChild(i).GetChild(0).SetParent(transform.GetChild(i - 1), false);
           
            if (transform.GetChild(2).childCount > 0)
                ExpandPanel(transform.GetChild(2).GetChild(0).gameObject);
        }
    }
    public void MovePanelsToRight()
    {
        if (transform.GetChild(4).childCount == 1)
            return;

        for (var i = transform.childCount - 1; i >= 0; i--)
        {
            if (transform.GetChild(i).childCount == 0)
                continue;

            transform.GetChild(i).GetChild(0).SetParent(transform.GetChild(i + 1), false);
           
            if(transform.GetChild(2).childCount > 0 )
                 ExpandPanel(transform.GetChild(2).GetChild(0).gameObject);
        }
    }
    public void ExpandPanel(GameObject panel)
    {
       
        if (_lastPanel != null)
        {
            _lastPanel.transform.localScale = new Vector3(10, 15, -2);
            panel.transform.localPosition = new Vector3(0, 0, 0);
            //_lastPenel.GetComponent<StoreButton>().CloseDetails();
        }

        _lastPanel = panel;
        panel.transform.localScale = new Vector3(NewWidth, 15, 1);
        //panel.transform.localPosition = new Vector3(0, -250, 0);
        //panel.GetComponentInChildren<StoreButton>().ShowDetails();
    }
}
