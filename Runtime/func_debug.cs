//Created by: Cake Logic
using UnityEngine;

namespace CakeLogicSDK {
[AddComponentMenu("Cake Logic/Logic's SDK/func_debug")]
public class func_debug : MonoBehaviour
{
    public void callLog(string msg)
    {
        Debug.Log(msg);
    }
    public void callWarning(string msg)
    {
        Debug.LogWarning(msg);
    }
    public void callError(string msg)
    {
        Debug.LogError(msg);
    }
    public void throwExeption(string msg) 
    {
        throw new System.Exception(msg);
    }
    public void QuitApplication() 
    {
        Application.Quit();
    }
  }
}