using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySummoner : MonoBehaviour
{
    private Animator animator;
    private GameObject target;

    public GameObject skullPrefab;
    public Transform skullSpawnPoint;

    public int minAmountOfSkulls = 2;
    public int maxAmountOfSkulls = 5;

    public bool facingRight = false;
    public bool playerDetected;

    private void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.Find("Adventurer");

        InvokeRepeating("SummoningAnimation", 1.0f, 1.0f);
    }

    private void Update()
    {
        if (target.transform.position.x < gameObject.transform.position.x && facingRight)
        {
            Flip();
        }

        if (target.transform.position.x > gameObject.transform.position.x && !facingRight)
        {
            Flip();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            playerDetected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerDetected = false;
        }
    }

    private void SummoningAnimation()
    {
        animator.SetBool("summon", true);
    }

    public void SummonSkulls()
    {
        if(playerDetected)
        {
            GameObject skulls = Instantiate(skullPrefab, skullSpawnPoint.position, skullSpawnPoint.rotation);
            Destroy(skulls, 4f);
        }
    }

    public void FinishAnimation()
    {
        animator.SetBool("summon", false);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = gameObject.transform.localScale;
        scale.x *= -1;
        gameObject.transform.localScale = scale;
    }
}
