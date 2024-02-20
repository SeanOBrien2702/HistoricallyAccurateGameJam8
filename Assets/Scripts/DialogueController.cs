using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    [SerializeField] float textSpeed;
    [SerializeField] TextMeshProUGUI leftNameText;
    [SerializeField] TextMeshProUGUI rightNameText;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] Dialogue startingDialogue;
    [SerializeField] Image leftCharacter;
    [SerializeField] Image rightCharacter;
    bool isReading = false;
    Dialogue currentDialogue;

    void Start()
    {
        currentDialogue = startingDialogue;
        StartCoroutine(PlayText(startingDialogue));
        OptionTextLinkHandler.OnOptionPicked += OptionTextLinkHandler_OnOptionPicked;
    }

    private void OnDestroy()
    {
        OptionTextLinkHandler.OnOptionPicked -= OptionTextLinkHandler_OnOptionPicked;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isReading)
        {
            if(currentDialogue.NextDialogue != null)
            {
                StartCoroutine(PlayText(currentDialogue.NextDialogue));
            }          
        }
    }

    IEnumerator PlayText(Dialogue dialogue)
    {
        isReading = true;
        dialogueText.text = "";
        currentDialogue = dialogue;
        UpdateUI(dialogue);

        foreach (char character in dialogue.DialogueText)
        {
            dialogueText.text += character;
            yield return new WaitForSeconds(textSpeed);
        }
          
        dialogueText.text = dialogue.DialogueText;
        int optionsIndex = 0;
        foreach (var option in dialogue.DialogueOptions)
        {
            dialogueText.text += '\n' + " <i><link=\"" + optionsIndex +"\">"+ option.OptionText+ "</i></link>";
        }
        isReading = false;
    }

    private void UpdateUI(Dialogue dialogue)
    {
        leftNameText.text = dialogue.LeftCharacterName;
        rightNameText.text = dialogue.RightCharacterName;
        leftCharacter.sprite = dialogue.LeftCharacterImage ?? leftCharacter.sprite;
        rightCharacter.sprite = dialogue.RightCharacterImage ?? rightCharacter.sprite;
    }

    private void OptionTextLinkHandler_OnOptionPicked(int selectedIndex)
    {
        StartCoroutine(PlayText(currentDialogue.DialogueOptions[selectedIndex].Dialogue));
    }
}