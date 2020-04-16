using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForTriggersAndCollision : MonoBehaviour
{
    public static CheckForTriggersAndCollision instance;

    private AreaEffector2D windArea;

    private void Update()
    {
        if (GameManager.instance.sceneName == "Level4")
        {
            windArea = GameObject.Find("WindArea").GetComponent<AreaEffector2D>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("AddWindForce"))
        {
            windArea.forceMagnitude = 50f;
        }

        if(collision.CompareTag("DecreaseWindForce"))
        {
            windArea.forceMagnitude = 10f;
        }

        if(collision.CompareTag("Deadly Obstacle"))
        {
            AudioManager.instance.Play("Death By Obstacles");
            Health.instance.TakeDamage(100);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Spikes"))
        {
            Health.instance.TakeDamage(100);
        }
    }
}
