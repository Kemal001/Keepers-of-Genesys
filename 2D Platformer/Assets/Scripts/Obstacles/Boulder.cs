using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Health.instance.TakeDamage(100);
        }
    }

    private void OnBecameVisible()
    {
        Debug.Log("Boulder became visible!");
    }

    private void OnBecameInvisible()
    {
        Debug.Log("Boulder became invisible!");
    }
}
