using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scenes.SpiritMountain.Scripts
{
	public class SpiritMountainGameManager : MonoBehaviour
	{
		public List<GameObject> SpellCards;
		public SpellScriptableObject[] ScriptableObjects;
		public GameObject Prefab;
		public Transform SpellMenuTransform;
		public Transform CanvasTransform;

		public void Start()
		{
			SpellCards = new List<GameObject>();
			foreach (var spellScriptableObject in ScriptableObjects)
			{
				SpellCards.Add(CreateSpell(spellScriptableObject));
			}
		}
    
		private GameObject CreateSpell(SpellScriptableObject spellScriptableObject)
		{
			var spell = Instantiate(Prefab, SpellMenuTransform);       
			spell.GetComponentInChildren<Image>().sprite = spellScriptableObject.Sprite;
			spell.GetComponentInChildren<Image>().type = Image.Type.Filled;

			var manager = spell.GetComponent<SpellCardManager>();
			manager.Sprite = spellScriptableObject.Sprite;
			manager.CanvasTransform = CanvasTransform;

			return spell;
		}

	}
}
