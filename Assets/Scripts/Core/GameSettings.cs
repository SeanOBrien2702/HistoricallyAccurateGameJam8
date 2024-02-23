using System;
using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    public static event Action<float> OnReadSpeedChange = delegate { };
    public static event Action<float> OnMusicVolumeChange = delegate { };

    [SerializeField] Slider readingSpeed;
    [SerializeField] Slider musicVolume;
    //TODO: connect SFX volumes
    [SerializeField] Slider soundFXVolume;

    void Awake()
    {
        readingSpeed.onValueChanged.AddListener(delegate { UpdateReadingSpeed(); });
        musicVolume.onValueChanged.AddListener(delegate { UpdateMusicVolume(); });
    }

    private void UpdateReadingSpeed()
    {
        OnReadSpeedChange?.Invoke(readingSpeed.value);
    }

    private void UpdateMusicVolume()
    {
        OnMusicVolumeChange?.Invoke(musicVolume.value);
    }
}