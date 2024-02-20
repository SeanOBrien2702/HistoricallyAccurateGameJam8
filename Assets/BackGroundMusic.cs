using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMusic : MonoBehaviour
{
    FMOD.Studio.EventInstance BackgroundMusic;
    private bool musicIsStarted;

    [SerializeField][Range(0f,3f)]
    private float intensity;

    // Start is called before the first frame update
    void Start()
    {
            BackgroundMusic = FMODUnity.RuntimeManager.CreateInstance("{747ee637-f9e0-4032-bc54-0855e81afb7a}");
            BackgroundMusic.start();
            musicIsStarted = true;
    }

    public void increaseIntensity() {
        float prevIntensity = intensity;
        intensity += 1f;
        if (intensity >= 4f) intensity = 0f;
        BackgroundMusic.setParameterByName("intensity", intensity);
        Debug.Log("Click - Intensity increased from " + prevIntensity + " to " + intensity);
    }


    public void killSting() {
       BackgroundMusic.setParameterByNameWithLabel("Kill", "Kill");
       Debug.Log("Click - Kill Sting");
    }


    public void endSting() {
       BackgroundMusic.setParameterByNameWithLabel("End", "End Scene");
       intensity = 0f;
       Debug.Log("Click - Back to normal - Intensity set to 0 - End sting triggered");
    }

    public void toggleMusic() {
        if (musicIsStarted) {
            musicIsStarted = false;
            BackgroundMusic.setParameterByNameWithLabel("End", "Stop Music");
            Debug.Log("Click - Stop Music");
        } else {
            musicIsStarted = true;
            Debug.Log("Click - Start Music");
            BackgroundMusic.setParameterByNameWithLabel("End", "Normal");
            BackgroundMusic.setParameterByNameWithLabel("Kill", "Normal");
            BackgroundMusic.setParameterByName("intensity", 0f);
            BackgroundMusic.start();
            intensity = 0;
        }
    }
}
