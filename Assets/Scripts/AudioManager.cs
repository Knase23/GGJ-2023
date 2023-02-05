using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum AudioType
{
    MusicAndAmbient,
    SFX,
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance = null;

    [SerializeField] private AudioMixer _audioMixer = null;

    [SerializeField] private AudioSource _musicSource = null;
    [SerializeField] private AudioSource _ambientSource = null;
    [SerializeField] private AudioSource _sfxSource = null;

    public AudioSource SfxSource { get => _sfxSource; set => _sfxSource = value; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    public void PlayOneShotSFX(AudioClip audioclip, float volume = 1, float pitch = 1)
    {
        _sfxSource.pitch=pitch;
        _sfxSource.volume= volume;
        _sfxSource.PlayOneShot(audioclip);
    }

    public void PlayOneShotAmbience(AudioClip audioclip, float volume = 1, float pitch = 1)
    {
        _ambientSource.pitch = pitch;
        _ambientSource.volume = volume;
        _ambientSource.PlayOneShot(audioclip);
    }

    public void PlayOneShotMusic(AudioClip audioclip) // not using pitch and volume as that would mess up regular looping music
    {
        _musicSource.PlayOneShot(audioclip);
    }
}
