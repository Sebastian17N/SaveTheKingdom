using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class DeadZone : MonoBehaviour
{
    public string SceneName;
    //GameObject Enemy;
    public float Health { get; set; }
    public float BasicHealth;
    //public bool DidGamerWin = false;

    private void Start()
    {
        PlayerPrefs.SetFloat("BasicHealth", BasicHealth);
    }
    void Update()
    {
        PlayerPrefs.SetFloat("Health", Health);
        if (Health <= 0)
        {
            PlayerPrefs.SetInt("DidGamerWin", 0);
            SceneManager.LoadScene(SceneName);            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemy = collision.gameObject.GetComponent<EnemyBasic>();

        if (enemy != null)
        {
            Health -= enemy.AttackDamage;
            FindObjectOfType<GameManager>().NumberOfEnemiesLeft--;
            Destroy(enemy.gameObject);
        }   

    }
}
