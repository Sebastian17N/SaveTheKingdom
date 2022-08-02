using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestController : MonoBehaviour
{   
    Animator animator;
    public GameObject Award;
    public Button ChestButton;
    
    private bool IsChestCliked = false;
    //Animation ChestAnimation;
    //private List<Sprite> _chestBrown;
    //void awake() => _chestBrown = GetComponent<Sprite>().ToList();
    //AnimatorOverrideController

    void Start()
    {
        animator = GetComponent<Animator>();

        //ChestAnimation = GetComponent<Animation>();
        //foreach (var item in ChestBrown)
        //{
        //    ChestAnimation.GetComponent<Image>().sprite = item;
        //}
    }

    void Update()
    {
        
    }    
    public void PlayerClickOnChest()
    {
        if (!IsChestCliked)
        {
            animator.SetTrigger("PlayerClick");
            StartCoroutine(ActivateAwards());
            IsChestCliked = true;
        }
    }
    IEnumerator ActivateAwards()
    {
        yield return new WaitForSeconds(2);
        var award = Instantiate(Award, transform.position, Quaternion.identity);
        award.transform.SetParent(transform);        
    }
    
    
}
