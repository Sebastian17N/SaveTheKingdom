using System;
using Assets.Common.Enums;
using Assets.Common.JsonModel;
using Assets.Common.Models;
using TMPro;
using UnityEngine;

public class ResourcesController : MonoBehaviour
{
	public int Quantity;
	public RewardType Type;

	public ResourcesController(string resourcesType, int quantity)
	{
		Quantity = quantity;
	}

	private void Start()
	{
		AssignmentResources();
		ShowResources($"{Type}");
	}

	public void AssignmentResources()
	{
		Quantity = PlayerPreferences.LoadResourceByType(Type);
	}

	public void ShowResources(string resourcesName)
	{
		AssignmentResources();
		transform.Find("NumberText").GetComponent<TMP_Text>().text = Quantity.ToString();
	}

	public void IncrementResources(int resourcesToAdd, string resourcesType)
	{
		Quantity = PlayerPreferences.LoadResourceByType(resourcesType) + resourcesToAdd;

		PlayerPreferences.Load().AddReward =
			new Reward { Type = Enum.Parse<RewardType>(resourcesType), Amount = resourcesToAdd };

		ShowResources(resourcesType);
	}

	public void DecrementResources(int resourcesToRemove, string resourcesType)
	{
		IncrementResources(-resourcesToRemove, resourcesType);
	}
}