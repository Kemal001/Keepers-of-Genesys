using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public static Trigger instance;

    public Sprite triggerOnSprite;
    public Sprite triggerOffSprite;

    private SpriteRenderer triggerSprite;

    public bool isTriggered;
    public bool triggerOn;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        triggerSprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(isTriggered && !triggerOn && Input.GetButtonDown("Interact"))
        {
            triggerSprite.sprite = triggerOnSprite;
            triggerOn = true;
        }
        else if(isTriggered && triggerOn && Input.GetButtonDown("Interact"))
        {
            triggerSprite.sprite = triggerOffSprite;
            triggerOn = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTriggered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTriggered = false;
        }
    }
}
