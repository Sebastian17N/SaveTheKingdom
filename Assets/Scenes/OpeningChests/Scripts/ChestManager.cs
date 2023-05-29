using Assets.Common.JsonModel;
using Assets.Scenes.Quests.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestManager : MonoBehaviour
{
    private Animator _animator;
    public ChestClassification ChestClassification;
    public bool _isChestClicked;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayerClickOnChest()
    {
        if (_isChestClicked)
            return;

        _animator.SetTrigger("PlayerClick");
        StartCoroutine(ActivateAwards());
        _isChestClicked = true;
        var openingChestsManager = GameObject.Find("OpeningChestsManager").GetComponent<OpeningChestsManager>();
        openingChestsManager.ActivateReward();
        PlayerPreferences.LogGatherAchievements(1, QuestType.ChestOpened1);
    }

    public IEnumerator ActivateAwards()
    {
        yield return new WaitForSeconds(2);
    }
}
