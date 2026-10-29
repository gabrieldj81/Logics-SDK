//Created by: Cake Logic
using System;
using System.Collections;
using UnityEngine;

namespace CakeLogicSDK {
[RequireComponent(typeof(AudioSource))]
[DisallowMultipleComponent]
[AddComponentMenu("Cake Logic/Logic's SDK/func_musicmanager")]
public class func_musicmanager : MonoBehaviour
{
    [SerializeField] private string currentMusic;
    public float fadeSpeed = 0.5f;
    [SerializeField] private bool playOnAwake;
    private AudioSource musicPlayer;
    private float volumeMemory = 1f;
    private Coroutine currentCoroutine;
    private Coroutine altCoroutine;
    void Awake()
    {
      musicPlayer = gameObject.GetComponent<AudioSource>();
      musicPlayer.loop = true;
      musicPlayer.playOnAwake = false;
      if(playOnAwake)
      {
        SetTrack(currentMusic);
        StartMusic(false);
      }
    }
  public void SetTrack(string input)
  {
   if(!musicPlayer.isPlaying)
   {
    musicPlayer.clip = Resources.Load<AudioClip>("Music/" + input);
    currentMusic = "Music/" + input;
   }
   else
   {
    Debug.LogError("Cannot call set track while Track is playing! Either stop the music and try again, or use SwitchTrack()");
   }
  }
  public void SwitchTrackWithFade(string input, bool playAfterSwitch)
  {
    StopConcurrentCoroutines();
    if(musicPlayer.isPlaying)
    {
      currentCoroutine = StartCoroutine(FadeVolAlt(fadeSpeed, 0, true, false, playAfterSwitch, input));
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
  public void SetVolume(float newVolume)
  {
   musicPlayer.volume = Mathf.Clamp01(newVolume);
   volumeMemory = Mathf.Clamp01(newVolume);
  }
  public void StartMusic(bool fade)
  {
    StopConcurrentCoroutines();
    musicPlayer.volume = 0;
    musicPlayer.Play();
    if(fade)
    {
      currentCoroutine = StartCoroutine(FadeVol(fadeSpeed, volumeMemory, false, false));
    }
    else
    {
     musicPlayer.volume = volumeMemory;
    }
  }
  public void StopMusic(bool fade)
  {
    StopConcurrentCoroutines();
    if(fade)
    {
      currentCoroutine = StartCoroutine(FadeVol(fadeSpeed, 0, true, false));
    }
    else
    {
      musicPlayer.Stop();
    }
  }
  public void PauseMusic(bool fade)
  {
    StopConcurrentCoroutines();
    if(fade)
    {
      currentCoroutine = StartCoroutine(FadeVol(fadeSpeed, 0, false, true));
    }
    else
    {
      musicPlayer.Pause();
    }
  }
  public void UnPauseMusic(bool fade)
  {
    StopConcurrentCoroutines();
    musicPlayer.volume = 0;
    musicPlayer.UnPause();
    if(fade)
    {
      currentCoroutine = StartCoroutine(FadeVol(fadeSpeed, volumeMemory, false, false));
    }
    else
    {
     musicPlayer.volume = volumeMemory;
    }
  }
  private IEnumerator FadeVol(float speed, float input, bool stopMusicManual, bool pauseMusicManual)
  {
    float newVolume = Mathf.Clamp01(input);
    if(Mathf.Approximately(newVolume, musicPlayer.volume))
    {
     yield break;
    }
    if(newVolume > musicPlayer.volume)
    {
        while(musicPlayer.volume < newVolume)
        {
          musicPlayer.volume += Time.deltaTime * speed;
          yield return null;
        }
        musicPlayer.volume = newVolume;
    }
    if(newVolume < musicPlayer.volume)
    {
        while(musicPlayer.volume > newVolume)
        {
          musicPlayer.volume -= Time.deltaTime * speed;
          yield return null;
        }
        musicPlayer.volume = newVolume;
    }
    if (stopMusicManual) musicPlayer.Stop();
    if (pauseMusicManual) musicPlayer.Pause();
  }
  private IEnumerator FadeVolAlt(float speed, float input, bool stopMusicManual, bool pauseMusicManual, bool playAfterSwitch, string newTrack)
  {
    altCoroutine = StartCoroutine(FadeVol(speed, input, stopMusicManual, pauseMusicManual));
    yield return altCoroutine;
    SetTrack(newTrack);
    if(playAfterSwitch)
    {
      StartMusic(true);
    }
  }
  private void StopConcurrentCoroutines()
  {
    if(currentCoroutine != null)
    {
      StopCoroutine(currentCoroutine);
    }
    if(altCoroutine != null)
    {
      StopCoroutine(altCoroutine);
    }
  }
}
}