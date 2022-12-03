using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    [Header("Click SFX")]
    [SerializeField] AudioSource clickAudioSource;
    [SerializeField] AudioClip ButtonDown;
    [SerializeField] AudioClip ButtonUp;

    [Header("Hover SFX")]
    [SerializeField] AudioSource hoverAudioSource;
    [SerializeField] AudioClip[] ButtonHoverList;

    private void Awake()
    {
        //audioSource = GetComponent<AudioSource>();
    }

    public void OnButtonDown()
    {
        if (clickAudioSource)
        {
            if (clickAudioSource.clip != ButtonDown)
            {
                clickAudioSource.clip = ButtonDown;
            }
            clickAudioSource.Play();
        }
    }

    public void OnButtonUp()
    {
        if (clickAudioSource)
        {
            if (clickAudioSource.clip != ButtonUp)
            {
                clickAudioSource.clip = ButtonUp;
            }

            clickAudioSource.Play();
        }
    }
    public void OnButtonHover()
    {
        if (hoverAudioSource)
        {
            hoverAudioSource.clip = ButtonHoverList[Random.Range(0, ButtonHoverList.Length)];
            Debug.Log("I Just played" + hoverAudioSource.clip.name);
            hoverAudioSource.Play();
        }
    }
}
