using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string SceneGoIn;
    void Start()
    {
        StartCoroutine(NewScene());
    }

    IEnumerator NewScene()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneGoIn);
    }
}
