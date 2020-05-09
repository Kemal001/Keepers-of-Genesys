using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    public GameObject healingEffect;
    public float amountOfHealthToAdd;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(Health.instance.currentHealth < 100)
            {
                AudioManager.instance.Play("Healing Sound");
                Instantiate(healingEffect, transform.position, healingEffect.transform.rotation);
                Health.instance.currentHealth += amountOfHealthToAdd;

                if(Health.instance.currentHealth > 100)
                {
                    Health.instance.currentHealth = 100;
                }
            }

            Destroy(gameObject);
        }
    }
}
