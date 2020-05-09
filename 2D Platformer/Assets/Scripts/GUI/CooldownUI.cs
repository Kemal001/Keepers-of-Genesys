using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownUI : MonoBehaviour
{
    public Image cooldownImage;
    public float cooldownTime;
    bool isCooldown;

    private void Update()
    {
        if(PlayerController.instance.isDashing)
        {
            isCooldown = true;
        }

        if(isCooldown)
        {
            cooldownImage.fillAmount += 1 / cooldownTime * Time.deltaTime;

            if(cooldownImage.fillAmount >= 1)
            {
                cooldownImage.fillAmount = 0;
                isCooldown = false;
            }
        }
    }
}
