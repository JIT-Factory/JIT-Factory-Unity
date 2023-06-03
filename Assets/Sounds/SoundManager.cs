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

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

public void PlaySound(AudioClip clip)
{
    foreach (AudioSource audioSource in audioSources)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = clip;
            audioSource.Play();
            Resources.UnloadUnusedAssets();
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
            Resources.UnloadUnusedAssets();
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
public void PauseSound(AudioClip clip)
{
    foreach (AudioSource audioSource in audioSources)
    {
        if (audioSource.clip == clip && audioSource.isPlaying)
        {
            audioSource.Pause();
            break;
        }
    }
}


}
