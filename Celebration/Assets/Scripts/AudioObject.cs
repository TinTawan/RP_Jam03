using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioObject : MonoBehaviour
{
    AudioSource audioSource;

    /*[SerializeField] */AudioClip clip;
    /*[SerializeField] */float vol = 1f, pitch = 1f;


    public void StartAudio()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.volume = vol;
        audioSource.pitch = pitch;
        audioSource.Play();

    }

    public void SetClip(AudioClip inClip)
    {
        clip = inClip;
    }
    public void SetVolume(float inVol)
    {
        vol = inVol;
    }
    public void SetPitchDelta(float inDelta)
    {
        pitch += inDelta;
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}