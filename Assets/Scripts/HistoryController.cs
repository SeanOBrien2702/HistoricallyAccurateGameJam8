using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HistoryController : MonoBehaviour
{
    Queue<TextMeshProUGUI> historyList = new Queue<TextMeshProUGUI>();
    [SerializeField] Transform content;
    [SerializeField] TextMeshProUGUI textPrefab;
    [SerializeField] int maxHistoryCount;
    bool isShown = false;
    Image image;

    public bool IsShown { get => isShown; set => isShown = value; }

    void Start()
    {
        image = GetComponent<Image>();
        DialogueController.OnNewDialogue += DialogueController_OnNewDialogue;
    }

    private void OnDestroy()
    {
        DialogueController.OnNewDialogue -= DialogueController_OnNewDialogue;
    }

    private void DialogueController_OnNewDialogue(Dialogue dialogue)
    {
        TextMeshProUGUI newText = Instantiate(textPrefab, content);
        
        if(dialogue.LeftCharacterName != "")
        {
            newText.text = dialogue.LeftCharacterName + ": ";
        }
        if (dialogue.RightCharacterName != "")
        {
            newText.text = dialogue.RightCharacterName + ": "; ;
        }
        newText.text += dialogue.DialogueText;
        if(dialogue.LeftCharacterName == "" &&
            dialogue.RightCharacterName == "")
        {
            newText.alignment = TextAlignmentOptions.Center;
        }
        historyList.Enqueue(newText);
        if(historyList.Count > maxHistoryCount)
        {
            Destroy(historyList.Dequeue().gameObject);
        }
    }

    public void ToggleHistory()
    {
        isShown = !isShown;
        image.raycastTarget = isShown;
    }
}