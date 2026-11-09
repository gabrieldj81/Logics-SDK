using UnityEngine;
using System;

namespace CakeLogicSDK {
 [DisallowMultipleComponent]
 [AddComponentMenu("Cake Logic/Logic's SDK/func_dev")]
 public class func_dev : MonoBehaviour
 {
    [Header("Add this script to any GameObject you only want to appear in the editor or in debug mode.")]
    public string uselessDoNotTouch;

    void Start()
    {
        if(Application.isEditor || Debug.isDebugBuild)
        {
            return;
        }
        else
        {
            Destroy(gameObject);
        }
    }
 }
}