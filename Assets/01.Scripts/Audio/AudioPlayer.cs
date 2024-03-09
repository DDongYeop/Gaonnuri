using System;
using UnityEngine;

public class AudioPlayer : PoolableMono
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public override void Init()
    {
        _audioSource.clip = null;
    }

    public void Setup(AudioClip clip)
    {
        _audioSource.clip = clip;
        _audioSource.Play();
    }
}
