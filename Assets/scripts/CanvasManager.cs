using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager instance;
    public Text scoreText;
    public GameObject finishText;
    public Text finishScoreText;
    public Text finishTimerText;
    public Text recordsTimerText;
    public GameObject[] button;
    private int score = 0;
    private int finishScore;
    public float finishTimer;
    public GameObject records;
    public Text recordsScoreText;
    private int hightScore;
    private float hightTimer = 100f;

 
    private void Awake()
    {
        instance = this;
        if (PlayerPrefs.HasKey("SaveScore"))
        {
            hightScore = PlayerPrefs.GetInt("SaveScore");
        }
        if (PlayerPrefs.HasKey("SaveTime"))
        {
            hightTimer = PlayerPrefs.GetFloat("SaveTime");
        }
        finishText.SetActive(false);
        records.SetActive(false);
        scoreText.text = score.ToString();
        Event.OnScoreCoin.AddListener(ScoreCoin);
        Event.OnFinishTimer.AddListener(FinishTimer);
        Event.OnFinish.AddListener(Finish);
    }

    public void ScoreCoin(int coin)
    {
        score = score + coin;
        scoreText.text = ("Score: " + score.ToString());
    }

    public void Finish()
    {
        finishScore = score;
        finishText.SetActive(true);
        records.SetActive(true);
        instance.HightScore();
        instance.HightTime();
        finishScoreText.text = ("Ваши очки: " + finishScore.ToString());
        finishTimerText.text = ("Ваше время: " + finishTimer.ToString());
        recordsScoreText.text = ("Очки: " + hightScore.ToString());
        recordsTimerText.text = ("Время: " + hightTimer.ToString());
        button[0].SetActive(true);
        button[1].SetActive(true);
    }

    public void FinishTimer(float timer)
    {
        finishTimer = timer;
    }

    public void HightScore()
    {
        if (hightScore < score)
        {
            hightScore = score;
        }
        PlayerPrefs.SetInt("SaveScore", hightScore);
    }

    public void HightTime()
    {
        if (hightTimer > finishTimer)
        {
            hightTimer = finishTimer;
        }
        PlayerPrefs.SetFloat("SaveTime", hightTimer);
    }
}
