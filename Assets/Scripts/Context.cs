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

    public Sprite BackgroundImage { get => backgroundImage; set => backgroundImage = value; }
    public Dialogue[] Dialogues { get => dialogues; set => dialogues = value; }
}