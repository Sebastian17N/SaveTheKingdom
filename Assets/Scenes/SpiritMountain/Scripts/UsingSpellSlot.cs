using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scenes.SpiritMountain.Scripts
{
	public class UsingSpellSlot : MonoBehaviour, IDropHandler
	{
		public void OnDrop(PointerEventData eventData)
		{
			if (eventData.pointerEnter != null)
			{
				eventData.pointerEnter.GetComponent<RectTransform>().anchoredPosition =
					GetComponent<RectTransform>().anchoredPosition;
			}

			transform.Find("SpellPrefab(Clone)").transform.localScale = new Vector3(2f, 2f, 2f);
		}
	}
}