using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class DeadZone : MonoBehaviour
{
    public string SceneName;
    //GameObject Enemy;

    private float _health;
    public float Health 
    {
        get => _health; 
        set
		{
            _health = value;
            PlayerPrefs.SetFloat("Health", Health);
        }
    }
    
    public float BasicHealth;
    //public bool DidGamerWin = false;

    private void Start()
    {
        PlayerPrefs.SetFloat("BasicHealth", BasicHealth);
    }
    void Update()
    {
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
