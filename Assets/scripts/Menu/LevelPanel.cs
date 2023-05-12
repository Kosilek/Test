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
    public GameObject content;
    public Transform scorePanel;
    public GameObject loadScene;
    public GameObject levelPanelScroll;
    public Transform levelPanel;
    [SerializeField] private int level;
    private float levelPositionX;
    private string[] textRecord;
    private int[] recordsScore;
    private float[] recordsTime;
    private void Awake()
    {
        ExtensionParetPanel();
        CoordinateCalculationX();
        textRecord = new string[level];
        recordsScore = new int[level];
        recordsTime = new float[level];
        CreatLevel(level);
        Debug.Log("Создание уровней");
    }

    private void ExtensionParetPanel()
    {
        content.GetComponent<RectTransform>().sizeDelta = new Vector2(-1 *(level * 240f) - 1920f, 460f);
    }

    private void CoordinateCalculationX()
    {
        if (level * 240f <= 1920f)
        {
            levelPositionX = -840f;
        }
        else if (level * 240f > 1920f)
        {
            levelPositionX = -1 * ((level * 240f) / 2 - 120f);
        }
    }

    private void CreatLevel(int level)
    {
        for (int i = 0; i < level; i++)
        {
            CreateButton((i+1).ToString(), (i+1).ToString(), levelPanel, levelPositionX, i);
            string str = TakeRecords(i);
            CreateText("1", str, scorePanel, false, 1);
            levelPositionX += 240;
        }
    }

    public void StartLevel(int x)
    {
        PlayerPrefsSave.SetInt(MeaningString.level, x);
        levelPanelScroll.SetActive(false);
        Instantiate(loadScene, gameObject.transform.position, gameObject.transform.rotation);
         StartCoroutine(LoadScene.LoadAsync(MeaningString.sampleScene));
        
    }

    private string TakeRecords(int i)
    {
        return Records(0, $"SaveScore{i}", $"SaveTime{i}");
        //Получение рекордов ост уровней;
    }

    private string Records(int i, string score, string time)
    {
        if (PlayerPrefsSave.HasKey(score))
        {
            recordsScore[i] = PlayerPrefsSave.GetInt(score);
        }
        else
        {
            recordsScore[i] = 0;
        }
        if (PlayerPrefsSave.HasKey(time))
        {
            recordsTime[i] = PlayerPrefsSave.GetFloat(time);
        }
        else recordsTime[i] = 0;
        //      if (recordsScore[i] == null && recordsTime[0] == null)
        return textRecord[i] = $"Очки: {recordsScore[i]}\nВремя: {recordsTime[i]}";
    }

    private bool CheckLevel(int x)
    {
        bool check = true;
        if (x != 0)
        {
            if (PlayerPrefsSave.HasKey(MeaningString.saveScore))
            {
                if (PlayerPrefsSave.GetInt($"{MeaningString.saveScore}{x - 1}") < 20)
                {
                    check = false;
                }
                else if (PlayerPrefsSave.GetInt($"{MeaningString.saveScore}{x - 1}") > 20)
                {
                    check = true;
                }
            }
            else check = false;
        }
        return check;
    }

    private void CreateButton(string nameButton, string textButton, Transform spawnBlock, float x, int level)
    {
        GameObject newButton = new GameObject(nameButton, typeof(Image), typeof(Button), typeof(LayoutElement));
        newButton.transform.SetParent(spawnBlock.transform);
        // newButton.GetComponent<LayoutElement>().minHeight = 35;
        newButton.GetComponent<RectTransform>().position = new Vector3(x, 0f, 0f);
        RectTransform rtButton = newButton.GetComponent<RectTransform>();
       rtButton.anchorMin = new Vector2(0.5f, 0.5f);
       rtButton.anchorMax = new Vector2(0.5f, 0.5f);
       rtButton.anchoredPosition = new Vector2(x, 0);
       rtButton.sizeDelta = new Vector2(160f, 160f);
        CreateText($"Text{nameButton}", textButton, newButton.transform, CheckLevel(level), 0);
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

    private void CreateText(string nameText, string textText, Transform spawnBlock, bool check, int checkObject)
    {
        GameObject newText = new GameObject(nameText, typeof(Text));
        newText.transform.SetParent(spawnBlock);
        newText.GetComponent<Text>().text = textText;
        newText.GetComponent<Text>().font = font;
        newText.GetComponent<Text>().resizeTextForBestFit = true;
        newText.GetComponent<Text>().resizeTextMaxSize = 100;
        newText.GetComponent<Text>().resizeTextMinSize = 14;
        if (checkObject == 0)
        {
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
        } else if (checkObject == 1)
        {
            newText.GetComponent<RectTransform>().position = new Vector3(levelPositionX, 0f, 0f);
            RectTransform rtButton = newText.GetComponent<RectTransform>();
            rtButton.anchorMin = new Vector2(0.5f, 0.5f);
            rtButton.anchorMax = new Vector2(0.5f, 0.5f);
            rtButton.anchoredPosition = new Vector2(levelPositionX, 0);
            rtButton.sizeDelta = new Vector2(160f, 160f);
            newText.GetComponent<Text>().color = Color.black;
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
