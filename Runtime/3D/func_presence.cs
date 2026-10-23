//Created by: Cake Logic
using UnityEngine;

namespace CakeLogicSDK.V3D {
[DisallowMultipleComponent]
[AddComponentMenu("Cake Logic/Logic's SDK/3D/func_presence")]
public class func_presence : MonoBehaviour
{
    public func_presencedetection TempObject = null;
    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            if(hit.transform.gameObject.tag == "PresenceDetection")
            {
               if(TempObject == null)
               {
                //Debug.Log("Looking At A Presence Detection Object");
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                TempObject = hit.transform.GetComponent<func_presencedetection>();
                TempObject.SetPresenceDetection(true);
                return;
               }
               if(hit.transform.GetComponent<func_presencedetection>() == TempObject)
               {
                //Debug.Log("Looking At A Presence Detection Object");
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                TempObject.SetPresenceDetection(true);
                return;
               }
               if(hit.transform.GetComponent<func_presencedetection>() != TempObject)
               {
                //Debug.Log("Switching TempObject");
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                TempObject.SetPresenceDetection(false);
                TempObject = null;
                TempObject = hit.transform.GetComponent<func_presencedetection>();
                return;
               }
            }
            else
            {
            if(TempObject != null)
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                TempObject.SetPresenceDetection(false);
                TempObject = null;
                return;
            }
         }
        }
        else
        {
            if(TempObject != null)
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                TempObject.SetPresenceDetection(false);
                TempObject = null;
                return;
            }
       }
    }
  }
}
