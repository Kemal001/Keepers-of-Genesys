using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAttackAnimations : MonoBehaviour
{
    public static ComboAttackAnimations instance;

    public Animator animator;

    [SerializeField]
    private Transform dashSliceArea;
    [SerializeField]
    private Transform attack1HitBoxPos;
    [SerializeField]
    private LayerMask whatIsDamageable;

    [SerializeField]
    private float dashSliceAreaRadius;

    [SerializeField]
    private int energyToGain;

    [SerializeField]
    private float attack1Radius, attack1Damage, attack2Damage, attack3Damage, dashSliceDamage;

    public int numberOfButtonClicks = 0;

    private float lastClickTime = 0f;
    public float maxComboDelay = 0.9f;

    private float[] attackDetails = new float[2];

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Attack1();
        DashAttack();
    }

    private void DashAttack()
    {
        if(PlayerController.instance.isDashing && PlayerController.instance.isTouchingEnemy)
        {
            Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(dashSliceArea.position, dashSliceAreaRadius, whatIsDamageable);

            attackDetails[0] = dashSliceDamage;
            attackDetails[1] = transform.position.x;

            foreach (Collider2D collider in detectedObjects)
            {
                collider.transform.parent.SendMessage("Damage", attackDetails);
            }
        }
    }

    private void Attack1()
    {
        if (Time.time - lastClickTime > maxComboDelay)
        {
            numberOfButtonClicks = 0;
        }

        if (Input.GetButtonDown("Attack") && !ArrowShooting.instance.bowPicked && PlayerController.instance.isGrounded)
        {
            lastClickTime = Time.time;
            numberOfButtonClicks++;
            numberOfButtonClicks = Mathf.Clamp(numberOfButtonClicks, 1, 3);

            if (numberOfButtonClicks == 1 && !PlayerController.instance.isMoving)
            {
                animator.SetBool("Attack1", true);
                AudioManager.instance.Play("Attack1");
                CheckAttack1HitBox();
            }
        }
    }

    public void Attack2()
    {
        if(numberOfButtonClicks >= 2 && !PlayerController.instance.isMoving)
        {
            animator.SetBool("Attack2", true);
            AudioManager.instance.Play("Attack2");
            CheckAttack2HitBox();
        }
        else
        {
            animator.SetBool("Attack1", false);
            numberOfButtonClicks = 0;
        }
    }

    public void Attack3()
    {
        if (numberOfButtonClicks >= 3 && !PlayerController.instance.isMoving)
        {
            animator.SetBool("Attack3", true);
            AudioManager.instance.Play("Attack3");
            CheckAttack3HitBox();
        }
        else
        {
            animator.SetBool("Attack2", false);
            numberOfButtonClicks = 0;
        }
    }

    public void FinishAttacks()
    {
        animator.SetBool("Attack1", false);
        animator.SetBool("Attack2", false);
        animator.SetBool("Attack3", false);
        numberOfButtonClicks = 0;
    }

    private void CheckAttack1HitBox()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attack1HitBoxPos.position, attack1Radius, whatIsDamageable);

        energyToGain = Random.Range(1, 3);

        attackDetails[0] = attack1Damage;
        attackDetails[1] = transform.position.x;

        foreach (Collider2D collider in detectedObjects)
        {
            collider.transform.parent.SendMessage("Damage", attackDetails);
            //Energy.instance.GainEnergy(energyToGain);
            AudioManager.instance.Play("Attack1HitBody");
        }
    }
    
    private void CheckAttack2HitBox()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attack1HitBoxPos.position, attack1Radius, whatIsDamageable);

        energyToGain = Random.Range(1, 5);

        attackDetails[0] = attack2Damage;
        attackDetails[1] = transform.position.x;

        foreach (Collider2D collider in detectedObjects)
        {
            collider.transform.parent.SendMessage("Damage", attackDetails);
            //Energy.instance.GainEnergy(energyToGain);
            AudioManager.instance.Play("Attack2HitBody");
        }
    }
    
    private void CheckAttack3HitBox()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attack1HitBoxPos.position, attack1Radius, whatIsDamageable);

        energyToGain = Random.Range(3, 7);

        attackDetails[0] = attack3Damage;
        attackDetails[1] = transform.position.x;

        foreach (Collider2D collider in detectedObjects)
        {
            collider.transform.parent.SendMessage("Damage", attackDetails);
            //Energy.instance.GainEnergy(energyToGain);
            AudioManager.instance.Play("Attack3HitBody");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(attack1HitBoxPos.position, attack1Radius);

        Gizmos.DrawWireSphere(dashSliceArea.position, dashSliceAreaRadius);
    }
}
