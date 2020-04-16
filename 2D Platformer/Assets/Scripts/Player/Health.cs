using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public static Health instance;

    public GameObject deathChunkParticles;
    public GameObject deathBloodParticles;

    public HealthBar healthBar;

    public int maxHealth = 100;
    [SerializeField]
    private int currentHealth;

    public GameManager GM;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        //healthBar = GameObject.Find("Health bar").GetComponent<HealthBar>();
        //GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Start()
    {
        currentHealth = maxHealth;

        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        if (currentHealth <= 0.0f)
        {
            Die();
        }
    }
    public void Respawn()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);
        healthBar.SetHealth(currentHealth);
    }

    private void Die()
    {
        Instantiate(deathChunkParticles, transform.position, deathChunkParticles.transform.rotation);
        Instantiate(deathBloodParticles, transform.position, deathBloodParticles.transform.rotation);
        gameObject.SetActive(false);
        GM.Respawn();
        //Destroy(gameObject);
    }
}