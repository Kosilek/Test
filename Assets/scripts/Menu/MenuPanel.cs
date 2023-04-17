using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour
{
    public GameObject button;

    private void Start()
    {
        CheckLevel();    
    }

    public void PlayLevel()
    {
            SceneManager.LoadScene("SampleScene");
    }

    private void CheckLevel()
    {
        if (PlayerPrefs.HasKey("Level"))
        {
            button.GetComponent<Button>().interactable = true;
        }
        else
        {
            button.GetComponent<Button>().interactable = false;
        }
    }
}