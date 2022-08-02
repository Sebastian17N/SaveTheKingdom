using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scenes.SpiritMountain.Scripts
{
	public class SpellDescriptionBackground : MonoBehaviour, IPointerClickHandler
	{
		public void OnPointerClick(PointerEventData eventData)
		{
			Destroy(gameObject);

			GameObject.Find("FrozenSpiritButton")
				.transform
				.Find("SpellMenu")
				.gameObject
				.SetActive(true);
		}
	}
}