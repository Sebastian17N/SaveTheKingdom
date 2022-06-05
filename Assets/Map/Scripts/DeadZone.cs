using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class DeadZone : MonoBehaviour
{
    public string SceneName;
    GameObject Enemy;
    public float Health;
    //public bool DidGamerWin = false;
    FightSummaryGameManager FightSummaryGameManager;
    private void Start()
    {
        FightSummaryGameManager = GetComponent<FightSummaryGameManager>();
    }
    void Update()
    {
        if (Health <= 0)
        {
            //FightSummaryGameManager.DidGamerWin = false;
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
