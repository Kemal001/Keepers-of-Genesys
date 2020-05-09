using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RazorWindCooldownUI : MonoBehaviour
{
    public static RazorWindCooldownUI instance;

    public Image cooldownImage;
    public float cooldownTime;
    [HideInInspector]
    public bool isCooldown;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Razor Wind"))
        {
            isCooldown = true;
        }

        if (isCooldown)
        {
            cooldownImage.fillAmount += 1 / cooldownTime * Time.deltaTime;

            if (cooldownImage.fillAmount >= 1)
            {
                cooldownImage.fillAmount = 0;
                isCooldown = false;
            }
        }
    }
}
