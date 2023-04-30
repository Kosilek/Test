using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singletone<LevelManager>
{
    public GameObject levelObject1;
    public GameObject levelObject2;
    public GameObject levelObject3;
    private int level;

    protected override void Awake()
    {
        base.Awake();
        if (PlayerPrefsSave.HasKey(MeaningString.level))
        {
            level = PlayerPrefsSave.GetInt(MeaningString.level);
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
        SceneManager.LoadScene(MeaningString.menu);
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
                case 2:
                Instantiate(levelObject3);
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
