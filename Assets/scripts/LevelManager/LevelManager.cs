using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject levelObject1;
    public GameObject levelObject2;
    private int level;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("Level"))
        {
            level = PlayerPrefs.GetInt("Level");
        }
        ChoiceLevel(level);
    }

    public void Restart()
    {
        string scene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(scene);
    }
    public void Menu()
    {
        DestroyLevel(level);
        SceneManager.LoadScene("Menu");
    }

    private void ChoiceLevel(int level)
    {
        switch(level)
        {
            case 0:
                Instantiate(levelObject1);
                break;
            case 1:
                Instantiate(levelObject2);
                break;
        }
    }
    private void DestroyLevel(int level)
    {
        switch (level)
        {
            case 0:
                Instantiate(levelObject1);
                break;
            case 1:
                Instantiate(levelObject2);
                break;
        }
    }
}
