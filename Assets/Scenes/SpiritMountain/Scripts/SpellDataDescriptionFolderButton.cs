using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scenes.SpiritMountain.Scripts
{
	public class SpellDataDescriptionFolderButton : MonoBehaviour
	{
		public SpellScriptableObject[] ScriptableObjects;
		public Sprite Sprite;

		public void Start()
		{
			foreach (var scriptableObject in ScriptableObjects)
			{
				CreateSpellDataDescriptionFolder(scriptableObject);
			}
		}

		private void CreateSpellDataDescriptionFolder(SpellScriptableObject spellScriptableObject)
		{
			transform.Find("SpellPrefab(Clone)")
				.GetComponentInChildren<Image>()
				.sprite = spellScriptableObject.Sprite;
		}
	}
}