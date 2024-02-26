using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuAudio : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image highlight;

    void Start()
    {
        highlight.enabled = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        highlight.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        highlight.enabled = false;
    }
}