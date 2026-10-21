//Created by: Cake Logic
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CakeLogicSDK {
[AddComponentMenu("Cake Logic/Logic's SDK/func_target")]
public class func_target : MonoBehaviour
{
    [Tooltip("Specify what happens when the target is being shot")] [SerializeField] private UnityEvent OnTargetHeld;
    [Tooltip("Specify what happens when the target gets shot")] [SerializeField] private UnityEvent OnTargetShot;
    private bool shotHeld;
    public void GetShot()
    {
      OnTargetShot.Invoke();
    }
    public void SetHeldFire(bool set)
    {
      shotHeld = set;
    }
    void Update()
    {
      if(shotHeld)
      {
        OnTargetHeld.Invoke();
      }
    }
  }
}
