using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionDoor : MonoBehaviour
{
    public GameObject returnKey;

    public Animator animator;

    public string SceneToLoad;

    private bool triggered;

    private void Update()
    {
        if(triggered && (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Interact")))
        {
            LoadNextScene();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            AudioManager.instance.Play("Openning Door");
            animator.SetBool("DoorIsOpen", true);
            returnKey.SetActive(true);
            triggered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.instance.Play("Closing Door");
            animator.SetBool("DoorIsOpen", false);
            returnKey.SetActive(false);
            triggered = false;
        }
    }

    private void LoadNextScene()
    {
        StartCoroutine(LevelLoader.instance.LoadLevel(SceneToLoad));
    }
}
