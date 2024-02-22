using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

public class Scene1BGMusic : MonoBehaviour
{
    FMOD.Studio.EventInstance BackgroundMusic01;
    // FMOD.Studio.EventInstance BackgroundMusic02;
    // FMOD.Studio.EventInstance BackgroundMusic03;
    // FMOD.Studio.EventInstance BackgroundMusic04;
    FMOD.Studio.EventInstance BackgroundMusic05;

    private int playingMood;
    private bool moodHasChanged = true;

    private float intensity;
    private bool intensityHasChanged = true;

    // Start is called before the first frame update
    void Start()
    {
            BackgroundMusic01 = FMODUnity.RuntimeManager.CreateInstance("event:/01-Happy-uplifting");
            BackgroundMusic05 = FMODUnity.RuntimeManager.CreateInstance("event:/05-Scheme");
           playingMood = 0;
    }

    public void Update() {
        if (moodHasChanged) {
            Debug.Log("Mood : " + playingMood);
            if (playingMood==5) {
                Debug.Log("Intensity : "+intensity);
            }
            moodHasChanged = false;
        }

        if (intensityHasChanged) {
            Debug.Log("Intensity : "+intensity);
            intensityHasChanged = false;
        }
    }

    public void PlayMusic(int mood) {

        if (playingMood==1) { BackgroundMusic01.setParameterByName("Status1", 1); }
        if (playingMood==5) { BackgroundMusic05.setParameterByName("Status5", 1); }

        if (mood != playingMood) {



            if (mood==1) {
                intensity=0;
                BackgroundMusic01.start();
                BackgroundMusic01.setParameterByName("Status1", 0);
            }
            if (mood==5) { 
                intensity=0;
                BackgroundMusic05.start();
                BackgroundMusic05.setParameterByName("Intensity5", 0f);
                BackgroundMusic05.setParameterByName("Status5", 0);
            }

            playingMood = mood;
        } else {
            playingMood = 0;
        }
        moodHasChanged = true;
    }

    public void IncreaseIntensity() {
        
        if (playingMood==5) {
            intensity ++;
            if (intensity > 3) { intensity = 0; }
            BackgroundMusic05.setParameterByName("Intensity5", intensity);
            intensityHasChanged = true;
        }
    }
}
