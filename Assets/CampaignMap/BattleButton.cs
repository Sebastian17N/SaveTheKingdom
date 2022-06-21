using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleButton : MonoBehaviour//, IPointerClickHandler
{
    public string SceneGoIn;

    private void Start()
    {
        
    }

    private void OnMouseDown()
    {
        SceneManager.LoadScene(SceneGoIn);
    }
}
