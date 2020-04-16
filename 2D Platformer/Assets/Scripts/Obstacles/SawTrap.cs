using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawTrap : MonoBehaviour
{
    public Transform pos1, pos2;
    public Transform startPos;
    
    public float rotationSpeed;
    public float movingSpeed;

    Vector3 nextPos;

    private void Start()
    {
        nextPos = startPos.position;

        //AudioManager.instance.Play("Circular Saw");
    }

    private void Update()
    {
        if (transform.position == pos1.position)
        {
            nextPos = pos2.position;
        }
        if (transform.position == pos2.position)
        {
            nextPos = pos1.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPos, movingSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 0, rotationSpeed));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Health.instance.TakeDamage(100);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(pos1.position, pos2.position);
    }
}
