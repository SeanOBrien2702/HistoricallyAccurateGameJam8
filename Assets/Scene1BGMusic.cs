using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

public class Scene1BGMusic : MonoBehaviour
{
    FMOD.Studio.EventInstance BackgroundMusic01;
    FMOD.Studio.EventInstance BackgroundMusic02;
    // FMOD.Studio.EventInstance BackgroundMusic03;
    FMOD.Studio.EventInstance BackgroundMusic04;
    FMOD.Studio.EventInstance BackgroundMusic05;

    private int playingMood;
    private bool moodHasChanged = true;

    private float intensity;
    private bool intensityHasChanged = true;

    // Start is called before the first frame update
    void Start()
    {
            BackgroundMusic01 = FMODUnity.RuntimeManager.CreateInstance("event:/01-Happy-uplifting");
            BackgroundMusic02 = FMODUnity.RuntimeManager.CreateInstance("event:/02-Pressure");
            BackgroundMusic04 = FMODUnity.RuntimeManager.CreateInstance("event:/04-Commanding");
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

        switch(playingMood) {
            case 1: BackgroundMusic01.setParameterByName("Status1", 1); break;
            case 2: BackgroundMusic02.setParameterByName("Status2", 1); break;
            case 4: BackgroundMusic04.setParameterByName("Status4", 1); break;
            case 5: BackgroundMusic05.setParameterByName("Status5", 1); break;
        }

        if (mood != playingMood) {


            switch(mood) {
                case 1:
                    intensity=0;
                    BackgroundMusic01.setParameterByName("Status1", 0);
                    BackgroundMusic01.start();
                    break;
                case 2:
                    intensity=0;
                    BackgroundMusic02.setParameterByName("Status2", 0);
                    BackgroundMusic02.start();
                    break;                    
                case 4:
                    intensity=0;
                    BackgroundMusic04.setParameterByName("Status4", 0);
                    BackgroundMusic04.start();
                    break;
                case 5:
                    intensity=0;
                    BackgroundMusic05.setParameterByName("Intensity5", 0f);
                    BackgroundMusic05.setParameterByName("Status5", 0);
                    BackgroundMusic05.start();
                    break;
                default:
                    break;
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
