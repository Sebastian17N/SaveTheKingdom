using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public string LevelName;
    void Start()
    {
        GetComponentInChildren<TMP_Text>().text = LevelName;
        GetComponent<Button>().onClick.AddListener(ChangeScene);
    }

    void ChangeScene()
    {
        SceneManager.LoadScene("Game");
        PlayerPrefs.SetString("current_level", LevelName);
    }
}