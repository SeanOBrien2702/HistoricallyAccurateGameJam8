using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUIController : MonoBehaviour
{
    [SerializeField] GameObject creditsPanel;

    public void StartGame()
    {
        SceneController.Instance.LoadNextScene(K.GameScene);
    }

    public void Settigns()
    {
        CrossSceneUI.Instance.ToggleSettings();
    }

    public void ToggleCredits()
    {
        creditsPanel.SetActive(!creditsPanel.activeSelf);
    }
}