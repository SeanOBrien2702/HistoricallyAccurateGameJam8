using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class GameSettings : MonoBehaviour
{
    public static event Action<float> OnReadSpeedChange = delegate { };

    [SerializeField] Slider readingSpeed;
    //TODO: connect music and SFX volumes
    [SerializeField] Slider musicVolume;
    [SerializeField] Slider soundFXVolume;

    void Awake()
    {
        readingSpeed.onValueChanged.AddListener(delegate { UpdateReadingSpeed(); });
    }

    private void UpdateReadingSpeed()
    {
        OnReadSpeedChange?.Invoke(readingSpeed.value);
    }
}