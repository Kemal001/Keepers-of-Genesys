using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    private AttackDetails attackDetails;

    public EnemyHealthBar healthBar;

    private GameObject alive;
    private Rigidbody2D aliveRb;
    private Animator aliveAnim;

    public GameObject hitParticle;

    public GameObject deathChunkParticle;
    public GameObject deathBloodParticle;

    public GameObject[] lootDrop;
    public int minLootCount;
    public int maxLootCount;

    [SerializeField]
    private float maxHealth;

    public float currentHealth;
    private int facingDirection;
    private int damageDirection;

    private void Start()
    {
        alive = transform.Find("Alive").gameObject;
        aliveRb = alive.GetComponent<Rigidbody2D>();
        aliveAnim = alive.GetComponent<Animator>();

        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth, maxHealth);
        facingDirection = 1;
    }

    private void Update()
    {
        if (currentHealth <= 0.0f)
        {
            Die();
        }
    }

    private void Damage(AttackDetails attackDetails)
    {
        currentHealth -= attackDetails.damageAmount;
        healthBar.SetHealth(currentHealth, maxHealth);
        //Instantiate(hitParticle, alive.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));

        //if (attackDetails.position.x > alive.transform.position.x)
        //{
        //    damageDirection = -1;
        //}
        //else
        //{
        //    damageDirection = 1;
        //}
    }

    private void Die()
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
}
