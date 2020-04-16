using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesTrap : MonoBehaviour
{
    public bool playerStepedOnTrapTrigger;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(playerStepedOnTrapTrigger)
        {
            Invoke("SpikesUp", 0.3f);

            Invoke("SpikesDown", 1.5f);
        }
    }

    private void SpikesUp()
    {
        animator.SetBool("SpikesUp", true);
    }

    private void SpikesDown()
    {
        animator.SetBool("SpikesUp", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            playerStepedOnTrapTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerStepedOnTrapTrigger = false;
        }
    }
}
