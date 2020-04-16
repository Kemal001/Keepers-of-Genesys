using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFXHandler : MonoBehaviour
{
    public void RLFootstepSFX()
    {
        AudioManager.instance.Play("RLFootstep");
    }

    public void LLFootstepSFX()
    {
        AudioManager.instance.Play("LLFootstep");
    }
}
