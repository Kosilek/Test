using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class LevelPanel : MonoBehaviour
{
  //  public Transform levelPanel;
    public Font font;
    public Transform panel;
    private void Start()
    {
        CreatButtonLevel(6); // изменить цифру при увеличении уровней (Создать доп панели или прокрутку при кол-во уровней превышающем длину экрана)
    }

    private void CreatButtonLevel(int level)
    {
        float levelPosition = -840f;
        for (int i = 0; i < level; i++)
        {
            CreateButton((i+1).ToString(), (i+1).ToString(), levelPosition, i);
            levelPosition += 240;
        }
    }

    private void StartLevel(int x)
    {
        PlayerPrefsSave.SetInt(MeaningString.level, x);
        SceneManager.LoadScene(MeaningString.sampleScene);
    }

    private bool CheckLevel(int x)
    {
        bool check = true;
        if (x != 0)
        {
            if (PlayerPrefsSave.HasKey(MeaningString.saveScore))
            {
                if (PlayerPrefsSave.GetInt($"{MeaningString.saveScore}{x - 1}") < 10)
                {
                    check = false;
                }
                else if (PlayerPrefsSave.GetInt($"{MeaningString.saveScore}{x - 1}") > 10)
                {
                    check = true;
                }
            }
            else check = false;
        }
        return check;
    }

    private void CreateButton(string nameButton, string textButton, /*Transform spawnBlock,*//* Action action,*/ float x, int level)
    {
        GameObject newButton = new GameObject(nameButton, typeof(Image), typeof(Button), typeof(LayoutElement));
        newButton.transform.SetParent(gameObject.transform);
        // newButton.GetComponent<LayoutElement>().minHeight = 35;
        newButton.GetComponent<RectTransform>().position = new Vector3(x, 0f, 0f);
        RectTransform rtButton = newButton.GetComponent<RectTransform>();
       rtButton.anchorMin = new Vector2(0.5f, 0.5f);
       rtButton.anchorMax = new Vector2(0.5f, 0.5f);
       rtButton.anchoredPosition = new Vector2(x, 0);
       rtButton.sizeDelta = new Vector2(160f, 160f);
        CreateText($"Text{nameButton}", textButton, newButton.transform, CheckLevel(level));
        if(CheckLevel(level))
        {
            newButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            newButton.GetComponent<Button>().interactable = false;
            CreatTextRequirements($"Requirements{level}", "Нужно набрать 20 очков на пред. уровне", panel, x);
        }
        newButton.GetComponent<Button>().onClick.AddListener(delegate { StartLevel(level); });
    }

    private void CreateText(string nameText, string textText, Transform spawnBlock, bool check)
    {
        GameObject newText = new GameObject(nameText, typeof(Text));
        newText.transform.SetParent(spawnBlock);
        newText.GetComponent<Text>().text = textText;
        newText.GetComponent<Text>().font = font;
        newText.GetComponent<Text>().resizeTextForBestFit = true;
        newText.GetComponent<Text>().resizeTextMaxSize = 100;
        newText.GetComponent<Text>().resizeTextMinSize = 14;
        RectTransform rt = newText.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0, 0);
        rt.anchorMax = new Vector2(1, 1);
        rt.anchoredPosition = new Vector2(0, 0);
        rt.sizeDelta = new Vector2(0, 0);
        if (check)
        {
            newText.GetComponent<Text>().color = Color.black;
        }
        else
        {
            newText.GetComponent<Text>().color = Color.red;
        }
        newText.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
    }

    private void CreatTextRequirements(string nameText, string textText, Transform spawnBlock, float x)
    {
        GameObject newText = new GameObject(nameText, typeof(Text));
        newText.transform.SetParent(spawnBlock);
        newText.GetComponent<Text>().text = textText;
        newText.GetComponent<Text>().font = font;
        newText.GetComponent<Text>().resizeTextForBestFit = true;
        newText.GetComponent<Text>().resizeTextMaxSize = 100;
        newText.GetComponent<Text>().resizeTextMinSize = 14;
        RectTransform rt = newText.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0.5f, 0.5f);
        rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.anchoredPosition = new Vector2(x, 0);
        rt.sizeDelta = new Vector2(160f, 160f);
        newText.GetComponent<Text>().color = Color.black;
        newText.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
    }
}
