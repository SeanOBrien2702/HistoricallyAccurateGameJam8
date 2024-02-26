using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;
using System;
using FMOD;

public class AudioController : MonoBehaviour
{
    [SerializeField] EventReference happy;
    [SerializeField] EventReference commanding;
    [SerializeField] EventReference scheme;

    List<EventInstance> backgroundMusic = new List<EventInstance>();
    EventInstance soundFX;
    public static AudioController Instance { get; private set; }
    int currentMusic = 0;

    float musicVolume = 0.5f;
    float soundVolume = 0.5f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        backgroundMusic.Add(RuntimeManager.CreateInstance(happy));
        backgroundMusic.Add(RuntimeManager.CreateInstance(happy));
        backgroundMusic.Add(RuntimeManager.CreateInstance(happy));
        backgroundMusic.Add(RuntimeManager.CreateInstance(commanding));
        backgroundMusic.Add(RuntimeManager.CreateInstance(scheme));
        
        backgroundMusic[0].setParameterByName(GetStatusString(0), 0);
        backgroundMusic[0].start();
        ContextController.OnNewContext += ContextController_OnNewContext;
        GameSettings.OnMusicVolumeChange += GameSettings_OnMusicVolumeChange;
        GameSettings.OnSoundFXVolumeChange += GameSettings_OnSoundFXVolumeChange;
    }

    private void OnDestroy()
    {
        ContextController.OnNewContext -= ContextController_OnNewContext;
        GameSettings.OnMusicVolumeChange -= GameSettings_OnMusicVolumeChange;
        GameSettings.OnSoundFXVolumeChange -= GameSettings_OnSoundFXVolumeChange;
    }

    private void ContextController_OnNewContext(Context context)
    {
        int newMusic = (int)context.BackgroundMusic;

        if (currentMusic != newMusic)
        {
            backgroundMusic[newMusic].setParameterByName(GetStatusString(newMusic), 0);            
            backgroundMusic[newMusic].start();
            backgroundMusic[newMusic].setVolume(musicVolume);
            backgroundMusic[currentMusic].setParameterByName(GetStatusString(currentMusic), 1);
            currentMusic = newMusic;
        }

        if (context.MusicIntensity >= 0)
        {
            backgroundMusic[currentMusic].setParameterByName(GetIntensityString(currentMusic), context.MusicIntensity);
        }
    }

    private string GetStatusString(int musicIndex)
    {
        return "Status" + (musicIndex + 1);
    }

    private string GetIntensityString(int musicIndex)
    {
        return "Intensity" + (musicIndex + 1);
    }

    public void PlayOneShot(EventReference sound)
    {
        RuntimeManager.PlayOneShot(sound);
    }

    internal void EndSoundEffect()
    {
        soundFX.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    internal void PlaySoundEffect(EventReference soundEffect)
    {
        soundFX = RuntimeManager.CreateInstance(soundEffect);
        soundFX.setVolume(soundVolume);
        soundFX.start();
    }

    private void GameSettings_OnSoundFXVolumeChange(float newSoundVolume)
    {
        soundVolume = newSoundVolume;
        soundFX.setVolume(soundVolume);
    }

    private void GameSettings_OnMusicVolumeChange(float newMusicVolume)
    {
        musicVolume = newMusicVolume;
        backgroundMusic[currentMusic].setVolume(musicVolume);
    }
}