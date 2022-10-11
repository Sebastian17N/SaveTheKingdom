using Assets.Scenes.Battleground.Units.Defenses.Scripts;
using Assets.Units.Weapons;
using UnityEngine;

namespace Assets.Units.Defenses.Scripts
{
	[CreateAssetMenu(menuName = "Units/Unit Card", fileName = "New Unit Card")]
	public class UnitScriptableObject : ScriptableObject
	{
		public int UnitId;
		[Header("Visuals")]
		public string Name;
		public Sprite Sprite;
		public RuntimeAnimatorController Animator;
		public Sprite Icon;

		[Header("Statistics")]
		public float Cost;

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

		//[Header("UpgradeStatistic")]
		public float ShardsNumber;
		public float UpgradeInitialCost;
		//public float UpgradeIncrementalCostFactor; //np 200*5
		//public float IncrementalCostFactor; 
		//public float IncrementalHealthFactor; 
		//public float IncrementalSpeedFactor; 
		//public float IncrementalAttackDamageFactor; 
		//public float IncrementalCooldownFactor;
	}
}
