//Created by: Cake Logic
using System;
using System.Collections;
using UnityEngine;

namespace CakeLogicSDK {
[DisallowMultipleComponent]
[AddComponentMenu("Cake Logic/Logic's SDK/func_musicmanager")]
public class func_musicmanager : MonoBehaviour
{
    [SerializeField] private string currentMusic; //Set this to the path of the music where it is located in the "Resources" folder
    [SerializeField] private bool playOnAwake;
    private Coroutine currentCoroutine;
    private Coroutine altCoroutine;
    private bool isRunningAlt = false;
    private AudioSource musicPlayer;
    private float volumeMemory = 1f;
    void Awake()
    {
        if(musicPlayer == null)
        {
            musicPlayer = gameObject.GetComponent<AudioSource>();
            if(musicPlayer == null)
            {
              gameObject.AddComponent<AudioSource>();   
              musicPlayer = gameObject.GetComponent<AudioSource>();       
            }
            musicPlayer.loop = true;
            musicPlayer.playOnAwake = false;
            if(playOnAwake)
            {
              SetTrack(currentMusic);
              StartMusic(false);
            }
        }
    }
  public void SetTrack(string input)
  {
   if(!musicPlayer.isPlaying)
   {
   try
   {
    musicPlayer.clip = Resources.Load<AudioClip>("Music/" + input);
    currentMusic = input;
   }
   catch
   {
    throw new ArgumentException("Failed to load track! Track is either invalid or does not exist!");
   }
   }
   else
   {
    Debug.LogError("Cannot call set track while Track is playing! Either stop the music and try again, or use SwitchTrack()");
   }
  }
  public void SwitchTrackWithFade(string input, bool playAfterSwitch)
  {
    StopCurrentCoroutine();
    if(musicPlayer.isPlaying)
    {
      isRunningAlt = true;
      currentCoroutine = StartCoroutine(FadeVolAlt(0.5f, 0, true, false, playAfterSwitch, input));
    }
    else
    {
      SetTrack(input);
      if(playAfterSwitch)
      {
        StartMusic(true);
      }
    }
  }
  public void SwitchTrack(string input, bool playAfterSwitch)
  {
   try
   {
    if(musicPlayer.isPlaying)
    {
     StopMusic(false);
    }
    currentMusic = "Music/" + input;
    musicPlayer.clip = Resources.Load<AudioClip>(currentMusic);
    if(playAfterSwitch)
    {
      StartMusic(false);
    }
   }
   catch
   {
    throw new ArgumentException("Failed to load track! Track is either invalid or does not exist!");
   }
  }
  public void SetVolume(float newVolume)
  {
   musicPlayer.volume = Mathf.Clamp01(newVolume);
   volumeMemory = Mathf.Clamp01(newVolume);
  }
  public void StartMusic(bool fade)
  {
    StopCurrentCoroutine();
    musicPlayer.volume = 0;
    musicPlayer.Play();
    if(fade)
    {
      currentCoroutine = StartCoroutine(FadeVol(0.5f, volumeMemory, false, false));
    }
    else
    {
     musicPlayer.volume = volumeMemory;
    }
  }
  public void StopMusic(bool fade)
  {
    StopCurrentCoroutine();
    if(fade)
    {
      currentCoroutine = StartCoroutine(FadeVol(0.5f, 0, true, false));
    }
    else
    {
      musicPlayer.Stop();
    }
  }
  public void PauseMusic(bool fade)
  {
    StopCurrentCoroutine();
    if(fade)
    {
      currentCoroutine = StartCoroutine(FadeVol(0.5f, 0, false, true));
    }
    else
    {
      musicPlayer.Pause();
    }
  }
  public void UnPauseMusic(bool fade)
  {
    StopCurrentCoroutine();
    musicPlayer.volume = 0;
    musicPlayer.UnPause();
    if(fade)
    {
      currentCoroutine = StartCoroutine(FadeVol(0.5f, volumeMemory, false, false));
    }
    else
    {
     musicPlayer.volume = 1f;
    }
  }
  private IEnumerator FadeVol(float speed, float input, bool stopMusicManual, bool pauseMusicManual)
  {
    float newVolume = Mathf.Clamp01(input);
    if(newVolume == musicPlayer.volume)
    {
     yield break;
    }
    while (!Mathf.Approximately(musicPlayer.volume, input))
    {
    musicPlayer.volume = Mathf.MoveTowards(musicPlayer.volume, input, speed);
    yield return null;
    }
    if (stopMusicManual) musicPlayer.Stop();
    if (pauseMusicManual) musicPlayer.Pause();
  }
  private IEnumerator FadeVolAlt(float speed, float input, bool stopMusicManual, bool pauseMusicManual, bool playAfterSwitch, string newTrack)
  {
    altCoroutine = StartCoroutine(FadeVol(speed, input, stopMusicManual, pauseMusicManual));
    yield return altCoroutine;
    SetTrackAndPlay(playAfterSwitch, newTrack);
  }
  private void SetTrackAndPlay(bool playAfterSwitch, string newTrack)
  {
    try
    {
    musicPlayer.clip = Resources.Load<AudioClip>("Music/" + newTrack);
    currentMusic = "Music/" + newTrack;
    if(playAfterSwitch)
    {
      StartMusic(true);
    }
    }
    catch
    {
      isRunningAlt = false;
      throw new ArgumentException("Failed to load track! Track is either invalid or does not exist!");
    }
    isRunningAlt = false;
  }
  private void StopCurrentCoroutine()
  {
    if(isRunningAlt)
    {
      StopCoroutine(altCoroutine);
      altCoroutine = null;
    }
    if (currentCoroutine != null)
    {
      StopCoroutine(currentCoroutine);
      currentCoroutine = null;
    }
  }
}
}