using Assets.Scenes.Battleground.Units.Defenses.Scripts;
using Assets.Units.Weapons;
using UnityEngine;

namespace Assets.Units.Defenses.Scripts
{
	[CreateAssetMenu(fileName = "UnitCard_", menuName = "Scriptable Objects/Unit Card")]
	public class UnitScriptableObject : ScriptableObject
	{
		public int UnitId;
		[Header("Visuals")]
		public string Name;
		public Sprite Sprite;
		public RuntimeAnimatorController Animator; 
		public RuntimeAnimatorController AnimatorCanvas;
		public Sprite Icon;
		public bool Unlocked = false;

		[Header("Statistics")]
		public float BatlegroundCost;

		public float HealthBasic;
		public float HealthUpgrade;
		public float Health;

		public float AttackDamageBasic;
		public float AttackDamageUpgrade;
		public float AttackDamage;
		public float AttackSpeed;

		public float Speed;
		public float Cooldown;
		public bool IsRange => BulletType?.Sprite != null;
		public BulletType BulletType;
		public UnitOrigin Origin;
		public UnitClassification Classification;
		public float Level;
		public float ShardsNeededToUpgrade;
        public float UpgradeCoinCost;
		public float UpgradeGemsCost;

		public float ShardCostOfUpgradeBasedOnClassification()
		{
			if (Classification == UnitClassification.Common)
			{
				ShardsNeededToUpgrade = 10;
            }
			else if (Classification == UnitClassification.Epic)
			{
                ShardsNeededToUpgrade = 30;
            }
            else if (Classification == UnitClassification.Legandary)
            {
                ShardsNeededToUpgrade = 50;
            }

			return ShardsNeededToUpgrade;
        }
    }
}
