using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestController : MonoBehaviour
{   
    Animator animator;
    public GameObject Award;
    public Button ChestButton;
    public Sprite[] ChestBrown;
    void Start()
    {
        animator = GetComponent<Animator>();
        //foreach (var item in ChestBrown)
        //{
        //    animator.GetComponent<Image>().sprite = item;
        //}
        ChestButton.onClick.AddListener(PlayerClickOnChest);
    }

    void Update()
    {
        
    }    
    public void PlayerClickOnChest()
    {
        animator.SetTrigger("PlayerClick");        
        StartCoroutine(ActivateAwards());
        
    }
    IEnumerator ActivateAwards()
    {
        yield return new WaitForSeconds(2);
        var award = Instantiate(Award, transform.position, Quaternion.identity);
        award.transform.SetParent(transform);        
    }
    public void ActivateButtons()
    {
        GetComponent<FightSummaryGameManager>().ActivateButton();
    }
}
