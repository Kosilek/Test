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
        questionPanel.SetActive(false);
    }
    public void KeyBoard()
    {
        optionSave = "keyboard";
    }

    public void ButtonGame()
    {
        optionSave = "button";
    }
    public void EasyDifficulty()
    {
        optionDifficutlySave = "easy";
    }

    public void NormalDifficulty()
    {
        optionDifficutlySave = "normal";
    }

    public void HardDifficulty()
    {
        optionDifficutlySave = "hard";
    }

    public void Back()
    {
        if (optionSave == PlayerPrefs.GetString("Option") && optionDifficutlySave == PlayerPrefs.GetString("Difficulty"))
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
        PlayerPrefs.SetString("Option", optionSave);
        PlayerPrefs.SetString("Difficulty", optionDifficutlySave);
    }
}
