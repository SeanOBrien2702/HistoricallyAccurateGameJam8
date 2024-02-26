using System;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Context", fileName = "Context.asset")]
public class Context : ScriptableObject
{   
    [SerializeField] Sprite backgroundImage;
    //TODO: add background music
    //[SerializeField] BackGroundMusic;
    [SerializeField] Dialogue[] dialogues;
    [SerializeField] BackgroundMusicType backgroundMusic;
    [SerializeField] int musicIntensity = -1;

    public Sprite BackgroundImage { get => backgroundImage; }
    public Dialogue[] Dialogues { get => dialogues; }
    public BackgroundMusicType BackgroundMusic { get => backgroundMusic; }
    public int MusicIntensity { get => musicIntensity; }
}