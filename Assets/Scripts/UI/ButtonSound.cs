using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField] AudioClip ButtonDown;
    [SerializeField] AudioClip ButtonUp;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnButtonDown() 
    {
        if (audioSource) 
        {
            if (audioSource.clip != ButtonDown)
            {
                audioSource.clip = ButtonDown;
            }
            audioSource.Play();
        }
    }

    public void OnButtonUp() 
    {
        if (audioSource)
        {
            if (audioSource.clip != ButtonUp)
            {
                audioSource.clip = ButtonUp;
            }

            audioSource.Play();
        }
    }

}
