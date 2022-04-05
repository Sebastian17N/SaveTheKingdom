using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreController : MonoBehaviour
{
    public static Vector3 newPose;
    public static bool SelectMove;
    public Transform storeContainer;
    public float lerpTime;
    private GameObject _lastSpirit;

    private void Start()
    {
        SelectSpirit(storeContainer.Find("FrozenSpiritButton").gameObject);        
    }
    private void Update()
    {
        if(storeContainer.position != newPose && SelectMove)
        {
            storeContainer.position = Vector3.Lerp(storeContainer.position, newPose, lerpTime * Time.deltaTime);
        }
        if(Vector3.Distance(storeContainer.position, newPose) < 0.1f)
        {
            storeContainer.position = newPose;
            SelectMove = false;
        }
    }

    public void SelectSpirit(GameObject spirit)
    {
        if(_lastSpirit != null)
        {
            _lastSpirit.transform.localScale = new Vector3(1, 1, 1);
            _lastSpirit.GetComponent<StoreButton>().CloseDetails();
        }
        _lastSpirit = spirit;
        spirit.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        spirit.GetComponent<StoreButton>().ShowDetails();
    }
}
