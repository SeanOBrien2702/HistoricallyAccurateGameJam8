using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class OptionTextLinkHandler : MonoBehaviour, IPointerClickHandler
{
    public static event System.Action<int> OnOptionPicked = delegate { };
    private TextMeshProUGUI textBox;
    private Canvas canvas;
    private Camera cameraToUse;

    private void Awake()
    {
        textBox = GetComponent<TextMeshProUGUI>();
        canvas = GetComponentInParent<Canvas>();

        if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
        {
            cameraToUse = null;
        }
        else
        {
            cameraToUse = canvas.worldCamera;
        }
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
}