using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FightButton : MonoBehaviour
{
    public string SceneGoIn;
    private void OnMouseDown()
    {
        SceneManager.LoadScene(SceneGoIn);
    }
    
}
