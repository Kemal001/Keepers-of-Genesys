using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RazorWind : MonoBehaviour
{
    [SerializeField]
    private float attackDamage;

    public LayerMask whatToHit;
    public LayerMask whatIsDamageable;

    private AttackDetails attackDetails;

    public ParticleSystem particles;

    private void Start()
    {
        particles.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((whatToHit & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
        {
            Destroy(gameObject);
        }

        if ((whatIsDamageable & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
        {
            attackDetails.damageAmount = attackDamage;
            attackDetails.position = transform.position;

            collision.transform.parent.SendMessage("Damage", attackDetails);
        }
    }
}
