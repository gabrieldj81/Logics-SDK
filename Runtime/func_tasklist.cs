//Created by: Cake Logic
using UnityEngine;
using UnityEngine.Events;
using System;

namespace CakeLogicSDK {
[AddComponentMenu("Cake Logic/Logic's SDK/func_tasklist")]
public class func_tasklist : MonoBehaviour
{
   [SerializeField] private bool RunOnStart;
   [SerializeField] private UnityEvent TaskList;
   void Start()
   {
     if(RunOnStart)
     {
       TaskList.Invoke();
     }
   }
   public void RunTaskList()
   {
     TaskList.Invoke();
   }
 }
}
