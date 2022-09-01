using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
		[SerializeField] GameObject _explosion;
        
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
				{
					_lastCollider.Unit = null;
					_lastCollider.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
				}

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
        }
        public void OnPointerUp(PointerEventData eventData)
        {
	        if (Collider == null)
		        return;

			
			Collider.IsSpellActivated = true;
			Collider.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);

			var explosion = Instantiate(_explosion, Collider.transform.position, Quaternion.identity);
			Destroy(explosion, 1);
			Destroy(_spellDragged);

			var fieldCollider = Collider.gameObject.GetComponent<BoxCollider2D>();
			var results = new List<Collider2D>();

			fieldCollider.OverlapCollider(new ContactFilter2D().NoFilter(), results);
			
			foreach (var enemy in 
			         results
				         .Where(obj => obj.gameObject.GetComponent<EnemyBasic>() != null)
				         .Select(obj => obj.gameObject.GetComponent<EnemyBasic>()))
			{
				enemy.DecreaseDurability(SpellScriptableObject.Damage);

			}			
		}

        /// <summary>
        /// Apply or remove the color from fields in a radius.
        /// </summary>
        /// <param name="x">Position X of field.</param>
        /// <param name="y">Position Y of field.</param>
        /// <param name="applyColor">True - apply the color, False - remove the color.</param>
        /// <param name="radius">Radius of spell</param>
        public void ChangeColorOfFieldForSpell(int x, int y, bool applyColor, int radius = 1)
		{
			// (0, 0), (0, 1), (0, 2)
			// (1, 0), (1, 1), (1, 2)
			// (2, 0), (2, 1), (2, 2)
		}
		
	}
}
