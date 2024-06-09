using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "AudioSourceSO", menuName = "FarmandoXP/Audio/AudioSourceSO", order = 1)]
public class AudioSourceSO : ScriptableObject
{
    [TextAreaAttribute]
    public string Description;
    [Space(5)]

    [Header("Basic Informations About AudioSource")]
    [Space(2)]
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private AudioMixerGroup audioMixerGroup;

    [SerializeField] private bool loop = false;
    [SerializeField] private bool PlayOnAwake = true;

    [Range(0f, 1f)]  [SerializeField] private float volume = 1f;
    [Range(0, 256)]  [SerializeField] private int priority = 128;
    [Range(-3f, 3f)] [SerializeField] private float pitch = 1f;
    
    [Header("Informations About 3D AudioSource")]
    [Range(-1f, 1f)] [SerializeField] private float stereoPan = 0f;
    [Range(0f, 1f)]  [SerializeField] private float spatialBlend = 0f;
    [Range(0f, 5f)]  [SerializeField] private float dopplerLevel = 1f;
    [Range(0, 360)]  [SerializeField] private int spread = 0;

    [SerializeField] private float minDistance = 1f;
    [SerializeField] private float maxDistance = 500f;

    private AudioSource audioSourceRef;
    
    internal void Setup(AudioSource audioSource)
    {
        audioSource.clip = audioClip;
        audioSource.outputAudioMixerGroup = audioMixerGroup;

        audioSource.loop = loop;
        audioSource.volume = volume;

        audioSource.volume = volume;
        audioSource.priority = priority;
        audioSource.pitch = pitch;

        audioSource.panStereo = stereoPan;
        audioSource.spatialBlend = spatialBlend;
        audioSource.dopplerLevel = dopplerLevel;
        audioSource.spread = spread;

        audioSource.minDistance = minDistance;
        audioSource.maxDistance = maxDistance;

        audioSourceRef = audioSource;
    }

    internal AudioSource GetAudioSource()
    {
        return audioSourceRef;
    }
}
