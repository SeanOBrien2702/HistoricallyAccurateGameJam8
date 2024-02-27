using FMOD.Studio;
using FMODUnity;
using System;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Dialogue", fileName = "Dialogue.asset")]
public class Dialogue : ScriptableObject
{   
    [SerializeField, TextArea] string dialogueText;

    [Header("Left Character")]
    [SerializeField] string leftCharacterName;
    [SerializeField] Sprite leftCharacterImage;
    [Header("Right Character")]
    [SerializeField] string rightCharacterName;
    [SerializeField] Sprite rightCharacterImage;
    [Header("Dialogue Options")]
    [SerializeField] Dialogue dialogue;
    [SerializeField] DialogueOption[] dialogueOptions;
    [Header("SFX")]
    [SerializeField] EventReference soundEffect;
    [SerializeField] EventInstance soundEffect2;

    public string DialogueText { get => dialogueText; }
    public Dialogue NextDialogue { get => dialogue; }
    public DialogueOption[] DialogueOptions { get => dialogueOptions; }
    public string LeftCharacterName { get => leftCharacterName; }
    public Sprite LeftCharacterImage { get => leftCharacterImage; }
    public string RightCharacterName { get => rightCharacterName; }
    public Sprite RightCharacterImage { get => rightCharacterImage; }
    public EventReference SoundEffect { get => soundEffect; }
    public EventInstance SoundEffect2 { get => soundEffect2; set => soundEffect2 = value; }
}