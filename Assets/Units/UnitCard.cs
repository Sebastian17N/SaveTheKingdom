using UnityEngine;
using UnityEngine.EventSystems;

public class UnitCard : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
	public GameObject Dragged;
	public GameObject Unit;

	public Canvas Canvas;

	private GameObject _unitInstance;
	private GameManager _gameManager;

	public void Start()
	{
		_gameManager = GameObject.FindObjectOfType<GameManager>();
	}

	public void OnDrag(PointerEventData eventData)
	{
		_unitInstance.transform.position = Input.mousePosition;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		_unitInstance = Instantiate(Dragged, Canvas.transform);
		_unitInstance.transform.position = Input.mousePosition;

		_gameManager.DraggingObject = _unitInstance;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		_gameManager.PlaceUnitOnBattlefield(this);

		_gameManager.DraggingObject = null;
		Destroy(_unitInstance);
	}
}
