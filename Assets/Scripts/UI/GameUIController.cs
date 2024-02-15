using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIController : MonoBehaviour
{
    public void EndGame()
    {
        SceneController.Instance.LoadNextScene(K.MenuScene);
    }
}
