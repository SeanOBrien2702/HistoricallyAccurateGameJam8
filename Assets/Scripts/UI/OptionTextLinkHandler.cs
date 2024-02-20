using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class OptionTextLinkHandler : MonoBehaviour, IPointerClickHandler
{
    public static event System.Action<int> OnOptionPicked = delegate { };
    public static event System.Action<int> OnHoverOnOption = delegate { };
    public static event System.Action OnHoverOffOption = delegate { };
    TextMeshProUGUI textBox;
    Canvas canvas;
    Camera cameraToUse;
    RectTransform rectTransform;
    int currentHoverIndex = -1;
    bool isHoveredOver = false;

    private void Awake()
    {
        textBox = GetComponent<TextMeshProUGUI>();
        canvas = GetComponentInParent<Canvas>();
        rectTransform = GetComponentInParent<RectTransform>();

        if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
        {
            cameraToUse = null;
        }
        else
        {
            cameraToUse = canvas.worldCamera;
        }
    }

    private void Update()
    {
        CheckForLinkAtMousePosition();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Vector3 mousePosition = new Vector3(eventData.position.x, eventData.position.y, 0);
        var linkTaggedText = TMP_TextUtilities.FindIntersectingLink(textBox, mousePosition, cameraToUse);

        if (linkTaggedText != -1)
        {
            OnOptionPicked?.Invoke(linkTaggedText);
        }
    }

    private void CheckForLinkAtMousePosition()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        bool isIntersectingRectTransform = TMP_TextUtilities.IsIntersectingRectTransform(rectTransform, mousePosition, cameraToUse);
        if (!isIntersectingRectTransform)
        {
            return;
        }

        int intersectingLink = TMP_TextUtilities.FindIntersectingLink(textBox, mousePosition, cameraToUse);
        if (currentHoverIndex != intersectingLink)
        {
            OnHoverOffOption?.Invoke();
            isHoveredOver = false;
        }

        if (intersectingLink == -1)
        {
            return;
        }
        if (!isHoveredOver)
        {
            isHoveredOver = true;
            TMP_LinkInfo linkInfo = textBox.textInfo.linkInfo[intersectingLink];
            currentHoverIndex = intersectingLink;
            OnHoverOnOption?.Invoke(currentHoverIndex);
        }
    }
}