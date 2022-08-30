using Assets.Common;
using Assets.Scenes.SpiritMountain.Scripts;
using Assets.Units.Enemies.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Map.Scripts
{
	public class UseSpell : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
	{
		//create and drag spell
		public SpellScriptableObject SpellScriptableObject;
		GameObject _spellDragged;
		public Sprite Sprite;
		public GameObject SpellPrefab;

		//move over field and drop
		public bool IsOverCollider = false;
		public FieldManager Collider;
		private FieldManager _lastCollider;

		public void OnPointerDown(PointerEventData eventData)
		{
			_spellDragged = Instantiate(SpellPrefab, transform.position, Quaternion.identity); 
			_spellDragged.GetComponent<SpriteRenderer>().sprite = Sprite;       
		}
  
		public void OnDrag(PointerEventData eventData)
		{
			if (_lastCollider != Collider || _lastCollider == null)
			{
				IsOverCollider = false;

				if (_lastCollider != null)
					_lastCollider.Unit = null;	
				
				_lastCollider = Collider;
			}

			// If you do not hover above field, Unit should be stick to your mouse pointer.
			if (!IsOverCollider)
			{
				// If you do not hover above field, Unit should be stick to your mouse pointer.
				var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				_spellDragged.transform.position = new Vector3(position.x, position.y, 0);				
			}
			else
			{
				// Unit should be snapped to the field.
				_spellDragged.transform.position = Collider.transform.position; // + new Vector3(0, 0.25f, 0);
				Collider.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.3f);				
			}

            // if (Collider != null && _spellDragged.transform.position != Collider.transform.position)
            //  {
			//	Collider.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0f);
			//}
        }
        public void OnPointerUp(PointerEventData eventData)
		{
			Collider.IsSpellActivated = true;
			Destroy(_spellDragged);

			return;
		}

        private void OnCollisionEnter2D(Collision2D collision)
        {
			//czy collision wyszukuje wszystkie kolizje czy tylko jedn¹/pierwsz¹?
			var enemy = collision.gameObject.GetComponent<EnemyBasic>();

			if (Collider.IsSpellActivated == true)
				return;

			if (enemy != null)
			{
				enemy.Health -= SpellScriptableObject.Damage;
				
				//FindObjectOfType<GameManager>().NumberOfEnemiesLeft--;
				//Destroy(enemy.gameObject);
			}
		}
    }
}
