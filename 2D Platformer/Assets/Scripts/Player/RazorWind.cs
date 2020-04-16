using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RazorWind : MonoBehaviour
{
    [SerializeField]
    private float attackDamage;

    public LayerMask whatToHit;
    public LayerMask whatIsDamageable;

    private float[] attackDetails = new float[2];

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
            attackDetails[0] = attackDamage;
            attackDetails[1] = transform.position.x;

            collision.transform.parent.SendMessage("Damage", attackDetails);
        }
    }
}
