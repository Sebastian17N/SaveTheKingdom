using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesMasterController : MonoBehaviour
{
	public GameObject CoinsNumber;
	public ResourcesController _coinsController => CoinsNumber.transform.GetComponent<ResourcesController>();
	public GameObject SapphireNumber;
	private ResourcesController _sapphireController => SapphireNumber.transform.GetComponent<ResourcesController>();
	public GameObject TopazNumber;
    private ResourcesController _topazController => TopazNumber.transform.GetComponent<ResourcesController>();
    public GameObject EmeraldNumber;
    private ResourcesController _emeraldController => EmeraldNumber.transform.GetComponent<ResourcesController>();
    public GameObject MoonStoneNumber;
    private ResourcesController _moonStoneController => MoonStoneNumber.transform.GetComponent<ResourcesController>();
   
}
