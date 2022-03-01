using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangingBuildingsViews : MonoBehaviour
{
    public string BuildingGoIn;
    public bool IsActive = true;

    private void OnMouseDown()
    {        
        if (!IsActive)
            return;
        SceneManager.LoadScene(BuildingGoIn);
    }
}
