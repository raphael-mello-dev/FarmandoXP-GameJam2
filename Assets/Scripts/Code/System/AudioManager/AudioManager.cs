using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public enum Soundtracks
{
    Menu,
    Gameplay
}

public enum SFXs
{
    ButtonClick,
    GameOver
}

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource soundtrackAudioSource;
    [SerializeField] private AudioSource sfxAudioSource;
    
    [SerializeField] private SoundtrackConfig[] soundtracksConfig;
    [SerializeField] private SFXConfig[] sfxsConfig;

    private Dictionary<Soundtracks, SoundtrackConfig> soundtracksDict;
    private Dictionary<SFXs, SFXConfig> sfxsDict;

    void Awake()
    {
        soundtracksDict = soundtracksConfig.ToDictionary(config => config.audioType, config => config);
        sfxsDict = sfxsConfig.ToDictionary(config => config.audioType, config => config);
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySoundtrack(Soundtracks type)
    {
        if (soundtracksDict.ContainsKey(type))
        {
            SoundtrackConfig config = soundtracksDict[type];
            soundtrackAudioSource.Stop();
            soundtrackAudioSource.clip = config.audioClip;
            soundtrackAudioSource.volume = config.volume;
            soundtrackAudioSource.Play();
        }
    }

    public void PlaySFX(SFXs type)
    {
        if (sfxsDict.ContainsKey(type))
        {
            SFXConfig config = sfxsDict[type];
            sfxAudioSource.PlayOneShot(config.audioClip, config.volume);
        }
    }
}

[Serializable]
public struct SoundtrackConfig
{
    public Soundtracks audioType;
    public AudioClip audioClip;

    [Range(0f, 1f)]
    public float volume;
}

[Serializable]
public struct SFXConfig
{
    public SFXs audioType;
    public AudioClip audioClip;
    
    [Range(0f, 1f)]
    public float volume;
}