using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public enum MixerGroup
{
    Master,
    Soundtracks,
    SFX
}

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
    [SerializeField] private AudioSource menuSFXAudioSource;
    [SerializeField] private AudioSource engineAudioSource;

    [SerializeField] private SoundtrackConfig[] soundtracksConfig;
    [SerializeField] private SFXConfig[] sfxsConfig;

    private Dictionary<MixerGroup, string> mixerGroupDict;
    private Dictionary<Soundtracks, SoundtrackConfig> soundtracksDict;
    private Dictionary<SFXs, SFXConfig> sfxsDict;

    void Awake()
    {
        mixerGroupDict = new()
        {
            { MixerGroup.Master, "VolumeMaster" },
            { MixerGroup.Soundtracks, "VolumeSoundtracks" },
            { MixerGroup.SFX, "VolumeSFX" }
        };

        soundtracksDict = soundtracksConfig.ToDictionary(config => config.audioType, config => config);
        sfxsDict = sfxsConfig.ToDictionary(config => config.audioType, config => config);
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SetMixerVolume(MixerGroup group, float normalizedValue)
    {
        string groupString = mixerGroupDict[group];
        float volume = Mathf.Log10(normalizedValue) * 20;
        audioMixer.SetFloat(groupString, volume);
    }

    public float GetMixerVolume(MixerGroup group, bool normalize = true)
    {
        string groupString = mixerGroupDict[group];
        audioMixer.GetFloat(groupString, out float volume);

        if (normalize)
        {
            volume = Mathf.Pow(10, volume / 20);
        }

        return volume;
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

    public void PlayMenuSFX(SFXs type)
    {
        if (sfxsDict.ContainsKey(type))
        {
            SFXConfig config = sfxsDict[type];
            menuSFXAudioSource.PlayOneShot(config.audioClip, config.volume);
        }
    }

    public void SetEnginePitch(float pitch)
    {
        engineAudioSource.pitch = pitch;
    }

    public void StartEngine()
    {
        SetEnginePitch(0f);
        engineAudioSource.Play();
    }

    public void StopEngine()
    {
        engineAudioSource.Stop();
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