using System;
using UnityEngine;

[System.Serializable]
public class DialogueOption
{
    [SerializeField] string optionText;
    [SerializeField] Dialogue dialogue;

    public string OptionText { get => optionText; set => optionText = value; }
    public Dialogue Dialogue { get => dialogue; set => dialogue = value; }
}

