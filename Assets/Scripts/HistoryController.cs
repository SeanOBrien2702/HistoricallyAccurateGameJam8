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
    CanvasGroup canvas;
    Image image;

    public bool IsShown { get => isShown; set => isShown = value; }

    void Start()
    {
        canvas = GetComponent<CanvasGroup>();
        image = GetComponent<Image>();
        canvas.alpha = isShown ? 1 : 0;
        DialogueController.OnNewDialogue += DialogueController_OnNewDialogue;
    }

    private void OnDestroy()
    {
        DialogueController.OnNewDialogue -= DialogueController_OnNewDialogue;
    }

    private void DialogueController_OnNewDialogue(Dialogue dialogue)
    {
        TextMeshProUGUI newText = Instantiate(textPrefab, content);
        newText.text = dialogue.DialogueText;
        if(dialogue.LeftCharacterName != "")
        {
            newText.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = dialogue.LeftCharacterName;
        }
        if (dialogue.RightCharacterName != "")
        {
            newText.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = dialogue.RightCharacterName;
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
        canvas.alpha = isShown ? 1 : 0;
    }
}