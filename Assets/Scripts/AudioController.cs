using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;
using System;
using FMOD;

public class AudioController : MonoBehaviour
{
    [Header("Music")]
    [SerializeField] EventReference happy;
    [SerializeField] EventReference commanding;
    [SerializeField] EventReference scheme;

    List<EventInstance> backgroundMusic = new List<EventInstance>();
    EventInstance soundFX;
    EventInstance voiceFX;

    [Header("Voices")]
    [SerializeField] EventReference david;
    [SerializeField] EventReference george;
    [SerializeField] EventReference henry;
    [SerializeField] EventReference patrick;
    [SerializeField] EventReference mary;
    [SerializeField] EventReference william;
    Dictionary<string, EventReference> voices = new Dictionary<string, EventReference>();

    public static AudioController Instance { get; private set; }
    int currentMusic = 0;

    float musicVolume = 0.5f;
    float soundVolume = 0.5f;
    float voiceVolume = 0.5f;

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

        voices.Add("David", david);
        voices.Add("Mary", mary);
        voices.Add("Henry", henry);
        voices.Add("Ruthven", patrick);
        voices.Add("George", george);
        voices.Add("Maitland", william);

        ContextController.OnNewContext += ContextController_OnNewContext;
        DialogueController.OnNewDialogue += DialogueController_OnNewDialogue;
        DialogueController.OnDialogueEnd += DialogueController_OnDialogueEnd;
        GameSettings.OnMusicVolumeChange += GameSettings_OnMusicVolumeChange;
        GameSettings.OnSoundFXVolumeChange += GameSettings_OnSoundFXVolumeChange;
        GameSettings.OnVoicesFXVolumeChange += GameSettings_OnVoicesFXVolumeChange;
    }

    private void OnDestroy()
    {
        ContextController.OnNewContext -= ContextController_OnNewContext;
        DialogueController.OnNewDialogue -= DialogueController_OnNewDialogue;
        DialogueController.OnDialogueEnd -= DialogueController_OnDialogueEnd;
        GameSettings.OnMusicVolumeChange -= GameSettings_OnMusicVolumeChange;
        GameSettings.OnSoundFXVolumeChange -= GameSettings_OnSoundFXVolumeChange;
        GameSettings.OnVoicesFXVolumeChange -= GameSettings_OnVoicesFXVolumeChange;
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

    void PlaySoundEffect(EventReference soundEffect)
    {
        soundFX = RuntimeManager.CreateInstance(soundEffect);
        soundFX.setVolume(soundVolume);
        soundFX.start();
    }

    void PlayCharacterVoice(Dialogue dialogue)
    {
        string characterName = "";
        if (dialogue.LeftCharacterName != "")
        {
            characterName = dialogue.LeftCharacterName;
        }
        else if(dialogue.RightCharacterName != "")
        {
            characterName = dialogue.RightCharacterName;
        }

        if(voices.ContainsKey(characterName))
        {
            voiceFX = RuntimeManager.CreateInstance(voices[characterName]);
            voiceFX.setVolume(voiceVolume);
            voiceFX.start();
        }
    }

    private void DialogueController_OnNewDialogue(Dialogue dialogue)
    {
        soundFX.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        if (!dialogue.SoundEffect.IsNull)
        {
            PlaySoundEffect(dialogue.SoundEffect);
        }
        PlayCharacterVoice(dialogue);
    }

    private void DialogueController_OnDialogueEnd(Dialogue obj)
    {
        voiceFX.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
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

    private void GameSettings_OnVoicesFXVolumeChange(float newVoiceVolume)
    {
        voiceVolume = newVoiceVolume;
        voiceFX.setVolume(voiceVolume);
    }

    internal void PlayOneShot(EventReference sound)
    {
        var instance = RuntimeManager.CreateInstance(sound);
        instance.setVolume(soundVolume);
        instance.start();
        instance.release();
    }
}