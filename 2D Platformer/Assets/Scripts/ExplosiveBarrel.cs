using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    public GameObject explosion;

    [SerializeField]
    private float cameraShakeDuration = 0.3f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fire Arrow"))
        {
            AudioManager.instance.Play("Explosion");
            CameraShake.instance.ShakeElapsedTime = cameraShakeDuration;
            Instantiate(explosion, new Vector2(transform.position.x, transform.position.y + 1f), transform.rotation);
            Destroy(gameObject);
        }
    }
}
