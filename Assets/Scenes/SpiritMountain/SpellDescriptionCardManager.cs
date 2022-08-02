using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scenes.SpiritMountain
{
	public class SpellDescriptionCardManager : MonoBehaviour, IPointerClickHandler
	{
		public SpellScriptableObject SpellScriptableObject;
		public Sprite Sprite;
		public GameObject SpellPrefab;
		public Transform CanvasTransform;
		public GameObject SpellDescriptionPrefab;
        
		public void OnPointerClick(PointerEventData eventData)
		{        
			Destroy(transform.parent.transform.parent.gameObject);

			var spellDescription = Instantiate(SpellDescriptionPrefab);
			spellDescription.transform.SetParent(
				this.transform.parent.transform.parent.transform.parent);
			spellDescription.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
			spellDescription.transform.Find("Button/SpellDataDescriptionFolder").
				GetComponent<Image>().sprite = Sprite;
		}
	}
}
