using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FallingPlatform : MonoBehaviour
{
    Rigidbody2D rb;

    public float resetTimer = 5.0f;

    private Vector3 initialPosition;

    private Quaternion initialRotation;

    private bool resetPlatform;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    private void Update()
    {
        if(resetPlatform)
        {
            transform.position = Vector2.MoveTowards(transform.position, initialPosition, 20f * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, initialRotation, 20f * Time.deltaTime);
        }

        if(transform.position.y == initialPosition.y && transform.rotation == initialRotation)
        {
            resetPlatform = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Deadly Obstacle"))
        {
            //gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && !resetPlatform)
        {
            Invoke("PlatformFall", 0.1f);
        }
    }

    private void PlatformFall()
    {
        rb.isKinematic = false;
        Invoke("ResetPlatform", 5f);
    }

    private void ResetPlatform()
    {
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        resetPlatform = true;
    }
}
