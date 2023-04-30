using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsScripts : MonoBehaviour
{
    private string optionSave;
    private string optionDifficutlySave;

    [SerializeField] private GameObject questionPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject menuPanel;

    private void Start()
    {
        if (PlayerPrefsSave.HasKey(MeaningString.option))
        {
            optionSave = PlayerPrefsSave.GetString(MeaningString.option);
        }
        if (PlayerPrefsSave.HasKey(MeaningString.difficulty))
        {
            optionDifficutlySave = PlayerPrefsSave.GetString(MeaningString.difficulty);
        }
        questionPanel.SetActive(false);
    }
    public void KeyBoard()
    {
        optionSave = MeaningString.keyboard;
    }

    public void ButtonGame()
    {
        optionSave = MeaningString.button;
    }
    public void EasyDifficulty()
    {
        optionDifficutlySave = MeaningString.easy;
    }

    public void NormalDifficulty()
    {
        optionDifficutlySave = MeaningString.normal;
    }

    public void HardDifficulty()
    {
        optionDifficutlySave = MeaningString.hard;
    }

    public void Back()
    {
        if (optionSave == PlayerPrefsSave.GetString(MeaningString.option) && optionDifficutlySave == PlayerPrefsSave.GetString(MeaningString.difficulty))
        {
            settingsPanel.SetActive(false);
            menuPanel.SetActive(true);
        } else
        {
            questionPanel.SetActive(true);
        }
    }

    public void Save()
    {
        PlayerPrefsSave.SetString(MeaningString.option, optionSave);
        PlayerPrefsSave.SetString(MeaningString.difficulty, optionDifficutlySave);
    }
}
