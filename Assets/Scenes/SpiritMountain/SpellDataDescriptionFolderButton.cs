using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scenes.SpiritMountain
{
	public class SpellDataDescriptionFolderButton : MonoBehaviour
	{    
		public SpellScriptableObject[] ScriptableObjects;    
		public Sprite Sprite;

		void Start()
		{
			foreach (SpellScriptableObject scriptableObject in ScriptableObjects)
			{
				CreateSpellDataDescriptionFolder(scriptableObject);
			}
		}
		private void CreateSpellDataDescriptionFolder(SpellScriptableObject spellScriptableObject)
		{
			var SpellFolder = transform.Find("SpellPrefab(Clone)");
			SpellFolder.GetComponentInChildren<Image>().sprite = spellScriptableObject.Sprite;
		}
	}
}
