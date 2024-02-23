using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class AudioController : MonoBehaviour
{
    [SerializeField] EventReference happy;
    [SerializeField] EventReference commanding;
    [SerializeField] EventReference scheme;

    List<EventInstance> backgroundMusic = new List<EventInstance>();

    int currentMusic = 0;

    float musicVolume = 0.5f;

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
    }

    private void GameSettings_OnMusicVolumeChange(float newMusicVolume)
    {
        musicVolume = newMusicVolume;
        backgroundMusic[currentMusic].setVolume(musicVolume);
    }

    private void OnDestroy()
    {
        ContextController.OnNewContext -= ContextController_OnNewContext;
        GameSettings.OnMusicVolumeChange -= GameSettings_OnMusicVolumeChange;
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
}