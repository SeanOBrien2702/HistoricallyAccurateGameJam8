using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ContextController : MonoBehaviour
{
    [SerializeField] Image background;
    [SerializeField] List<Context> contexts = new List<Context>();

    void Start()
    {
        DialogueController.OnNewDialogue += DialogueController_OnNewDialogue;
    }

    void OnDestroy()
    {
        DialogueController.OnNewDialogue -= DialogueController_OnNewDialogue;
    }

    void DialogueController_OnNewDialogue(Dialogue dialogue)
    {
        foreach (var context in contexts)
        {           
            if(context.Dialogues.Contains(dialogue))
            {
                background.sprite = context.BackgroundImage;
            }
        }
    }
}