using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyController : MonoBehaviour
{
    private enum State
    {
        Moving,
        Knockback,
        Dead
    }

    private State currentState;

    [SerializeField]
    private float
        groundCheckDistance,
        wallCheckDistance,
        movementSpeed,
        maxHealth,
        knockbackDuration,
        lastTouchDamageTime,
        touchDamageCooldown,
        touchDamage,
        touchDamageWidth,
        touchDamageHeight;

    [SerializeField]
    private Transform
        groundCheck,
        wallCheck,
        touchDamageCheck;
    [SerializeField]
    private LayerMask
        whatIsGround,
        whatIsPlayer;
    [SerializeField]
    private Vector2 knockbackSpeed;
    [SerializeField]
    private GameObject
        hitParticle,
        deathChunkParticle,
        deathBloodParticle;

    [SerializeField]
    private int
        minLootCount = 3,
        maxLootCount = 5;

    [SerializeField]
    private GameObject[] lootDrop;

    private float 
        currentHealth,
        knockbackStartTime;

    private AttackDetails attackDetails;

    private int 
        facingDirection,
        damageDirection;

    private Vector2
        movement,
        touchDamageBotleft,
        touchDamageTopRight;

    private bool
        groundDetected,
        wallDetected;

    private GameObject alive;
    private Rigidbody2D aliveRb;
    private Animator aliveAnim;

    private void Start()
    {
        alive = transform.Find("Alive").gameObject;
        aliveRb = alive.GetComponent<Rigidbody2D>();
        aliveAnim = alive.GetComponent<Animator>();

        currentHealth = maxHealth;
        facingDirection = 1;
    }

    private void Update()
    {
        switch(currentState)
        {
            case State.Moving:
                UpdateMovingState();
                break;
            case State.Knockback:
                UpdateKnockbackState();
                break;
            case State.Dead:
                UpdateDeadState();
                break;
        }
    }

    //--WALKING STATE----------------------------------------------------------------------------------------------------------------

    private void EnterMovingState()
    {

    }

    private void UpdateMovingState()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);

        //CheckTouchDamage();

        if(!groundDetected || wallDetected)
        {
            Flip();
        }
        else
        {
            movement.Set(movementSpeed * facingDirection, aliveRb.velocity.y);
            aliveRb.velocity = movement;
        }
    }

    private void ExitMovingState()
    {

    }

    //--KNOCKBACK STATE---------------------------------------------------------------------------------------------------------------

    private void EnterKnockbackState()
    {
        knockbackStartTime = Time.time;
        movement.Set(knockbackSpeed.x * damageDirection, knockbackSpeed.y);
        aliveRb.velocity = movement;
        aliveAnim.SetBool("Knockback", true);
    }

    private void UpdateKnockbackState()
    {
        if (Time.time >= knockbackStartTime + knockbackDuration)
        {
            SwitchState(State.Moving);
        }
    }

    private void ExitKnockbackState()
    {
        aliveAnim.SetBool("Knockback", false);
    }

    //--DEAD STATE---------------------------------------------------------------------------------------------------------------------

    private void EnterDeadState()
    {
        Instantiate(deathChunkParticle, alive.transform.position, deathChunkParticle.transform.rotation);
        Instantiate(deathBloodParticle, alive.transform.position, deathBloodParticle.transform.rotation);

        AudioManager.instance.Play("Monster Death");
        AudioManager.instance.Play("Coins Drop");

        Vector2 direction = new Vector2((float)Random.Range(-5, 5), (float)Random.Range(10, 10));
        float force = Random.Range(30, 50);

        int count = Random.Range(minLootCount, maxLootCount);
        for (int i = 0; i < count; i++)
        {
            int lootIndex = Random.Range(0, lootDrop.Length);
            GameObject collectables = Instantiate(lootDrop[lootIndex], alive.transform.position, lootDrop[lootIndex].transform.rotation);
            collectables.GetComponent<Rigidbody2D>().AddForce(direction * force);
        }

        Destroy(gameObject);
    }

    private void UpdateDeadState()
    {

    }

    private void ExitDeadState()
    {

    }

    //--OTHER FUNCTIONS-----------------------------------------------------------------------------------------------------------------

    private void Damage(AttackDetails attackDetails)
    {
        currentHealth -= attackDetails.damageAmount;

        Instantiate(hitParticle, alive.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));

        if(attackDetails.position.x > alive.transform.position.x)
        {
            damageDirection = -1;
        }
        else
        {
            damageDirection = 1;
        }

        //Hit particle

        if(currentHealth > 0.0f)
        {
            SwitchState(State.Knockback);
        }
        else if(currentHealth <= 0.0f)
        {
            SwitchState(State.Dead);
        }
    }

    //private void CheckTouchDamage()
    //{
    //    if(Time.time >= lastTouchDamageTime + touchDamageCooldown)
    //    {
    //        touchDamageBotleft.Set(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2));
    //        touchDamageTopRight.Set(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));

    //        Collider2D hit = Physics2D.OverlapArea(touchDamageBotleft, touchDamageTopRight, whatIsPlayer);

    //        if(hit != null)
    //        {
    //            lastTouchDamageTime = Time.time;
    //            attackDetails[0] = touchDamage;
    //            attackDetails[1] = alive.transform.position.x;
    //            hit.SendMessage("Damage", attackDetails);
    //        }
    //    }
    //}

    private void Flip()
    {
        facingDirection *= -1;
        alive.transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void SwitchState(State state)
    {
        switch(currentState)
        {
            case State.Moving:
                ExitMovingState();
                break;
            case State.Knockback:
                ExitKnockbackState();
                break;
            case State.Dead:
                ExitDeadState();
                break;
        }

        switch (state)
        {
            case State.Moving:
                EnterMovingState();
                break;
            case State.Knockback:
                EnterKnockbackState();
                break;
            case State.Dead:
                EnterDeadState();
                break;
        }

        currentState = state;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }
}
