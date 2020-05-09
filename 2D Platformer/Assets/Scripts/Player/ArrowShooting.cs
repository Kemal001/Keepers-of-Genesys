using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArrowShooting : MonoBehaviour
{
    public static ArrowShooting instance;

    public Animator animator;
    public Animator uiAnimator;

    public GameObject arrowPrefab;
    public GameObject fireArrowPrefab;
    public GameObject currentArrow;
    public ParticleSystem fireArrowParticles;
    public ParticleSystem fireArrowUIParticles;
    public Transform arrowFirePoint;

    public float arrowSpeed = 10f;

    public int initialNumberOfArrows;
    public int currentNumberOfArrows;

    [HideInInspector]
    public bool bowPicked;


    public int delay = 1;
    protected float Timer;

    [Space]
    [Header("UI Text")]
    public TextMeshProUGUI arrowUIText;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        currentNumberOfArrows = initialNumberOfArrows;
        currentArrow = arrowPrefab;
    }

    private void Update()
    {
        ChangeArrows();
        BowPicked();
        ChargingBow();

        //if(currentNumberOfArrows < 10)
        //{
        //    Timer += Time.deltaTime;

        //    if(Timer >= delay)
        //    {
        //        Timer = 0f;
        //        currentNumberOfArrows += 1;
        //    }
        //}

        if(currentArrow == arrowPrefab)
        {
            fireArrowParticles.Stop();
        }

        arrowUIText.text = currentNumberOfArrows.ToString();
    }

    private void ChangeArrows()
    {
        if((Input.GetButtonDown("Change Arrow") || Input.GetAxisRaw("Change Arrow") > 0) && currentArrow == arrowPrefab)
        {
            currentArrow = fireArrowPrefab;
            fireArrowUIParticles.Play();
            AudioManager.instance.Play("Switch Arrows");
        }
        else if((Input.GetButtonDown("Change Arrow") || Input.GetAxisRaw("Change Arrow") > 0) && currentArrow == fireArrowPrefab)
        {
            currentArrow = arrowPrefab;
            fireArrowUIParticles.Stop();
            AudioManager.instance.Play("Switch Arrows");
        }
    }

    public void PlayFireArrowParticles()
    {
        if (currentArrow == fireArrowPrefab)
        {
            fireArrowParticles.Play();
        }
    }

    public void StopFireArrowParticles()
    {
        fireArrowParticles.Stop();
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

        if(Input.GetButtonDown("Bow Picked") && currentNumberOfArrows <= 0)
        {
            uiAnimator.SetTrigger("outOfArrows");
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

        if (bowPicked && Input.GetButtonDown("Fire Arrow") && currentNumberOfArrows <= 0)
        {
            uiAnimator.SetTrigger("outOfArrows");
        }
    }

    public void FiringArrow()
    {
        animator.SetBool("isFiringArrow", false);
        GameObject arrow = Instantiate(currentArrow, arrowFirePoint.position, arrowFirePoint.rotation);
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
