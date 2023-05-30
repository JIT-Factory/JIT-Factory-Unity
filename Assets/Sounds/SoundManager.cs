using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    public static SoundManager Instance
    {
        get { return instance; }
    }

    private AudioSource[] audioSources;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        audioSources = GetComponentsInChildren<AudioSource>();
    }

    public void PlaySound(AudioClip clip)
    {
        foreach (AudioSource audioSource in audioSources)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = clip;
                audioSource.Play();
                break;
            }
        }
    }

    public void StopSound(AudioClip clip)
    {
        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource.clip == clip)
            {
                audioSource.Stop();
                break;
            }
        }
    }

    public bool IsPlaying(AudioClip clip)
    {
        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource.clip == clip && audioSource.isPlaying)
            {
                return true;
            }
        }
        return false;
    }
}
