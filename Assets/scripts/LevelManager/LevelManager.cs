using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject[] levelOnject;
    private int level;

    private void Awake()
    {
        DeactivityLevel();
        if (PlayerPrefs.HasKey("Level"))
        {
            level = PlayerPrefs.GetInt("Level");
        }
        levelOnject[level].SetActive(true);
    }

    public void Restart()
    {
        string scene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(scene);
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    private void DeactivityLevel()
    {
        levelOnject[0].SetActive(false);
        levelOnject[1].SetActive(false);
    }
}
