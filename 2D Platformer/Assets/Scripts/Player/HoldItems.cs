using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldItems : MonoBehaviour
{
    public bool grabbed;
    public float distance;
    public float throwForce;

    public Transform itemHolder;

    private Animator animator;

    RaycastHit2D hit;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(Input.GetButtonDown("Interact"))
        {
            if(!grabbed)
            {
                Physics2D.queriesStartInColliders = false;

                hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance);

                if(hit.collider != null && hit.collider.CompareTag("Spear"))
                {
                    animator.SetBool("HoldItem", true);
                    grabbed = true;
                }
            }

        }

        if (grabbed)
        {
            hit.collider.gameObject.transform.position = itemHolder.position;
            hit.collider.gameObject.transform.rotation = itemHolder.rotation;
        }

        if (Input.GetButtonDown("Throw Item") && grabbed)
        {
            animator.SetBool("HoldItem", false);
            animator.SetBool("ThrowItem", true);
        }
                

    }

    public void ThrowItem()
    {
        grabbed = false;
        if (hit.collider.gameObject.GetComponent<Rigidbody2D>() != null)
        {
            hit.collider.gameObject.GetComponent<Rigidbody2D>().velocity = transform.right * throwForce;
        }
    }

    public void FinishThrowAnimation()
    {
        animator.SetBool("ThrowItem", false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * distance);
    }
}
