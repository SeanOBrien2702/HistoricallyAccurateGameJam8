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

    [SerializeField] Dialogue dialogue;
    [SerializeField] DialogueOption[] dialogueOptions;

    public string DialogueText { get => dialogueText; set => dialogueText = value; }
    public Dialogue NextDialogue { get => dialogue; set => dialogue = value; }
    public DialogueOption[] DialogueOptions { get => dialogueOptions; set => dialogueOptions = value; }
    public string LeftCharacterName { get => leftCharacterName; set => leftCharacterName = value; }
    public Sprite LeftCharacterImage { get => leftCharacterImage; set => leftCharacterImage = value; }
    public string RightCharacterName { get => rightCharacterName; set => rightCharacterName = value; }
    public Sprite RightCharacterImage { get => rightCharacterImage; set => rightCharacterImage = value; }
}