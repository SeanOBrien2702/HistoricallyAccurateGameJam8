using FMODUnity;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonAudio : MonoBehaviour
{
    [SerializeField] EventReference buttonClick;
    Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        if (button.interactable)
        {
            AudioController.Instance.PlayOneShot(buttonClick);
        }
    }
}