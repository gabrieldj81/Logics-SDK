//Created by: Cake Logic
using System;
using UnityEngine;
using UnityEngine.Events;

namespace CakeLogicSDK {
[AddComponentMenu("Cake Logic/Logic's SDK/func_collidertrigger")]
public class func_collidertrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent OnEnter;
    [SerializeField] private UnityEvent OnLeave;
    [SerializeField] private UnityEvent OnStay;
    [SerializeField] private bool useStay = false;
    [SerializeField] private string interactionTag;
    public GameObject tempObject;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == interactionTag)
        {
            if(tempObject != collision.transform.gameObject)
            {
            tempObject = collision.transform.gameObject;
            }
            OnEnter.Invoke();
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == interactionTag)
        {
            if(tempObject != collision.transform.gameObject)
            {
            tempObject = collision.transform.gameObject;
            }
            OnLeave.Invoke();
        }
    }
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == interactionTag && useStay)
        {
            if(tempObject != collision.transform.gameObject)
            {
            tempObject = collision.transform.gameObject;
            }
            OnStay.Invoke();
        }
    }
  }
}