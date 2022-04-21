using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public float TimeToChangeScene;
    public string SceneGoIn;
    void Start()
    {
        StartCoroutine(NewScene());
    }

    IEnumerator NewScene()
    {
        yield return new WaitForSeconds(TimeToChangeScene);
        SceneManager.LoadScene(SceneGoIn);
    }
}
