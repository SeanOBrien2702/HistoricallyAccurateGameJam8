using UnityEngine;

public class CrossSceneUI : MonoBehaviour
{
    public static CrossSceneUI Instance { get; private set; }
    [SerializeField] GameObject settingsUI;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleSettings();
        }
    }

    public void ToggleSettings()
    {
        settingsUI.SetActive(!settingsUI.activeSelf);
    }

    public void MainMenu()
    {
        settingsUI.SetActive(false);
        SceneController.Instance.LoadNextScene(K.MenuScene);
    }
}