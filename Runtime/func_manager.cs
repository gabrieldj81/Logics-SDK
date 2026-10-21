//Created by: Cake Logic
using UnityEngine;
using System;
namespace CakeLogicSDK {
[DisallowMultipleComponent]
[AddComponentMenu("Cake Logic/Logic's SDK/func_manager")]
public class func_manager : MonoBehaviour
{
    public static func_manager Instance { get; private set; }
    private func_presistentdata dataManager;
    public event Action OnSceneLoading;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        dataManager = gameObject.GetComponent<func_presistentdata>();
        if(dataManager == null)
        {
            gameObject.AddComponent<func_presistentdata>();
            dataManager = gameObject.GetComponent<func_presistentdata>();
        }
        DontDestroyOnLoad(gameObject);
    }

    public void LoadingScene()
    {
        OnSceneLoading?.Invoke();
    }

    public func_presistentdata GetDataManager()
    {
        return dataManager;
    }
}
}
