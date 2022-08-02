using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scenes.SpiritMountain.Scripts
{
	public class SpellCardManager : MonoBehaviour, IPointerClickHandler
	{        
		public Sprite Sprite;
		public GameObject SpellDescriptionPrefab;
		public Transform CanvasTransform;
		public GameObject SpiritSpellMenu;

		public void OnPointerClick(PointerEventData eventData)
		{
			var spellDescription = Instantiate(SpellDescriptionPrefab);
			spellDescription.transform.SetParent(CanvasTransform);
			spellDescription.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
			spellDescription.transform.Find("Button/SpellDataDescriptionFolder").
				GetComponent<Image>().sprite = Sprite;

			SpiritSpellMenu = transform.parent.gameObject;
			SpiritSpellMenu.SetActive(false);
		}
	}
}

