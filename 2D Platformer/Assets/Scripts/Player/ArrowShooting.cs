using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShooting : MonoBehaviour
{
    public static ArrowShooting instance;

    public Animator animator;

    public GameObject arrowPrefab;
    public Transform arrowFirePoint;

    public float arrowSpeed = 10f;

    public int initialNumberOfArrows;
    public int currentNumberOfArrows;

    [HideInInspector]
    public bool bowPicked;


    public int delay = 1;
    protected float Timer;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        currentNumberOfArrows = initialNumberOfArrows;
    }

    private void Update()
    {
        BowPicked();
        ChargingBow();

        if(currentNumberOfArrows < 10)
        {
            Timer += Time.deltaTime;

            if(Timer >= delay)
            {
                Timer = 0f;
                currentNumberOfArrows += 1;
            }
        }
    }

    private void BowPicked()
    {
        if (Input.GetButtonDown("Bow Picked") && !PlayerController.instance.isMoving && currentNumberOfArrows > 0)
        {
            animator.SetBool("BowPicked", true);
            bowPicked = true;
        }

        if (Input.GetButtonUp("Bow Picked"))
        {
            animator.SetBool("BowPicked", false);
            bowPicked = false;
        }
    }

    private void ChargingBow()
    {
        if(bowPicked && Input.GetButtonDown("Fire Arrow") && currentNumberOfArrows > 0)
        {
            animator.SetBool("isChargingBow", true);
        }

        if(bowPicked && Input.GetButtonUp("Fire Arrow"))
        {
            animator.SetBool("isChargingBow", false);
            animator.SetBool("isFiringArrow", true);
        }
    }

    public void FiringArrow()
    {
        animator.SetBool("isFiringArrow", false);
        GameObject arrow = Instantiate(arrowPrefab, arrowFirePoint.position, arrowFirePoint.rotation);
        arrow.GetComponent<Rigidbody2D>().velocity = arrowFirePoint.right * arrowSpeed;

        currentNumberOfArrows--;
    }

    public void BowChargeSFX()
    {
        AudioManager.instance.Play("Bow Charge");
    }

    public void ReleaseArrowSFX()
    {
        AudioManager.instance.Play("Release Arrow");
    }
}
