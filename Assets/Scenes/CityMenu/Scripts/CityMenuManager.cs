using Assets.Common.JsonModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityMenuManager : MonoBehaviour
{
    public GameObject ScriptableObjectManager;
    private void Awake()
    {
        Instantiate(ScriptableObjectManager);
    }
    void Start()
    {
        PlayerPreferences.LogGatherAchievements(1, Assets.Scenes.Quests.Scripts.QuestType.LogInEveryDay5); 
    }
}
