using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    [Header("Click SFX")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip ButtonDown;
    [SerializeField] AudioClip ButtonUp;

    [Header("Hover SFX")]
    [SerializeField] AudioClip[] ButtonHoverList;

    private void Awake()
    {
        //audioSource = GetComponent<AudioSource>();
    }

    public void OnButtonDown()
    {
        if (audioSource)
        {
            audioSource.PlayOneShot(ButtonDown);
        }
    }

    public void OnButtonUp()
    {
        if (audioSource)
        {
            audioSource.PlayOneShot(ButtonUp);
        }
    }
    public void OnButtonHover()
    {
        if (audioSource)
        {
            audioSource.PlayOneShot(ButtonHoverList[Random.Range(0, ButtonHoverList.Length)]);
        }
    }
}
