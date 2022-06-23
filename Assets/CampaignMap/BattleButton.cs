using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleButton : MonoBehaviour
{
    public string SceneGoIn;
    public bool StageUnlock;
    public string LevelName;

    private void Start()
    {
        var levelFinished = PlayerPrefs.GetInt(LevelName + "_finished", 0) != 0;
        var boxCollider = GetComponent<BoxCollider2D>();
        var spriteRenderer = GetComponent<SpriteRenderer>();
        var stars = GameObject.Find("Stars");
        var padlock = GameObject.Find("Padlock");

        if (!levelFinished)
        {
            boxCollider.enabled = false;
            spriteRenderer.color = Color.black;
            stars.SetActive(false);
        }
        else
        {
            boxCollider.enabled = true;
            spriteRenderer.color = Color.white;            
            padlock.SetActive(false);
            stars.SetActive(true);
        }


    }
    private void OnMouseDown()
    {
        SceneManager.LoadScene(SceneGoIn);
    }    
}
