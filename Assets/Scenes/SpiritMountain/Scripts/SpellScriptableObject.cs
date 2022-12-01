using UnityEngine;

namespace Assets.Scenes.SpiritMountain.Scripts
{
	[CreateAssetMenu(fileName = "SpellCard_", menuName = "Scriptable Objects/Spell Card")]
	public class SpellScriptableObject : ScriptableObject
	{
		public int SpellId;
		public Sprite Sprite;
		public int Cooldown;
		public int Damage;
	}
}
