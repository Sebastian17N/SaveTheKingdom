using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scenes.SpiritMountain.Scripts
{
	public class SpellDescriptionMenu : MonoBehaviour
	{
		public List<GameObject> SpellDescriptionCards;
		public SpellScriptableObject[] ScriptableObjects;
		public GameObject Prefab;
		public Transform CanvasTransform;

		void Start()
		{
			SpellDescriptionCards = new List<GameObject>();
			foreach (var spellDescriptionScriptableObject in ScriptableObjects)
			{
				SpellDescriptionCards.Add(CreateSpell(spellDescriptionScriptableObject));
			}
		}

		public GameObject CreateSpell(SpellScriptableObject spellScriptableObject)
		{
			var spell = Instantiate(Prefab, this.transform);
			spell.GetComponentInChildren<Image>().sprite = spellScriptableObject.Sprite;
			spell.GetComponentInChildren<Image>().type = Image.Type.Filled;

			var manager = spell.GetComponent<SpellDescriptionCardManager>();
			manager.Sprite = spellScriptableObject.Sprite;
			manager.DamageNumber = spellScriptableObject.Damage.ToString();
			manager.CanvasTransform = CanvasTransform;

			return spell;
		}
	}
}