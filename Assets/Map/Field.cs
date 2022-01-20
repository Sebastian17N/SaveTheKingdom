using UnityEngine;
using UnityEngine.UI;

public class Field : MonoBehaviour
{
	public bool IsAssigned;
	public GameManager GameManager;
	public Image BackgroundImage;

	public void Start()
	{
		GameManager = GameObject.FindObjectOfType<GameManager>();
	}

	public void OnMouseOver()
	{
		if (GameManager.DraggingObject != null && !IsAssigned)
		{
			GameManager.CurrentContainer = gameObject;
			//BackgroundImage.enabled = true;
		}
	}
	
	public void OnMouseExit()
	{
		GameManager.CurrentContainer = null;
		//BackgroundImage.enabled = false;
	}
}
