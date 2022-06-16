using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestController : MonoBehaviour
{
    Animator animator;
    public GameObject Award;
    public Button ChestButton;
    void Start()
    {
        animator = GetComponent<Animator>();
        ChestButton.onClick.AddListener(PlayerClickOnChest);
    }

    void Update()
    {
        
    }
    //private void OnMouseDown()
    //{
    //    animator.SetTrigger("PlayerClick");
    //    var award = StartCoroutine(ActivateAwards());

        
    //}
    private void PlayerClickOnChest()
    {
        animator.SetTrigger("PlayerClick");
        var award = StartCoroutine(ActivateAwards());
    }
    IEnumerator ActivateAwards()
    {
        yield return new WaitForSeconds(2);
        var award = Instantiate(Award, transform.position, Quaternion.identity);
        award.transform.SetParent(transform);
    }
}
