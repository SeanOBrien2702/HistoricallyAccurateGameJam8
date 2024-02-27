using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanel : MonoBehaviour
{
    [SerializeField] CharacterInfo[] characters;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] Image portrait;
    int currentIndex = 0;

    void Start()
    {
        UPdateUI(0);
    }

    void UPdateUI(int index)
    {
        nameText.text = characters[index].Name;
        descriptionText.text = characters[index].Description;
        portrait.sprite = characters[index].Portrait;
    }

    public void ChagneIndex(bool isNext)
    {
        if(isNext)
        {
            currentIndex++;
            if(currentIndex >= characters.Length)
            {
                currentIndex = 0;
            }
        }
        else
        {
            currentIndex--;
            if(currentIndex < 0)
            {
                currentIndex = characters.Length - 1;
            }
        }
        UPdateUI(currentIndex);
    }

}

[Serializable]
public class CharacterInfo
{
    public string Name;
    public string Description;
    public Sprite Portrait;
}