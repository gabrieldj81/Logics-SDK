//Created by: Cake Logic
using System;
using UnityEngine;
using UnityEngine.Events;

namespace CakeLogicSDK {
[AddComponentMenu("Cake Logic/Logic's SDK/func_datacheck")]
public class func_datacheck : MonoBehaviour
{
    [SerializeField] private string key;
    [SerializeField] private string data;
    [SerializeField] private UnityEvent IfDataMatches;
    [SerializeField] private UnityEvent IfDataDoesNotMatch;
    private func_presistentdata dataManager;

    void Start()
    {
        dataManager = func_manager.Instance.GetDataManager();
    }

    public void CheckDataFromKey()
    {
       string tempData = dataManager.GetData(key);
       if(tempData != null && tempData == data)
       {
         IfDataMatches.Invoke();
       }
       else
       {
        IfDataDoesNotMatch.Invoke();
       }
    }
}
}