using System.Collections.Generic;
using System.Linq;
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
					ChangeColorOfFieldForSpell(_lastCollider.X, _lastCollider.Y, 0, 1);
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
				ChangeColorOfFieldForSpell(Collider.X, Collider.Y, 0.3f, 1);
			}
        }
        public void OnPointerUp(PointerEventData eventData)
        {
			//TODO: When spell is used outside fields it stay on cursor.
			if (Collider == null)
			{
				Destroy(_spellDragged);
				return;
			}

			Collider.IsSpellActivated = true;
			ChangeColorOfFieldForSpell(Collider.X, Collider.Y, 0, 1);
			ActivateSpell(Collider.X, Collider.Y, 1);
				
		}

		/// <summary>
		/// Apply or remove the color from fields in a radius.
		/// </summary>
		/// <param name="x">Position X of field.</param>
		/// <param name="y">Position Y of field.</param>
		/// <param name="colorIntensity">Float value of color intensity</param>
		/// <param name="radius">Radius of spell</param>
		public void ChangeColorOfFieldForSpell(int x, int y, float colorIntensity, int radius = 1)
		{
			var fieldsWithEffect = new List<(int x, int y)>();

			for (var i = -1; i < 2; i++)
				for (var j = -1; j < 2; j++)
					fieldsWithEffect.Add((x + i, y + j));

			var background = GameObject.Find("Background");

			for (var childId = 0; childId < background.transform.childCount; childId++)
			{
				var field = background.transform.GetChild(childId);
				var fieldManager = field.GetComponent<FieldManager>();
				if (fieldsWithEffect.Contains((fieldManager.X, fieldManager.Y)))
                {
					field.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, colorIntensity);
				}
			}
		}
		public void ActivateSpell(int x, int y, int radius = 1)
        {
			var fieldsWithEffect = new List<(int x, int y)>();

			for (var i = -1; i < 2; i++)
				for (var j = -1; j < 2; j++)
					fieldsWithEffect.Add((x + i, y + j));

			var background = GameObject.Find("Background");

			for (var childId = 0; childId < background.transform.childCount; childId++)
			{
				var field = background.transform.GetChild(childId);
				var fieldManager = field.GetComponent<FieldManager>();
				if (fieldsWithEffect.Contains((fieldManager.X, fieldManager.Y)))
				{
                    var explosion = Instantiate(_explosion, Collider.transform.position, Quaternion.identity);
                    Destroy(explosion, 1);
                    Destroy(_spellDragged);

                    var fieldCollider = field.gameObject.GetComponent<BoxCollider2D>();
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
			}
		}
	}
}
