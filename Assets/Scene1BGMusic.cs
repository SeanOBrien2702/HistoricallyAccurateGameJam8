using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

public class Scene1BGMusic : MonoBehaviour
{
    FMOD.Studio.EventInstance BackgroundMusic;
    private bool musicIsStarted;

    private float variation = -1f;

    // Start is called before the first frame update
    void Start()
    {
            BackgroundMusic = FMODUnity.RuntimeManager.CreateInstance("event:/01-Wedding-Music");
            BackgroundMusic.start();
            musicIsStarted = true;
    }

    public void Update() {
        float newVariation;
        BackgroundMusic.getParameterByName("NextVariation", out newVariation);

        if ( variation != newVariation) {
            Debug.Log("Next Variation: " + newVariation);
            variation = newVariation;
        }

    }

    public void toggleMusic() {
        if (musicIsStarted) {
            musicIsStarted = false;
            BackgroundMusic.setParameterByNameWithLabel("Playing", "End");
            Debug.Log("Click - Stop Music");
        } else {
            musicIsStarted = true;
            Debug.Log("Click - Start Music");
            BackgroundMusic.setParameterByNameWithLabel("Playing", "Playing");
            BackgroundMusic.start();
        }
    }
}
