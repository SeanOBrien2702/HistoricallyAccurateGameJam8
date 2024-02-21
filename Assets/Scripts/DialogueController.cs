using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    public static event System.Action<Dialogue> OnNewDialogue = delegate { };
    [SerializeField] HistoryController historyController;
    [Header("Dialogue")]
    [SerializeField] float textSpeed;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] Dialogue startingDialogue;
    [Header("Characters")]
    [SerializeField] TextMeshProUGUI leftNameText;
    [SerializeField] TextMeshProUGUI rightNameText;
    [SerializeField] Image leftCharacter;
    [SerializeField] Image rightCharacter;
    [Header("Options")]
    [SerializeField] Color optionColour;
    string optionColourHexCode;
    [SerializeField] Color hoverColour;
    string hoverColourHexCode;

    bool isReading = false;
    Dialogue currentDialogue;
    string currentText;

    void Start()
    {
        currentDialogue = startingDialogue;
        optionColourHexCode = ColorUtility.ToHtmlStringRGB(optionColour);
        hoverColourHexCode = ColorUtility.ToHtmlStringRGB(hoverColour);

        StartCoroutine(PlayText(startingDialogue));
        OptionTextLinkHandler.OnOptionPicked += OptionTextLinkHandler_OnOptionPicked;
        OptionTextLinkHandler.OnHoverOnOption += OptionTextLinkHandler_OnHoverOnOption;
        OptionTextLinkHandler.OnHoverOffOption += OptionTextLinkHandler_OnHoverOffOption;
    }

    private void OnDestroy()
    {
        OptionTextLinkHandler.OnOptionPicked -= OptionTextLinkHandler_OnOptionPicked;
        OptionTextLinkHandler.OnHoverOnOption -= OptionTextLinkHandler_OnHoverOnOption;
        OptionTextLinkHandler.OnHoverOffOption -= OptionTextLinkHandler_OnHoverOffOption;
    }

    void Update()
    {      
        if (Input.GetMouseButtonDown(0) && 
            !historyController.IsShown &&
            !IsOverUI() &&
            !isReading)
        {
            if (currentDialogue.NextDialogue != null)
            {
                StartCoroutine(PlayText(currentDialogue.NextDialogue));
            }          
        }
    }

    public bool IsOverUI()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        foreach (RaycastResult result in results)
        {
            if (LayerMask.LayerToName(result.gameObject.layer) == K.UILayer)
            {
                return true;
            }
        }

        return false;
    }

    IEnumerator PlayText(Dialogue dialogue)
    {
        isReading = true;
        dialogueText.text = "";
        currentDialogue = dialogue;
        OnNewDialogue?.Invoke(dialogue);
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
            dialogueText.text += '\n' + "  <color=#ff0000><i><link=\"" + optionsIndex +"\">"+ option.OptionText+ "</color></i></link>";
        }
        currentText = dialogueText.text;
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

    private void OptionTextLinkHandler_OnHoverOnOption(int index)
    {
        if (isReading)
        {
            return;
        }
        int position = 0;
        StringBuilder buffer = new StringBuilder(currentText);
        for (int i = 0; i <= index; i++)
        {
            position = currentText.IndexOf('#', position + 1);
        }

        position++;
        buffer.Remove(position, K.HexCodeLength);
        buffer.Insert(position, hoverColourHexCode);
        dialogueText.text = buffer.ToString();
    }

    private void OptionTextLinkHandler_OnHoverOffOption()
    {
        if (isReading)
        {
            return;
        }
        dialogueText.text = currentText;
    }
}