using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using Tools;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{

    [SerializeField]
    private AudioClip[] _clips;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = Camera.main.GetComponent<AudioSource>();
    }

    public void PlayFieldPlacementSound()
    {
        _audioSource.PlayOneShot(_clips[0]);
    }

    public void PlayMessageSound()
    {
        _audioSource.PlayOneShot(_clips[3]);
    }

    public void PlayKanskaartGoedSound()
    {
        _audioSource.PlayOneShot(_clips[1]);
    }

    public void PlayKanskaartFoutSound()
    {
        _audioSource.PlayOneShot(_clips[2]);
    }


    public void PlayUpgradeSound()
    {
        _audioSource.PlayOneShot(_clips[4]);
    }

    public void FarmPlacementSound()
    {
        _audioSource.PlayOneShot(_clips[5]);
    }
}
