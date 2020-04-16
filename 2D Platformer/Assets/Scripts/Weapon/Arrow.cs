using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField]
    private float attackDamage;

    public LayerMask whatToHit;
    public LayerMask whatIsDamageable;

    [SerializeField]
    private GameObject arrowHitParticle;
    public Transform arrowHitPosition;

    private float[] attackDetails = new float[2];

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((whatToHit & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
        {
            CameraShake.instance.ShakeElapsedTime = CameraShake.instance.ShakeDuration;
            Instantiate(arrowHitParticle, arrowHitPosition.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
            AudioManager.instance.Play("Arrow Hit");

            this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            Destroy(gameObject, 1f);
        }

        if ((whatIsDamageable & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
        {
            CameraShake.instance.ShakeElapsedTime = CameraShake.instance.ShakeDuration;
            //Instantiate(hitParticle, transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
            AudioManager.instance.Play("Arrow Hit Body");

            attackDetails[0] = attackDamage;
            attackDetails[1] = transform.position.x;

            collision.transform.parent.SendMessage("Damage", attackDetails);

            Destroy(gameObject);
        }
    }
}
