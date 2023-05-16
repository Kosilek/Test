using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : Singletone<CanvasManager>
{
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

 
    protected override void Awake()
    {
        base.Awake();
        if (PlayerPrefsSave.HasKey($"{MeaningString.saveScore}{PlayerPrefsSave.GetInt(MeaningString.level)}"))
        {
            hightScore = PlayerPrefsSave.GetInt($"{MeaningString.saveScore}{PlayerPrefsSave.GetInt(MeaningString.level)}");
        }
        if (PlayerPrefsSave.HasKey($"{MeaningString.saveTime}{PlayerPrefsSave.GetInt(MeaningString.level)}"))
        {
            hightTimer = PlayerPrefsSave.GetFloat($"{MeaningString.saveTime}{PlayerPrefsSave.GetInt(MeaningString.level)}");
        }
        finishText.SetActive(false);
        records.SetActive(false);
        scoreText.text = score.ToString();
        Event.OnScoreCoin.AddListener(ScoreCoin);
        Event.OnFinishTimer.AddListener(FinishTimer);
        Event.OnFinish.AddListener(Finish);
    }

    private void Start()
    {
        Debug.Log(PlayerPrefsSave.GetInt(MeaningString.level));
        Debug.Log($"{MeaningString.saveScore}{PlayerPrefsSave.GetInt(MeaningString.level)}");
        Debug.Log("������ ����");
        Debug.Log(PlayerPrefsSave.GetInt($"{MeaningString.saveScore}{PlayerPrefsSave.GetInt(MeaningString.level)}"));
        Debug.Log($"���� = {MeaningString.saveScore}{PlayerPrefsSave.GetInt(MeaningString.level)}");
        Debug.Log($"���� ����� {MeaningString.saveTime}{PlayerPrefsSave.GetInt(MeaningString.level)}");
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
        HightScore();
        HightTime();
        //instance.HightScore();
       // instance.HightTime();
        finishScoreText.text = ("���� ����: " + finishScore.ToString());
        finishTimerText.text = ("���� �����: " + finishTimer.ToString());
        recordsScoreText.text = ("����: " + hightScore.ToString());
        recordsTimerText.text = ("�����: " + hightTimer.ToString());
        button[0].SetActive(true);
        button[1].SetActive(true);
       // Debug.Log($"score = {score} hscore = {hightScore}");
    }

    public void FinishTimer(float timer)
    {
        finishTimer = timer;
        Debug.Log($"timer = {finishTimer}");
    }

    private void HightScore()
    {
        if (hightScore < score)
        {
            hightScore = score;
            Debug.Log($"score = {score} hscore = {hightScore}");
        }
        Debug.Log($"���� = {MeaningString.saveScore}{PlayerPrefsSave.GetInt(MeaningString.level)}");
        PlayerPrefs.SetInt($"{MeaningString.saveScore}{PlayerPrefsSave.GetInt(MeaningString.level)}", hightScore);
    }

    private void HightTime()
    {
        if (hightTimer > finishTimer)
        {
            hightTimer = finishTimer;
        }
        Debug.Log($"���� ����� {MeaningString.saveTime}{PlayerPrefsSave.GetInt(MeaningString.level)}");
        PlayerPrefs.SetFloat($"{MeaningString.saveTime}{PlayerPrefsSave.GetInt(MeaningString.level)}", hightTimer);
    }
}
