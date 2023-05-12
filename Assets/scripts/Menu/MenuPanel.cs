using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour
{
    public GameObject button;
    public GameObject loadScene;

    private void Start()
    {
        CheckLevel();    
    }

    public void PlayLevel()
    {
        // SceneManager.LoadScene(MeaningString.sampleScene);
        Instantiate(loadScene, gameObject.transform.position, gameObject.transform.rotation);
        StartCoroutine(LoadScene.LoadAsync());
    }

    private void CheckLevel()
    {
        if (PlayerPrefs.HasKey(MeaningString.level))
        {
            button.GetComponent<Button>().interactable = true;
        }
        else
        {
            button.GetComponent<Button>().interactable = false;
        }
    }
}