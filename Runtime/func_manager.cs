//Created by: Cake Logic
using UnityEngine;
using System;

namespace CakeLogicSDK {
[DisallowMultipleComponent]
[AddComponentMenu("Cake Logic/Logic's SDK/func_manager")]
public class func_manager : MonoBehaviour
{
    public static func_manager Instance { get; private set; }
    private func_persistentdata dataManager;
    private func_musicmanager musicManager;
    public event Action OnSceneLoading;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        dataManager = gameObject.GetComponent<func_persistentdata>();
        if(dataManager == null)
        {
            gameObject.AddComponent<func_persistentdata>();
            dataManager = gameObject.GetComponent<func_persistentdata>();
        }
        musicManager = gameObject.GetComponent<func_musicmanager>();
        if(musicManager == null)
        {
          gameObject.AddComponent<func_musicmanager>();
          musicManager = gameObject.GetComponent<func_musicmanager>();
        }
        DontDestroyOnLoad(gameObject);
    }

    public void LoadingScene()
    {
        OnSceneLoading?.Invoke();
    }

    public func_persistentdata GetDataManager()
    {
        return dataManager;
    }

    public func_musicmanager GetMusicManager()
    {
        return musicManager;
    }
}
}
