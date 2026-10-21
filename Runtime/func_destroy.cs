//Created by: Cake Logic
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CakeLogicSDK {
[AddComponentMenu("Cake Logic/Logic's SDK/func_destroy")]
public class func_destroy : MonoBehaviour
{
   [Tooltip("Specify which GameObject to destroy")] [SerializeField] private GameObject DestroyedObject;
   [Tooltip("Specify whether or not the gameobject should destroy itself if the 'DestroyedObject' variable is null")] [SerializeField] private bool destroyItselfIfVarNull;
   public void SetDestroyedObject(GameObject destroyable)
   {
     DestroyedObject = destroyable;
   }
   public void DestroyObject()
   {
    if(DestroyedObject != null)
    {
    Destroy(DestroyedObject);
    DestroyedObject = null;
    }
    else
    {
      if(destroyItselfIfVarNull)
      {
      Destroy(gameObject);
      }
    }
   }
  }
}
