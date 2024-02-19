using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance { get; private set; }

    private void Awake() {
        if (instance != null ) {
            Debug.LogError("There's more than one audio manager in this scene.");
        }
        instance = this;
    }

    public void PlayOneShot(EventReference sound, Vector3 intensity) {
        RuntimeManager.PlayOneShot(sound, intensity);
    }

    public EventInstance CreateInstance(EventReference eventReference) {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        return eventInstance;
    }
 
}