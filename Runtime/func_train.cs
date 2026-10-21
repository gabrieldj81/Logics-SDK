//Created by: Cake Logic
using System;
using UnityEngine;
using UnityEngine.Events;

namespace CakeLogicSDK {
[DisallowMultipleComponent]
[AddComponentMenu("Cake Logic/Logic's SDK/func_train")]
public class func_train : MonoBehaviour
{
    //"GET IN THE FUNC_TRAIN!!!" -Logic's Mod Guide
    public func_traintrack currentTrack;
    [SerializeField] private bool running;
    [SerializeField] private float speed = 5f;
    [SerializeField] private bool useEvent;
    [SerializeField] private UnityEvent OnDestinationReached;

    public void ClearTrainTrack()
    {
        currentTrack = null;
    }
    public void SetTrainTrack(func_traintrack newTrack)
    {
        currentTrack = newTrack;
    }
    public void StartTrain(bool yes)
    {
        running = yes;
    }
    public void SetTrainSpeed(float newSpeed) 
    {
        speed = newSpeed;
    }
    void Update()
    {
        try
        {
        if (transform.position == currentTrack.currentPosition)
        {
            if (currentTrack.nextTrainTrack != null)
            {
                currentTrack = currentTrack.nextTrainTrack;
                if(useEvent)
                {
                    OnDestinationReached.Invoke();
                }
            }
            if (currentTrack.nextTrainTrack == null)
            {
                currentTrack = null;
                if (useEvent)
                {
                    OnDestinationReached.Invoke();
                }
            }
        }
        if (currentTrack != null && running)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, currentTrack.currentPosition, step);
        }
        }
        catch
        {
            return;
        }
    }
  }
}