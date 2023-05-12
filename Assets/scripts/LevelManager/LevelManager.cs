using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LevelManager : Singletone<LevelManager>
{
    public GameObject levelObject1;
    public GameObject levelObject2;
    public GameObject levelObject3;
    public GameObject player;
    public GameObject loadScene;
    public static GameObject playerSave;
    private int level;

    protected override void Awake()
    {
        playerSave = player;
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
        CntrButton(scene);
    }
    public void Menu()
    {
      //  DestroyLevel(level);
        CntrButton(MeaningString.menu);
    }

    private void CntrButton(string scene)
    {
        StartCoroutine(LoadScene.LoadAsync(scene));
        Instantiate(loadScene, gameObject.transform.position, gameObject.transform.rotation);
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
   /* private void DestroyLevel(int level)
    {
        switch (level)
        {
            case 0:
                Destroy(levelObject1);
                break;
            case 1:
                Destroy(levelObject2);
                break;
        }
    }*/
}
