using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class DeadZone : MonoBehaviour
{
    public string LevelName = "Menu";
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyBasic>() == null)
            return;

        SceneManager.LoadScene(LevelName);
    }
}
