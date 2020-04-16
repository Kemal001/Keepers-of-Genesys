using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticPlatform : MonoBehaviour
{
    private PlatformEffector2D effector;
    public float waitTime;

    public bool playerIsOnPlatform = false;

    private void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)/* || Input.GetAxis("VerticalPlatform") != 0)*/ && playerIsOnPlatform)
        {
            if (waitTime <= 0)
            {
                effector.rotationalOffset = 180f;
                GetComponent<BoxCollider2D>().enabled = false;
                waitTime = 0.5f;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }

        StartCoroutine(EnablePlatformCollider());

        if (/*Input.GetButtonDown("JoystickJump") || */Input.GetKeyDown(KeyCode.Space))
        {
            effector.rotationalOffset = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "Player")
        {
            playerIsOnPlatform = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.name == "Player")
        {
            playerIsOnPlatform = false;
        }
    }

    IEnumerator EnablePlatformCollider()
    {
        yield return new WaitForSeconds(0.2f);
        GetComponent<BoxCollider2D>().enabled = true;
        waitTime = 0;
    }
}
