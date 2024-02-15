using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUIController : MonoBehaviour
{
    public void StartGame()
    {
        SceneController.Instance.LoadNextScene(K.GameScene);
    }
}
