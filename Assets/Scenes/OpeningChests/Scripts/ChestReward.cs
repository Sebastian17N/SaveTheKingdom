using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestReward : MonoBehaviour
{
    public bool isFlipped = false;
    RectTransform rectTransform;
    public GameObject GameLogo;
    public GameObject RewardImage;
    public float rotacja;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.localScale = Vector3.zero;
    }

    void Update()
    {
        rotacja = rectTransform.rotation.eulerAngles.y;
        RewardRotation();
        ChangeOfVisibleElement();
    }
    private void RewardRotation()
    {
        var targetRotation = GetTargetRotation();
        rectTransform.rotation = Quaternion.Lerp(rectTransform.rotation, targetRotation, Time.deltaTime * 7f);
    }
    private Quaternion GetTargetRotation()
    {
        var rotation = isFlipped ? (Vector3.up * -180f) : Vector3.zero;

        return Quaternion.Euler(rotation);
    }
    public void PlayerClickOnReward()
    {
        isFlipped = true;
    }
    private void ChangeOfVisibleElement()
    {
        var angle = rectTransform.rotation.eulerAngles.y;

        if (angle >= 60f && angle <= 90f)
        {
            GameLogo.SetActive(false);
        }
        if (angle >= 90f && angle <= 120f)
        {
            RewardImage.SetActive(true);
        }

    }
    
}
