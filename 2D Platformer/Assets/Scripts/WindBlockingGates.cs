using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class WindBlockingGates : MonoBehaviour
{
    private Animator animator;

    private CinemachineVirtualCamera CVC;

    private AreaEffector2D windArea;

    private bool focusedOnGates = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        CVC = GameObject.Find("Player Camera").GetComponent<CinemachineVirtualCamera>();
        windArea = GameObject.Find("WindArea").GetComponent<AreaEffector2D>();
        windArea.enabled = false;
    }

    private void Update()
    {
        if(Trigger.instance.triggerOn)
        {
            StartCoroutine(FocusCameraOnGates());
        }
    }

    IEnumerator FocusCameraOnGates()
    {
        Trigger.instance.triggerOn = false;
        CVC.m_Follow = this.transform;
        AudioManager.instance.Play("Earthquake");
        
        yield return new WaitForSecondsRealtime(0.5f);
        animator.SetTrigger("isOpen");
        CameraShake.instance.ShakeElapsedTime = 4.5f;
        windArea.enabled = true;

        yield return new WaitForSecondsRealtime(4.5f);
        CVC.m_Follow = GameObject.FindWithTag("Player").transform;
        
    }
}
