using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingSkull : MonoBehaviour
{
    private GameObject target;

    public ParticleSystem particles;
    
    public float skullSpeed = 5;
    public int damageToDeal = 5;

    public bool facingRight = false;

    private void Start()
    {
        target = GameObject.Find("Adventurer");
        particles.Play();
    }

    private void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, skullSpeed * Time.deltaTime);

            if (target.transform.position.x < gameObject.transform.position.x && facingRight)
            {
                Flip();
            }

            if (target.transform.position.x > gameObject.transform.position.x && !facingRight)
            {
                Flip();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Health.instance.TakeDamage(5);
            Destroy(gameObject);
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = gameObject.transform.localScale;
        scale.x *= -1;
        gameObject.transform.localScale = scale;
    }
}
