using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public static class Event
{
    public static UnityEvent<int> OnScoreCoin = new UnityEvent<int>();
    public static UnityEvent<float> OnFinishTimer = new UnityEvent<float>();
    public static UnityEvent OnFinish = new UnityEvent();

    public static void SendScoreCoin(int score)
    {
        OnScoreCoin.Invoke(score);
    }

    public static void SendFinishTimer(float timer)
    {
        OnFinishTimer.Invoke(timer);
    }

    public static void SendFinish()
    {
        OnFinish.Invoke();
    }

    
}
