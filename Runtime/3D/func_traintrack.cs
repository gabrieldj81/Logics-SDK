//Created by: Cake Logic
using System;
using UnityEngine;

namespace CakeLogicSDK.V3D {
[AddComponentMenu("Cake Logic/Logic's SDK/3D/func_traintrack")]
public class func_traintrack : MonoBehaviour
{
    public func_traintrack nextTrainTrack;
    public Vector3 currentPosition;
    public bool AlwaysUpdatePosition = false;
    [SerializeField] private bool AutoSetPosition = false;
   
    void Start()
    {
        if(AutoSetPosition)
        {
            currentPosition = transform.position;
        }
    }

    void Update()
    {
        if(AlwaysUpdatePosition)
        {
            currentPosition = transform.position;
        }
    }
  }
}