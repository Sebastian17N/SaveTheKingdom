using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scenes.SpiritMountain
{
	public class SpellDescriptionBackground : MonoBehaviour, IPointerClickHandler
	{    
		public void OnPointerClick(PointerEventData eventData)
		{
			Destroy(gameObject);

			var SpiritButton = GameObject.Find("FrozenSpiritButton");
			SpiritButton.transform.Find("SpellMenu").gameObject.SetActive(true);
		}
        
	}
}
