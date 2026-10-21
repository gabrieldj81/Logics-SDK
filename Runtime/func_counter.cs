//Created by: Cake Logic
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CakeLogicSDK {
[AddComponentMenu("Cake Logic/Logic's SDK/func_counter")]
public class func_counter : MonoBehaviour
{
    [Tooltip("Specify the initial amount of the counter")] [SerializeField] private int counter;
    [Tooltip("Specify whether or not to use the goal feature of the counter")] [SerializeField] private bool UseGoal;
    [Tooltip("Specify the number at which the goal is reached")] [SerializeField] private int goal;
    [Tooltip("Specify what happens when the goal is reached")] [SerializeField] private UnityEvent OnGoalReached;
    
    public void Increase (int num)
    {
      counter = counter + num;
      if(UseGoal)
      {
        CheckGoal();
      }
    }
    public void Decrease (int num)
    {
        counter = counter - num;
        if(UseGoal)
        {
         CheckGoal();
        }
    }
    public void Reset()
    {
        counter = 0;
        if(UseGoal)
        {
         CheckGoal();
        }
    }
    void CheckGoal()
    {
        if(counter == goal && UseGoal)
        {
          OnGoalReached.Invoke();
        }
    }
  }
}
