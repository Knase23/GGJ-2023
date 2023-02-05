using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GD.MinMaxSlider;

/// <summary>
/// inspired by Richard Fines talk about scriptableObjects https://www.youtube.com/watch?v=6vmRwLYWNRo
/// </summary>
[CreateAssetMenu(menuName ="Scriptable Objects/Audio/Cue")]
public class AudioCue : ScriptableObject
{
    [SerializeField] private List<AudioClip> _audioClips = new List<AudioClip>();

    [MinMaxSlider(0f,2f)]
    [SerializeField] private Vector2 _volumeRange = Vector2.one;
    [MinMaxSlider(0f,2f)]
    [SerializeField] private Vector2 _pitchRange = Vector2.one;

    public void Play(AudioSource source)
    {
        if (_audioClips.Count == 0)
        {
            Debug.LogWarning("AudioCue has no audioclips to play");
            return;
        }

        source.clip = _audioClips[Random.Range(0, _audioClips.Count)];
        source.volume = Random.Range(_volumeRange.x, _volumeRange.y);
        source.pitch = Random.Range(_pitchRange.x, _pitchRange.y);
        source.Play();  
    }

    public void PlayOneShot(AudioSource source) 
    {
        if (_audioClips.Count == 0)
        {
            Debug.LogWarning("AudioCue has no audioclips to play");
            return;
        }

        source.volume = Random.Range(_volumeRange.x, _volumeRange.y);
        source.pitch = Random.Range(_pitchRange.x, _pitchRange.y);
        source.PlayOneShot(_audioClips[Random.Range(0, _audioClips.Count)]);
    }

    public void PlayOneShot(AudioSource source, float pitch)
    {
        if (_audioClips.Count == 0)
        {
            Debug.LogWarning("AudioCue has no audioclips to play");
            return;
        }

        source.volume = Random.Range(_volumeRange.x, _volumeRange.y);
        source.pitch = pitch;
        source.PlayOneShot(_audioClips[Random.Range(0, _audioClips.Count)]);
    }

}
