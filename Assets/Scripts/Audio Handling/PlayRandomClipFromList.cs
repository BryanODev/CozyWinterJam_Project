using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayRandomClipFromList : MonoBehaviour
{
    [SerializeField] private AudioClip[] clipList;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clipList[Random.Range(0, clipList.Length)];
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
