using UnityEngine;
using UnityEngine.UI;

public class Field : MonoBehaviour
{
	public bool IsAssigned;
	public GameManager GameManager;
	public Image BackgroundImage;

	private void Awake()
	{
		GameManager = GameManager.Instance;
	}

	public void OnTriggerEnter2D(Collider2D collision)
	{
		if (GameManager.DraggingObject != null && !IsAssigned)
		{
			GameManager.CurrentContainer = gameObject;
			BackgroundImage.enabled = true;
		}
	}
	
	public void OnTriggerExit2D(Collider2D collision)
	{
		GameManager.CurrentContainer = null;
		BackgroundImage.enabled = false;
	}
}
