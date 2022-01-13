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
		_gameManager = GameManager.Instance;
	}

	public void OnDrag(PointerEventData eventData)
	{
		_unitInstance.transform.position = Input.mousePosition;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		_unitInstance = Instantiate(Dragged, Canvas.transform);
		_unitInstance.transform.position = Input.mousePosition;

		GameManager.Instance.DraggingObject = _unitInstance;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		_gameManager.PlaceUnitOnBattlefield(this);

		GameManager.Instance.DraggingObject = null;
		Destroy(_unitInstance);
	}
}
