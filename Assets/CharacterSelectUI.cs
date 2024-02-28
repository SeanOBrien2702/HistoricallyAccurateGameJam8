using UnityEngine;

public class CharacterSelectUI : MonoBehaviour
{
    float normalHeight = 270;
    float characterSelectHeight = 530;
    float dialogueBoxWidth = -250;
    int index = 0;
    [SerializeField] GameObject historyButton;
    [SerializeField] GameObject nameTag;

    void Start()
    {
        historyButton.SetActive(false);
        nameTag.SetActive(false);
        DialogueController.OnNewDialogue += DialogueController_OnNewDialogue;
    }

    private void OnDestroy()
    {
        DialogueController.OnNewDialogue -= DialogueController_OnNewDialogue;
    }

    private void DialogueController_OnNewDialogue(Dialogue dialogue)
    {
        index++;
        if(index == 2)
        {
            GetComponent<RectTransform>().sizeDelta = new Vector2(dialogueBoxWidth, characterSelectHeight);
        }
        if (index == 3)
        {
            GetComponent<RectTransform>().sizeDelta = new Vector2(dialogueBoxWidth, normalHeight);
            nameTag.SetActive(true);
            historyButton.SetActive(true);
        }
    }
}