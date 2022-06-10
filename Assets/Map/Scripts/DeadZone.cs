using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class DeadZone : MonoBehaviour
{
    public string SceneName;
    //GameObject Enemy;
    public float Health;
    public float BasicHealth;
    //public bool DidGamerWin = false;
    FightSummaryGameManager FightSummaryGameManager;

    private void Start()
    {
        FightSummaryGameManager = GetComponent<FightSummaryGameManager>();
        PlayerPrefs.SetFloat("BasicHealth", BasicHealth);
    }
    void Update()
    {
        if (Health <= 0)
        {
            PlayerPrefs.SetInt("DidGamerWin", 0);
            PlayerPrefs.SetFloat("Health", Health);
            SceneManager.LoadScene(SceneName);            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemy = collision.gameObject.GetComponent<EnemyBasic>();

        if (enemy != null)
        {
            Health -= enemy.AttackDamage;            
        }   

    }
}
