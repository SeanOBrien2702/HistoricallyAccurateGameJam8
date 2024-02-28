using System;
using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    public static event Action<float> OnReadSpeedChange = delegate { };
    public static event Action<float> OnMusicVolumeChange = delegate { };
    public static event Action<float> OnSoundFXVolumeChange = delegate { };
    public static event Action<float> OnVoicesFXVolumeChange = delegate { };

    [SerializeField] Slider readingSpeed;
    [SerializeField] Slider musicVolume;
    [SerializeField] Slider soundFXVolume;
    [SerializeField] Slider voiceFXVolume;

    void Awake()
    {
        readingSpeed.onValueChanged.AddListener(delegate { UpdateReadingSpeed(); });
        musicVolume.onValueChanged.AddListener(delegate { UpdateMusicVolume(); });
        soundFXVolume.onValueChanged.AddListener(delegate { UpdateSoundFXVolume(); });
        voiceFXVolume.onValueChanged.AddListener(delegate { UpdateVoiceFXVolume(); });
    }

    private void UpdateVoiceFXVolume()
    {
        OnVoicesFXVolumeChange?.Invoke(voiceFXVolume.value);
    }

    private void UpdateReadingSpeed()
    {
        OnReadSpeedChange?.Invoke(readingSpeed.value);
    }

    private void UpdateMusicVolume()
    {
        OnMusicVolumeChange?.Invoke(musicVolume.value);
    }

    private void UpdateSoundFXVolume()
    {
        OnSoundFXVolumeChange?.Invoke(soundFXVolume.value);
    }
}