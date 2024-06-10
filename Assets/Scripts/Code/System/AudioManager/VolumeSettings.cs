using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private Slider volumeMasterSlider;
    [SerializeField] private Slider volumeSoundtracksSlider;
    [SerializeField] private Slider volumeSFXSlider;

    [SerializeField] private TextMeshProUGUI masterPercentage;
    [SerializeField] private TextMeshProUGUI soundtracksPercentage;
    [SerializeField] private TextMeshProUGUI sfxPercentage;

    void Start()
    {
        volumeMasterSlider.onValueChanged.AddListener(OnMasterSliderVolumeChanged);
        volumeSoundtracksSlider.onValueChanged.AddListener(OnSoundtracksSliderVolumeChanged);
        volumeSFXSlider.onValueChanged.AddListener(OnSFXSliderVolumeChanged);

        volumeMasterSlider.SetValueWithoutNotify(GameManager.Instance.AudioManager.GetMixerVolume(MixerGroup.Master));
        volumeSoundtracksSlider.SetValueWithoutNotify(GameManager.Instance.AudioManager.GetMixerVolume(MixerGroup.Soundtracks));
        volumeSFXSlider.SetValueWithoutNotify(GameManager.Instance.AudioManager.GetMixerVolume(MixerGroup.SFX));

        OnPercentageTextChanged(volumeMasterSlider, masterPercentage);
        OnPercentageTextChanged(volumeSoundtracksSlider, soundtracksPercentage);
        OnPercentageTextChanged(volumeSFXSlider, sfxPercentage);
    }

    private void OnMasterSliderVolumeChanged(float value)
    {
        GameManager.Instance.AudioManager.SetMixerVolume(MixerGroup.Master, value);
        OnPercentageTextChanged(volumeMasterSlider, masterPercentage);
    }

    private void OnSoundtracksSliderVolumeChanged(float value)
    {
        GameManager.Instance.AudioManager.SetMixerVolume(MixerGroup.Soundtracks, value);
        OnPercentageTextChanged(volumeSoundtracksSlider, soundtracksPercentage);
    }

    private void OnSFXSliderVolumeChanged(float value)
    {
        GameManager.Instance.AudioManager.SetMixerVolume(MixerGroup.SFX, value);
        OnPercentageTextChanged(volumeSFXSlider, sfxPercentage);
    }

    private void OnPercentageTextChanged(Slider slider, TextMeshProUGUI text)
    {
        text.text = $"{slider.value * 100:00}%";
    }
}