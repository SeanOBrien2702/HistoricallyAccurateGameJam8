using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static event Action OnSceneChange = delegate { };

    public static SceneController Instance { get; private set; }
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

    public void LoadNextScene(string sceneName)
    {
        OnSceneChange?.Invoke();
        StartCoroutine(StartLoad(sceneName));
    }

    IEnumerator StartLoad(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            yield return new WaitForEndOfFrame();
        }
    }
}