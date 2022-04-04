using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpiritsMointain : MonoBehaviour
{
    public string SceneGoIn;
    public Button SpiritsMointainButton;

    void Start()
    {
        SpiritsMointainButton.onClick.AddListener(ChangeScene);
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene(SceneGoIn);
    }
}
