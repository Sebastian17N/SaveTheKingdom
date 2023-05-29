using Assets.Units.Defenses.Scripts;
using Assets.Units.Enemies.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableObjectManager : MonoBehaviour
{
    public List<UnitScriptableObject> UnitsScriptableObjects;
    public List<ScriptableEnemy> EnemiesScriptableObjects;
    private static ScriptableObjectManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
