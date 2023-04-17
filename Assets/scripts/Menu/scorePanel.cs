using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class scorePanel : MonoBehaviour
{
    public Font font;
    private string[] textRecord = new string[6]; //= "Очки: 123\nВремя: 123";(Размер зависим от кол-ва уровней)
    private int[] recordsScore = new int[6];
    private float[] recordsTime = new float[6];

    private void Start()
    {
        CreatTextLevel(6); // изменить цифру при увеличении уровней (Создать доп панели или прокрутку при кол-во уровней превышающем длину экрана)
    }
    
    private string TakeRecords(int i)
    {
        return Records(0, $"SaveScore{i}", $"SaveTime{i}");
        //Получение рекордов ост уровней;
    }

    private string Records(int i, string score, string time)
    {
        if (PlayerPrefs.HasKey(score))
        {
            recordsScore[i] = PlayerPrefs.GetInt(score);
        }
        else
        {
            recordsScore[i] = 0;
        }
        if (PlayerPrefs.HasKey(time))
        {
            recordsTime[i] = PlayerPrefs.GetFloat(time);
        }
        else recordsTime[i] = 0;
  //      if (recordsScore[i] == null && recordsTime[0] == null)
        return textRecord[i] = $"Очки: {recordsScore[i]}\nВремя: {recordsTime[i]}";
    }
    private void CreatTextLevel(int level)
    {
        float levelPosition = -840f;
        for (int i = 0; i < level; i++)
        {
            string str = TakeRecords(i);
            CreateText("1", str, levelPosition);
            levelPosition += 240;
        }
    }
    private void CreateText(string nameText, string textText, float x)
    {
        GameObject newText = new GameObject(nameText, typeof(Text));
        newText.transform.SetParent(gameObject.transform);
        newText.GetComponent<Text>().text = textText;
        newText.GetComponent<Text>().font = font;
        newText.GetComponent<Text>().resizeTextForBestFit = true;
        newText.GetComponent<Text>().resizeTextMaxSize = 100;
        newText.GetComponent<Text>().resizeTextMinSize = 14;
        newText.GetComponent<RectTransform>().position = new Vector3(x, 0f, 0f);
        RectTransform rtButton = newText.GetComponent<RectTransform>();
        rtButton.anchorMin = new Vector2(0.5f, 0.5f);
        rtButton.anchorMax = new Vector2(0.5f, 0.5f);
        rtButton.anchoredPosition = new Vector2(x, 0);
        rtButton.sizeDelta = new Vector2(160f, 160f);
        newText.GetComponent<Text>().color = Color.black;
        newText.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
    }
}
