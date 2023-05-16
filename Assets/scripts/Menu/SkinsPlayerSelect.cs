using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinsPlayerSelect : MonoBehaviour
{
    int choicePack;
    public GameObject savePanel;
    public GameObject MainMenuPanel;

    public void SelectPack(int x)
    {
        CharacterSelection.SelectPack(x);
        choicePack = CharacterSelection.ChoiceSkin;
    }

    public void Save()
    {
        CharacterSelection.Save(MeaningString.playerSelect, choicePack);
    }
    public void Back()
    {
        if (choicePack != PlayerPrefs.GetInt(MeaningString.playerSelect))
        {
            savePanel.SetActive(true);
        }
        else if (choicePack == PlayerPrefs.GetInt(MeaningString.playerSelect))
        {
            MainMenuPanel.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
