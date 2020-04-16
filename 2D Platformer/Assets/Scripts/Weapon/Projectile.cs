using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float attackDamage;

    public LayerMask whatToHit;
    public LayerMask whatIsDamageable;

    [SerializeField]
    private GameObject hitParticle;
    public Transform hitPosition;

    private float[] attackDetails = new float[2];

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((whatToHit & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
        {
            CameraShake.instance.ShakeElapsedTime = CameraShake.instance.ShakeDuration;
            Instantiate(hitParticle, hitPosition.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
            AudioManager.instance.Play("Explosion");

            Destroy(gameObject);
        }

        if ((whatIsDamageable & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
        {
            CameraShake.instance.ShakeElapsedTime = CameraShake.instance.ShakeDuration;
            //Instantiate(hitParticle, transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
            AudioManager.instance.Play("Explosion");

            attackDetails[0] = attackDamage;
            attackDetails[1] = transform.position.x;

            collision.transform.parent.SendMessage("Damage", attackDetails);

            Destroy(gameObject);
        }
    }
}
