using FMODUnity;
using System.Collections;
using UnityEngine;

public class GameUIController : MonoBehaviour
{
    [SerializeField] Transform historyButton;
    [SerializeField] Transform historyPanel;

    [SerializeField] Transform raisedPosition;
    [SerializeField] Transform loweredPotation;

    [SerializeField] EventReference openHistorySound;
    [SerializeField] EventReference closeHistorySound;

    float historySpeed = 0.4f;
    bool isLowered = true;
    bool isMoving = false;

    public void EndGame()
    {
        SceneController.Instance.LoadNextScene(K.MenuScene);
    }

    public void ToggleSettings()
    {
        CrossSceneUI.Instance.ToggleSettings();
    }

    public void ToggleHistory()
    {
        if(isMoving)
        {
            return;
        }
        isMoving = true;
       
        Transform position = isLowered ? raisedPosition : loweredPotation;
        StartCoroutine(LerpPosition(position.position, historyPanel, historySpeed));
        StartCoroutine(LerpPosition(position.position, historyButton, historySpeed));
        isLowered = !isLowered;
        if (isLowered)
        {

            AudioController.Instance.PlayOneShot(openHistorySound);
        }
        else
        {
            AudioController.Instance.PlayOneShot(closeHistorySound);
        }
    }

    IEnumerator LerpPosition(Vector3 targetPosition, Transform item, float duration)
    {
        float time = 0;
        Vector3 startPosition = item.position;
        while (time < duration)
        {
            item.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        item.position = targetPosition;
        isMoving = false;
    }
}