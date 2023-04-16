using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBox : MonoBehaviour
{
    public AudioSource audioSource;
    public bool activeAudio;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (activeAudio && !audioSource.isPlaying)
            audioSource.Play();
    }
}
