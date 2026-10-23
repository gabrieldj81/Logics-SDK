//Created by: Cake Logic
using System;
using UnityEngine;
using UnityEngine.Events;

namespace CakeLogicSDK {
[AddComponentMenu("Cake Logic/Logic's SDK/func_timer")]
public class func_timer : MonoBehaviour
{
    [SerializeField] private float timeRemaining = 60;
    [SerializeField] private bool timerIsRunning = false;
    public UnityEvent OnTimerEnd;
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                OnTimerEnd.Invoke();
            }
        }
    }
    public void SetTimer(float set)
    {
        timeRemaining = set;
    }
    public void StartTimer(bool set) 
    {
        timerIsRunning = set;
    }
}
}