using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scenes.SpiritMountain.Scripts
{
	public class SpellCardManager : MonoBehaviour, IPointerClickHandler
	{        
		public Sprite Sprite;
		public string DamageNumber;
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
			spellDescription.transform.Find("Button/SpellDataDescriptionFolder/Damage/DamageNumber").
				GetComponent<TextMeshProUGUI>().text = DamageNumber;

			SpiritSpellMenu = transform.parent.gameObject;
			SpiritSpellMenu.SetActive(false);
		}
	}
}

