using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scenes.SpiritMountain
{
	public class SpellDescriptionButton : MonoBehaviour, IPointerClickHandler
	{
		public void OnPointerClick(PointerEventData eventData)
		{
			Destroy(transform.parent.transform.parent.gameObject);
		}
	}
}
