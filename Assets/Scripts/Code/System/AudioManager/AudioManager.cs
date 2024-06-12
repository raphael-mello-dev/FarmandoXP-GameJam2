using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Audio;
using System.Linq;
using UnityEngine.SceneManagement;

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
    Thunder,
    Rain,
    Wind,
    Alarm,
    Delivery,
    GameOver
}

public class AudioManager : MonoBehaviour
{
    [SerializeField] private float thunderMaxDistance;
    [SerializeField] private List<AudioSource> audioSourceMock;
    [SerializeField] private float audioSourceMockSize = 10f;
    private bool flag = false;

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource soundtrackAudioSource;
    [SerializeField] private AudioSource sfxAudioSource;
    [SerializeField] private AudioSource menuSFXAudioSource;
    [SerializeField] private AudioSource engineAudioSource;

    [SerializeField] private GameObject audioPrefab3D;

    [SerializeField] private SoundtrackConfig[] soundtracksConfig;
    [SerializeField] private SFXConfig[] sfxsConfig;



    private Dictionary<MixerGroup, string> mixerGroupDict;
    private Dictionary<Soundtracks, SoundtrackConfig> soundtracksDict;
    private Dictionary<SFXs, SFXConfig> sfxsDict;
    private PlayerLocomotion player;

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

    private void FixedUpdate()
    {
        if(SceneManager.GetActiveScene().buildIndex > 0 && !flag)
        {
            flag = true;
            InitAudioSource();
        }

        if (SceneManager.GetActiveScene().buildIndex <= 0 && flag)
        {
            flag = false;
            DetachAudioSource();
        }

        if (player != null) return;
        player = FindAnyObjectByType<PlayerLocomotion>();
    }

    public void InitAudioSource()
    {
        for (int i = 0; i < audioSourceMockSize; i++)
        {
            audioSourceMock.Add(Instantiate(audioPrefab3D).GetComponent<AudioSource>());
        }
    }
    public void DetachAudioSource()
    {
        audioSourceMock.Clear();
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
        if (!GameManager.Instance.IsPaused)
        {
            if (sfxsDict.ContainsKey(type))
            {
                SFXConfig config = sfxsDict[type];
                sfxAudioSource.PlayOneShot(config.audioClip, config.volume);
            }
        }
    }

    public void PlaySFXAtPoint(SFXs type, Transform soundPosition)
    {
        if (player == null) return;
        AudioSource freeAudioSource = audioSourceMock.Find(go => go.isPlaying == false);

        float distance = Vector3.Distance(soundPosition.position, player.transform.position);
        distance = Mathf.Clamp(thunderMaxDistance - Mathf.Abs(distance), 0f, thunderMaxDistance);
        float volume = distance / thunderMaxDistance;

        if (freeAudioSource == null)
        {
            freeAudioSource = audioSourceMock.OrderBy(go => go.volume).FirstOrDefault();
        }
        //como coloco o audiosource como 33d, mas que ele nao fique andando pelos lados do fone, apenas para audio?
        if (distance >= thunderMaxDistance) return;

        AudioSource audioSource = freeAudioSource;
        audioSource.transform.position = soundPosition.position;

        if (!GameManager.Instance.IsPaused)
        {
            if (sfxsDict.ContainsKey(type))
            {
                SFXConfig config = sfxsDict[type];

                audioSource.PlayOneShot(config.audioClip, volume);
            }
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