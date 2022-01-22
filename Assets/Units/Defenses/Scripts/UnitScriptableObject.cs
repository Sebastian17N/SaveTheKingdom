using UnityEngine;

[CreateAssetMenu(menuName = "Units/Unit Card", fileName = "New Unit Card")]
public class UnitScriptableObject : ScriptableObject
{
    public Texture Icon;
    public Sprite Sprite;
    public int Cost;
}
