//Created by: Cake Logic
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CakeLogicSDK.V3D {
[AddComponentMenu("Cake Logic/Logic's SDK/3D/func_gun")]
public class func_gun : MonoBehaviour
{
  [SerializeField] private Transform playerCamera;

  [SerializeField] private Transform barrelEndPoint;

  [SerializeField] private LayerMask layerMask;

  [SerializeField] private InputActionReference triggerAction;

  [SerializeField] private LineRenderer lineRenderer;

  [SerializeField] private AudioSource gunShootSFX;

  [SerializeField] private AudioSource gunNoAmmoSFX;

  [SerializeField] private func_timer fireRateTimer;

  [SerializeField] private func_timer bulletTravelTimer;

  [SerializeField] private Animator animator;

  [SerializeField] private float fireRate;

  [SerializeField] private bool holdFire;

  private bool canFire = true;

  private bool hittingSomething;

  private Vector3 hitPoint;

  private Vector3 barrelDirection;

  private Vector3 cameraDirection;

  private bool triggerPressed;

  private func_target oof;

   void Start()
   {
    if(lineRenderer != null)
    {
      lineRenderer.startWidth = 0.02f;
      lineRenderer.endWidth = 0.02f;
      lineRenderer.positionCount = 2;
    }
   }
    void OnEnable()
    {
        if(bulletTravelTimer != null)
        {
          bulletTravelTimer.OnTimerEnd.AddListener(SetLineVisibility);
        }
        if(fireRateTimer != null)
        {
          fireRateTimer.OnTimerEnd.AddListener(SetFireAbility);
        }
    }

    void OnDisable()
    {
        if(bulletTravelTimer != null)
        {
          bulletTravelTimer.OnTimerEnd.RemoveListener(SetLineVisibility);
        }
        if(fireRateTimer != null)
        {
          fireRateTimer.OnTimerEnd.RemoveListener(SetFireAbility);
        }
    }

   void Update()
   {
      UpdateRaycast();
      UpdateInput();
      UpdateVisuals();
   }

   void UpdateRaycast()
   {
    cameraDirection = playerCamera.TransformDirection(Vector3.forward);
      RaycastHit hit;
         if (Physics.Raycast(playerCamera.position, cameraDirection, out hit, Mathf.Infinity, layerMask))
           {
              try
              {
                if(hit.collider.gameObject.GetComponent<func_target>() != oof)
                {
                if(oof != null)
                {
                 oof.SetHeldFire(false);
                }
                oof = hit.collider.gameObject.GetComponent<func_target>();
                }
              }
              catch
              {
               if(oof != null)
               {
                oof.SetHeldFire(false);
               }
               oof = null;
               Debug.Log("No Target to Shoot");
              }
              hittingSomething = true;
              hitPoint = hit.point;
           }
           else
           {
              if(oof != null)
              {
                oof.SetHeldFire(false);
              }
              hittingSomething = false;
              oof = null;
           }
   }

   void UpdateInput()
   {
         if(!holdFire)
         {
           triggerPressed = triggerAction.action.WasPressedThisFrame();
         }
         if(holdFire)
         {
           triggerPressed = triggerAction.action.IsPressed();
         }
           if (triggerPressed && !holdFire)
           {
            if (hittingSomething)
            {
               Shoot(false);
            }
            else
            {
               Shoot();
            }
           }
           if(triggerPressed && holdFire)
           {
             if(hittingSomething)
             {
               Shoot(true);
             }
             else
             {
               Shoot();
             }
           }
           if(!triggerPressed && holdFire)
           {
            StopHoldingFire();
           }
   }

   void UpdateVisuals()
   {
      barrelDirection = barrelEndPoint.TransformDirection(Vector3.forward);
      if(lineRenderer != null)
      {
       lineRenderer.SetPosition(0, barrelEndPoint.position);
      }
      if(hittingSomething)
      {
        if(lineRenderer != null)
        {
         lineRenderer.SetPosition(1, hitPoint);
        }
      }
      else
      {
         Vector3 barrelOutwardsPos = barrelEndPoint.position + (barrelEndPoint.forward * 50f);
         if(lineRenderer != null)
         {
         lineRenderer.SetPosition(1, barrelOutwardsPos);
         }
      }
   }

   void Shoot()
   {
     if(canFire && !holdFire)
     {
     //Version of shooting that does not hit anything
     if(gunShootSFX != null)
     {
     gunShootSFX.Play();
     }
     if(bulletTravelTimer != null)
     {
     bulletTravelTimer.SetTimer(0.01f);
     bulletTravelTimer.StartTimer(true);
     if(lineRenderer != null)
     {
     lineRenderer.enabled = true;
     }
     }
     if(fireRateTimer != null)
     {
     fireRateTimer.SetTimer(fireRate);
     fireRateTimer.StartTimer(true);
     canFire = false;
     }
     if(animator != null)
     {
     animator.SetTrigger("Fire");
     }
     Debug.Log("Hit Nothing.");
     }
     if(canFire && holdFire)
     {
      //Version of shooting that does not hit anything
     if(gunShootSFX != null)
     {
      if(!gunShootSFX.isPlaying)
      {
        gunShootSFX.Play();
      }
     }
     if(lineRenderer != null)
     {
     lineRenderer.enabled = true;
     }
     if(animator != null)
     {
     animator.SetTrigger("Fire");
     }
     Debug.Log("Hit Nothing.");
     }
   }
   
   void Shoot(bool cool)
   {
    if(canFire)
    {
      if(!holdFire)
      {
    //Version of shooting that does hit something
    if(oof != null)
    {
    oof.GetShot();
    }
    if(gunShootSFX != null)
    {
    gunShootSFX.Play();
    }
    if(lineRenderer != null)
    {
    lineRenderer.enabled = true;
    }
    if(bulletTravelTimer != null)
    {
    bulletTravelTimer.SetTimer(0.01f);
    bulletTravelTimer.StartTimer(true);
    }
    if(fireRateTimer != null)
    {
    fireRateTimer.SetTimer(fireRate);
    fireRateTimer.StartTimer(true);
    canFire = false;
    }
    if(animator != null)
    {
    animator.SetTrigger("Fire");
    }
    Debug.Log("Hit Something.");
      }
      if(holdFire)
      {
       if(oof != null)
       {
         oof.SetHeldFire(true);
       }
       if(gunShootSFX != null)
       {
        gunShootSFX.Play();
       }
       if(lineRenderer != null)
       {
       lineRenderer.enabled = true;
       }
       if(animator != null)
       {
       animator.SetTrigger("Fire");
       }
       Debug.Log("Hit Something.");
      }
    }
    if(!canFire)
    {
      if(holdFire)
      {
         StopHoldingFire();
      }
      if(!holdFire)
      {
        if(gunNoAmmoSFX != null)
        {
         gunNoAmmoSFX.Play();
        }
      }
    }
   }
    public void SetFireAbility(bool set)
    {
      canFire = set;
    }
    public void SetFireAbility()
    {
      canFire = true;
    }
    public void SetLineVisibility()
    {
      lineRenderer.enabled = false;
    }
    public void StopHoldingFire()
    {
      if(gunShootSFX != null)
      {
       gunShootSFX.Stop();
      }
      if(oof != null)
      {
       oof.SetHeldFire(false);
      }
       if(lineRenderer != null)
       {
       lineRenderer.enabled = false;
       }
    }
  }
}