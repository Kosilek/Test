using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ButtonCreate : MonoBehaviour
{
    public Transform[] spawnBlock;
    public Font font;
    private string[] textRecord = new string[2]; //= "Очки: 123\nВремя: 123";
    //List <string> textRecord = new List<string>();
    private int[] recordsScore = new int[2];
    //List <int> recordsScore = new List<int>();
    private float[] recordsTime = new float[2];
    //List <float> recordsTime = new List<float>();

    void Start()
    {
        TakeRecords();   
        CreateAndDestroy0_1_6(0);
    }

    private void TakeRecords()
    {
         Records(0, "SaveScore", "SaveTime");
        //Получение рекордов ост уровней;
    }

    private void Records(int i, string score, string time)
    {
        if (PlayerPrefs.HasKey(score))
        {
            recordsScore[i] = PlayerPrefs.GetInt(score);
        }
        if (PlayerPrefs.HasKey(time))
        {
            recordsTime[i] = PlayerPrefs.GetFloat(time);
        }
        textRecord[i] = $"Очки: {recordsScore[i]}\nВремя: {recordsTime[i]}";
    }

    private void Play()
    {
        CreateAndDestroy0_1_6(1);
        CreateAndDestroy2_3_4(0);
        CheckCoinLevel2(recordsScore[0]); // для каждого уровня отдельно далее переделать в один метод или придумать как сокр:
    }

    private void Option()
    {
        CreateAndDestroy0_1_6(1);
        CreateAndDestroy7_8(0);
    }

    private void KeyBoard()
    {
        PlayerPrefs.SetString("Option", "keyboard");
        CreateAndDestroy7_8(1);
        CreateAndDestroy0_1_6(0);
    }

    private void ButtonGame()
    {
        PlayerPrefs.SetString("Option", "button");
        CreateAndDestroy7_8(1);
        CreateAndDestroy0_1_6(0);
    }
    
    private void ExitGame()
    {
        Debug.Log("Exit");
    }

    private void Level1()
    {
        CreateAndDestroy2_3_4(1);
        DestroyText(2);
        CreateAndDestroy9_10_11(0);
        PlayerPrefs.SetInt("Level", 0);
    }

    private void Level2() 
    {
        CreateAndDestroy2_3_4(1);
        DestroyText(2);
        CreateAndDestroy9_10_11(0);
        PlayerPrefs.SetInt("Level", 1);
    }

    private void EasyDifficulty()
    {
        CreateAndDestroy9_10_11(1);
        PlayerPrefs.SetString("Difficulty", "easy");
        SceneManager.LoadScene("SampleScene");
    }

    private void NormalDifficulty()
    {
        CreateAndDestroy9_10_11(1);
        PlayerPrefs.SetString("Difficulty", "normal");
        SceneManager.LoadScene("SampleScene");
    }

    private void HardDifficulty()
    {
        CreateAndDestroy9_10_11(1);
        PlayerPrefs.SetString("Difficulty", "hard");
        SceneManager.LoadScene("SampleScene");
    }

    private void CheckCoinLevel2(int score)
    {
        if (score >= 20)
        {
            CreateText("recordsLevel2", textRecord[0], spawnBlock[5]);// изменить текс рекорд
            GameObject.Find("TextLevelTwo").GetComponent<Text>().color = Color.black;
            GameObject.Find("LevelTwo").GetComponent<Button>().interactable = true;
        } else if (score < 20)
        {
            CreateText("conditionsLevel2", "Для открытия уровня нужно набрать 20 очков на 1 уровне", spawnBlock[12]);
            GameObject.Find("TextLevelTwo").GetComponent<Text>().color = Color.red;
            GameObject.Find("LevelTwo").GetComponent<Button>().interactable = false;
        }
    }

    private void DestroyText(int i)
    {
        if (GameObject.Find($"recordsLevel{i}") != null)
        {
            Destroy(GameObject.Find($"recordsLevel{i}"));
        } else if (GameObject.Find($"conditionsLevel{i}") != null)
        {
            Destroy(GameObject.Find($"conditionsLevel{i}"));
        }
    }

    private void CreateAndDestroy0_1_6(int i)  //Метод отвечающий за обьекты 0, 1, 6;
    {
        switch(i)
        {
            case 0:
                CreateButton("Play", "PLAY", spawnBlock[0], Play);
                CreateButton("Option", "OPTION", spawnBlock[6], Option);
                CreateButton("Exit", "EXIT", spawnBlock[1], ExitGame);
                break;
            case 1:
                Destroy(GameObject.Find("Play"));
                Destroy(GameObject.Find("Option"));
                Destroy(GameObject.Find("Exit"));
                break;
        }
    }

    private void CreateAndDestroy2_3_4(int i) //метод отвечающий за обьекты 2, 3, 4, 5; (Чем больше уровней тем больше обьектов)
    {
        switch (i)
        {
            case 0:
                CreateButton("LevelOne", "Level One", spawnBlock[2], Level1);
                CreateText("recordsLevel1", textRecord[0], spawnBlock[4]);
                CreateButton("LevelTwo", "Level Two", spawnBlock[3], Level2);
              //  CreateText("recordsLevel2", textRecord[0], spawnBlock[5]);
                break;
            case 1:
                Destroy(GameObject.Find("LevelOne"));
                Destroy(GameObject.Find("recordsLevel1"));
                Destroy(GameObject.Find("LevelTwo"));
               // Destroy(GameObject.Find("recordsLevel2"));
                break;
        }
    }

    private void CreateAndDestroy7_8(int i) // метод отвечающий за обьекты 7, 8;
    {
        switch (i)
        {
            case 0:
                CreateButton("Keyboard", "KEYBOARD", spawnBlock[7], KeyBoard);
                CreateButton("Button", "BUTTON", spawnBlock[8], ButtonGame);
                break;
            case 1:
                Destroy(GameObject.Find("Keyboard"));
                Destroy(GameObject.Find("Button"));
                break;
        }
    }

    private void CreateAndDestroy9_10_11(int i) //метод отвечающий за обьекты 9, 10, 11;
    {
        switch (i)
        {
            case 0:
                CreateButton("Easy", "EASY", spawnBlock[9], EasyDifficulty);
                CreateButton("Normal", "NORMAL", spawnBlock[10], NormalDifficulty);
                CreateButton("Hard", "HARD", spawnBlock[11], HardDifficulty);
                break;
            case 1:
                Destroy(GameObject.Find("Easy"));
                Destroy(GameObject.Find("Normal"));
                Destroy(GameObject.Find("Hard"));
                break;
        }
    }

    private void CreateButton(string nameButton, string textButton, Transform spawnBlock, Action action)
    {
        GameObject newButton = new GameObject(nameButton, typeof(Image), typeof(Button), typeof(LayoutElement));
        newButton.transform.SetParent(spawnBlock);
        // newButton.GetComponent<LayoutElement>().minHeight = 35;
        newButton.GetComponent<RectTransform>().position = new Vector3(spawnBlock.position.x, spawnBlock.position.y, spawnBlock.position.z);
        RectTransform rtButton = newButton.GetComponent<RectTransform>();
        rtButton.anchorMin = new Vector2(0, 0);
        rtButton.anchorMax = new Vector2(1, 1);
        rtButton.anchoredPosition = new Vector2(0, 0);
        rtButton.sizeDelta = new Vector2(0, 0);
        CreateText($"Text{nameButton}", textButton, newButton.transform);
        newButton.GetComponent<Button>().onClick.AddListener(delegate { action(); });
    }

    private void CreateText(string nameText, string textText, Transform spawnBlock)
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
        newText.GetComponent<Text>().color = Color.black;
        newText.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
    }
}

   

