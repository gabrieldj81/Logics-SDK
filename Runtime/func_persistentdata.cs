//Created by: Cake Logic
using UnityEngine;
using System;
using System.Linq;

namespace CakeLogicSDK {
[DisallowMultipleComponent]
[AddComponentMenu("Cake Logic/Logic's SDK/func_persistentdata")]
public class func_persistentdata : MonoBehaviour
{
    //This script utilizes array indexing alongside two arrays to easily store and retrieve data

    //Keys essentially acts like a records book that says where certain data is located
    [SerializeField] private string[] keys = new string[1];
    //Datas is the data recorded by the Keys database
    [SerializeField] private string[] datas = new string[1];
    
    public void SetData(string key, string data)
    {
      if(key == null || key == "")
      {
        //Setting the record to null or nothing is not possible as either is used as a free space indicator
        //and saving data to free space is essentially like shouting into a void and expecting it to remember.
        throw new ArgumentException("Failed to Save! Provided Key Cannot be null or nothing.");
      }
      if(data == null)
      {
        //Setting the data to null breaks GetSave() as null is used to say that an error has occured
        throw new ArgumentException("Failed to Save! Provided Data Cannot be null.");
      }
      if(keys.Length == 0 || keys == null)
      {
       ResizeArrays(1);
      }
      if(FindItemByName(key) != -1)
      {
        datas[FindItemByName(key)] = data;
        return;
      }
      for (int i = 0; i < keys.Length; i++)
      {
        if(keys[i] == "" || keys[i] == null)
        {
         keys[i] = key;
         datas[i] = data;
         i = 0;
         return;
        }
      }
      ResizeArrays(keys.Length + 1);
      keys[keys.Length - 1] = key;
      datas[keys.Length - 1] = data;
    }
    public string GetData(string key)
    {
      string setData = null;
      if(key == null || key == "")
      {
        //Providing no key whatsoever is like asking a hotel manager for a room that doesn't exist.
        throw new ArgumentException("Failed to Get! Provided Key Cannot be null or nothing.");
      }
      if(FindItemByName(key) != -1)
      {
        setData = datas[FindItemByName(key)];
        return setData;
      }
      else
      {
        Debug.LogError("Failed to Get! Key not in Database!");
        return null;
      }
    }
    public void ClearData(string deletedKey)
    {
      int tempId = 0;
      if(deletedKey == null || deletedKey == "")
      {
        //Trying to delete a data entry that can't exist is pointless
        throw new ArgumentException("Failed to Clear! Provided Key Cannot be null or nothing.");
      }
      if(FindItemByName(deletedKey) != -1)
      {
        tempId = FindItemByName(deletedKey);
        keys[tempId] = "";
        datas[tempId] = "";
        return;
      }
      else{
        //Trying to delete a data entry that doesn't exist is also pointless
        Debug.LogError("Failed to Clear! Key not in Database!");
        return;
      }
    }
    public bool CheckIfKeyExists(string keyName)
    {
      //A dedicated function to tell you if a key exists
      if(FindItemByName(keyName) != -1)
      {
        return true;
      }
      else
      {
        return false;
      }
    }
    private int FindItemByName(string searchName)
    {
     try
     {
      return Array.IndexOf(keys, searchName);
     }
     catch
     {
      return -1;
     }
    }
    public void OptimizeArrays()
    {
      int i = 0;
      int x = 0;
      string tempKey;
      string tempData;
      for (i = 0; i < keys.Length; i++)
      {
        if(keys[i] == "" || keys[i] == null)
        {
          if(i != keys.Length - 1)
          {
          if(keys[i + 1] != null && keys[i + 1] != "")
          {
            tempKey = keys[i + 1];
            tempData = datas[i + 1];
            keys[i + 1] = "";
            datas[i + 1] = "";
            keys[i - x] = tempKey;
            datas[i - x] = tempData;
          }
          else
          {
            x++;
          }
          }
          else
          {
            x++;
          }
        }
      }
      ResizeArrays(keys.Length - x);
      return;
    }
    private void ResizeArrays(int Length)
    {
      Array.Resize(ref keys, Length);
      Array.Resize(ref datas, Length);
    }
  }
}