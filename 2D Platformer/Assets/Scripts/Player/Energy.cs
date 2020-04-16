using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    public static Energy instance;

    public EnergyBar energyBar;

    public float minEnergy = 0;
    public float maxEnergy = 100;
    public float currentEnergy;

    private WaitForSeconds regenTick = new WaitForSeconds(0.01f);
    private Coroutine regen;

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
    }

    private void Start()
    {
        currentEnergy = maxEnergy;

        energyBar.SetMinEnergy(maxEnergy);
    }

    private void Update()
    {
        energyBar.SetEnergy(currentEnergy);
        
    }

    public void UseEnergy(float energy)
    {
        if(currentEnergy - energy >= 0)
        {
            currentEnergy -= energy;
            Debug.Log(currentEnergy);
            energyBar.SetEnergy(currentEnergy);

            if (regen != null)
                StopCoroutine(regen);

            regen = StartCoroutine(RegenEnergy());
        }
        else
        {
            Debug.Log("Not enough energy");
        }
    }

    private IEnumerator RegenEnergy()
    {
        yield return new WaitForSeconds(2);

        while (currentEnergy < maxEnergy)
        {
            currentEnergy += 0.3f;
            energyBar.SetEnergy(currentEnergy);
            yield return regenTick;
        }
        regen = null;
    }
}
