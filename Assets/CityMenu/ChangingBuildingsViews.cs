using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangingBuildingsViews : MonoBehaviour
{
    public string SceneGoIn;
    public bool IsActive = true;
    public Button ChangingButton;

    private void Start()
    {
        ChangingButton.onClick.AddListener(ChangeScene);
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene(SceneGoIn);
        //transform.parent.GetComponent<CityGameManager>().SetActive(false);
    }
}
