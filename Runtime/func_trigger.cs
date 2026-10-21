//Created by: Cake Logic
using System;
using UnityEngine;
using UnityEngine.Events;

namespace CakeLogicSDK {
[DisallowMultipleComponent]
[AddComponentMenu("Cake Logic/Logic's SDK/func_trigger")]
public class func_trigger : MonoBehaviour
{
    public bool useStay = false;
    public GameObject tempObject;
    [SerializeField] private UnityEvent OnEnter;
    [SerializeField] private UnityEvent OnLeave;
    [SerializeField] private UnityEvent OnStay;
    [SerializeField] private string interactionTag;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == interactionTag)
        {
            if(tempObject != other.transform.gameObject)
            {
            tempObject = other.transform.gameObject;
            }
            OnEnter.Invoke();
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == interactionTag)
        {
            if(tempObject != other.transform.gameObject)
            {
            tempObject = other.transform.gameObject;
            }
            OnLeave.Invoke();
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == interactionTag && useStay)
        {
            if(tempObject != other.transform.gameObject)
            {
            tempObject = other.transform.gameObject;
            }
            OnStay.Invoke();
        }
    }
  }
}