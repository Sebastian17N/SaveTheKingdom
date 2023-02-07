using UnityEngine;

[CreateAssetMenu(fileName = "Quest_", menuName = "Scriptable Objects/Quest")]
public class QuestScriptableObject : ScriptableObject
{
    [Header("Quest Details")]
    public string questName;
    public string questDescriptions;
    public int totalPointsRequireToEndQuest;
    public int currentPointsRequireToEndQuest;
    public Sprite rewardIcon; 
    public QuestOrigin origin;
}

public enum QuestOrigin
{
    Daily,
    General
}
