using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RazorWindAbility : MonoBehaviour
{
    public GameObject razorWindPrefab;

    public Transform razorWindFirePoint;

    public float razorWindSpeed;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(Input.GetButtonDown("Razor Wind")/* && Energy.instance.currentEnergy >= 100*/)
        {
            animator.SetBool("RazorWind", true);
            //Energy.instance.currentEnergy -= Energy.instance.currentEnergy;
        }
    }

    public void RazorWind()
    {
        GameObject razorWind = Instantiate(razorWindPrefab, razorWindFirePoint.position, razorWindFirePoint.rotation);
        razorWind.GetComponent<Rigidbody2D>().velocity = razorWindFirePoint.right * razorWindSpeed;
    }

    public void FinishAnimation()
    {
        animator.SetBool("RazorWind", false);
    }
}
