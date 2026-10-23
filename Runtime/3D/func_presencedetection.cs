//Created by: Cake Logic
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CakeLogicSDK.V3D {
[AddComponentMenu("Cake Logic/Logic's SDK/3D/func_presencedetection")]
public class func_presencedetection : MonoBehaviour
{
    [Tooltip("Specify whether or not to utilize the hold feature of the presence detector")] [SerializeField] private bool useHold;
    [Tooltip("Specify what happens when the func_presence looks at the presence detector")] [SerializeField] private UnityEvent OnPresenceDetection;
    [Tooltip("Specify what happens when the func_presence stops looking at the presence detector")] [SerializeField] private UnityEvent OnPresenceNull;
    [Tooltip("Specify what happens when the func_presence keeps looking at the presence detector")] [SerializeField] private UnityEvent OnPresenceHold;
    private bool ranOnce;
    private bool ranOnce2;
    private bool presenceDetected;

    private void Update()
    {
       if(presenceDetected)
       {
         if(!ranOnce)
         {
            OnPresenceDetection.Invoke();
            ranOnce = true;
            ranOnce2 = false;
         }
         if(useHold)
         {
            OnPresenceHold.Invoke();
         }
       }
       if(!presenceDetected)
       {
         if(!ranOnce2)
         {
            OnPresenceNull.Invoke();
            ranOnce = false;
            ranOnce2 = true;
         }
       }
    }
    public void SetPresenceDetection(bool set)
    {
      presenceDetected = set;
    }
  }
}
