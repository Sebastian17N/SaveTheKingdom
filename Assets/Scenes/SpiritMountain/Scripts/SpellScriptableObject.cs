using UnityEngine;

namespace Assets.Scenes.SpiritMountain.Scripts
{
	[CreateAssetMenu(menuName ="Spell Card", fileName ="New Spell Card")]
	public class SpellScriptableObject : ScriptableObject
	{
		public Sprite Sprite;
		public int Cooldown;
		public int Damage;
	}
}