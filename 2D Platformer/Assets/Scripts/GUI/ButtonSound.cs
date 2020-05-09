using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip buttonHover;
    public AudioClip buttonPress;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void HoverButton()
    {
        audioSource.clip = buttonHover;
        audioSource.Play();
    }

    public void PressButton()
    {
        audioSource.clip = buttonPress;
        audioSource.Play();
    }
}
