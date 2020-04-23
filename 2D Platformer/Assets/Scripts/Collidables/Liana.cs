using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liana : MonoBehaviour
{
    public string animationName;

    public AudioSource audioSource;
    public AudioClip[] lianaSound;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            audioSource.clip = lianaSound[Random.Range(0, lianaSound.Length)];
            
            animator.SetTrigger(animationName);

            audioSource.Play();
        }
    }
}
