using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scenes.SpiritMountain.Scripts
{
	public class SpellDescriptionCardManager : MonoBehaviour, IPointerClickHandler
	{
		public Sprite Sprite;
		public Transform CanvasTransform;
		public GameObject SpellDescriptionPrefab;

		public void OnPointerClick(PointerEventData eventData)
		{
			Destroy(transform.parent.transform.parent.gameObject);

			var spellDescription = Instantiate(SpellDescriptionPrefab);
			spellDescription.transform.SetParent(transform.parent.transform.parent.transform.parent);
			spellDescription.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
			spellDescription.transform.Find("Button/SpellDataDescriptionFolder").GetComponent<Image>().sprite = Sprite;
		}
	}
}