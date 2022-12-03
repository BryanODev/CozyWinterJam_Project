using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource m_AudioSource;

    [SerializeField] public Dictionary<string, AudioClip> clipsDictionary = new Dictionary<string, AudioClip>();
    
    private void Awake()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(string soundKey) 
    {
        foreach (string key in clipsDictionary.Keys) 
        {
            if (soundKey == key) 
            {
               // m_AudioSource.clip = clipsDictionary[key];
                break;
            }
        }

        m_AudioSource.Play();
    }
}
