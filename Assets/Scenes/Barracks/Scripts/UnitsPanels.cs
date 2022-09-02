using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class UnitsPanels : MonoBehaviour
{    
    //Expand center panel
    private GameObject _lastPanel;
    //public string FirstPanelToExpand;
    public Transform AllPanels;
    public float NewWidth;

    void Start()
    {
        ExpandPanel(AllPanels.Find("NavigationPoint3/LaganatPanel").gameObject);        
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
            _lastPanel.transform.GetChild(0).GetComponentInChildren<SpriteRenderer>().enabled = true;
        }

        _lastPanel = panel;
        panel.transform.localScale = new Vector3(NewWidth, 15, 1);
        panel.transform.GetChild(0).GetComponentInChildren<SpriteRenderer>().enabled = false;    

        ShowOnePanelOnly(panel.name);
    }

    private void ShowOnePanelOnly(string panelName)
    {
	    GameObject.Find("GhagarScrollView")?.SetActive(false);
        GameObject.Find("LaganatScrollView")?.SetActive(false);
        GameObject.Find("HaikoScrollView")?.SetActive(false);
        
        var originName =
	        Regex.Replace(panelName, "([A-Z])", " $1", RegexOptions.Compiled)
		        .Trim()
		        .Split(' ')
		        .First();

        var canvas = GameObject.Find("Canvas");
	    var scrollView = canvas.transform.Find($"{originName}ScrollView");
        scrollView.gameObject.SetActive(true);
    }
}
