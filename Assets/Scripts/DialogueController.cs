using FMODUnity;
using System;
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
    public static event System.Action<Dialogue> OnDialogueEnd = delegate { };
    [SerializeField] HistoryController historyController;
    [SerializeField] EventReference nexDialogueSound;
    [Header("Dialogue")]
    [SerializeField] float textSpeed;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] Dialogue startingDialogue;
    bool isReading = false;
    bool isFastForward = false;
    [Header("Characters")]
    [SerializeField] TextMeshProUGUI leftNameText;
    [SerializeField] TextMeshProUGUI rightNameText;
    [SerializeField] Image leftNameTag;
    [SerializeField] Image rightNameTag;
    [SerializeField] Image leftCharacter;
    [SerializeField] Image rightCharacter;
    [Header("Options")]
    [SerializeField] Color optionColour;
    string optionColourHexCode;
    [SerializeField] Color hoverColour;
    string hoverColourHexCode;
 
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
        GameSettings.OnReadSpeedChange += GameSettings_OnReadSpeedChange;
    }

    private void OnDestroy()
    {
        OptionTextLinkHandler.OnOptionPicked -= OptionTextLinkHandler_OnOptionPicked;
        OptionTextLinkHandler.OnHoverOnOption -= OptionTextLinkHandler_OnHoverOnOption;
        OptionTextLinkHandler.OnHoverOffOption -= OptionTextLinkHandler_OnHoverOffOption;
        GameSettings.OnReadSpeedChange -= GameSettings_OnReadSpeedChange;
    }

    void Update()
    {      
        if (Input.GetMouseButtonDown(0) && 
            !historyController.IsShown &&
            !IsOverUI())
        {

            if (isReading)
            {
                isFastForward = true;
            }

            if (currentDialogue.NextDialogue != null && 
                !isReading)
            {
                StartCoroutine(PlayText(currentDialogue.NextDialogue));
            }
            if (currentDialogue.NextDialogue == null &&
                currentDialogue.DialogueOptions.Length == 0 &&
               !isReading)
            {
                
                SceneController.Instance.LoadNextScene(K.MenuScene);
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
        AudioController.Instance.PlayOneShot(nexDialogueSound);
        string buffer =  dialogue.DialogueText.Replace('\u2019', '\'');
        foreach (char character in buffer)
        {
            dialogueText.text += character;
            if (!isFastForward)
            {
                yield return new WaitForSeconds(textSpeed);
            }
        }
          
        dialogueText.text = buffer;
        int optionsIndex = 0;
        foreach (var option in dialogue.DialogueOptions)
        {
            dialogueText.text += '\n' + "  <color=#ff0000><i><link=\"" + optionsIndex +"\">"+ option.OptionText+ "</color></i></link>";
        }
        OnDialogueEnd?.Invoke(dialogue);
        currentText = dialogueText.text;
        isReading = false;
        isFastForward = false;
    }

    private void UpdateUI(Dialogue dialogue)
    {
        Debug.Log(leftNameText.text + " " + rightNameText.text);
        if (dialogue.LeftCharacterName != "")
        {
            leftNameText.text = dialogue.LeftCharacterName;
            leftNameTag.enabled = true;
        }
        else
        {
            leftNameText.text = "";
            leftNameTag.enabled = false;
        }
        if (dialogue.RightCharacterName != "")
        {
            rightNameText.text = dialogue.RightCharacterName;
            rightNameTag.enabled = true;
        }
        else
        {
            rightNameText.text = "";
            rightNameTag.enabled = false;
        }
        if(dialogue.LeftCharacterImage)
        {
            leftCharacter.enabled = true;
            leftCharacter.sprite = dialogue.LeftCharacterImage;
        }
        else
        {
            leftCharacter.enabled = false;
        }
        if (dialogue.RightCharacterImage)
        {
            rightCharacter.enabled = true;
            rightCharacter.sprite = dialogue.RightCharacterImage;
        }
        else
        {
            rightCharacter.enabled = false;
        }
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

    private void GameSettings_OnReadSpeedChange(float readingSpeed)
    {
        textSpeed = readingSpeed;
    }
}