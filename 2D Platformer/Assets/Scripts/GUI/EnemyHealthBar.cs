using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Slider slider;
    public Color lowHP;
    public Color highHP;
    public Vector3 offset;

    public void SetHealth(float currentHealth, float maxHealth)
    {
        slider.gameObject.SetActive(currentHealth < maxHealth);
        slider.value = currentHealth;
        slider.maxValue = maxHealth;

        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(lowHP, highHP, slider.normalizedValue);
    }

    private void Update()
    {
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
    }
}
