using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathLine : MonoBehaviour
{
    public int damageToDeal = 100;
    private bool damageDealed;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !damageDealed)
        {

            Debug.Log(collision.gameObject.name);
            Health.instance.TakeDamage(damageToDeal);
            damageDealed = true;
        }
    }
    private void LateUpdate()
    {
        damageDealed = false;
    }
}
