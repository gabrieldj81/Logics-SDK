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
    if(musicPlayer.isPlaying)
    {
      StartCoroutine(FadeVolAlt(0.5f, 0, true, false, playAfterSwitch, input));
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
    musicPlayer.clip = Resources.Load<AudioClip>("Music/" + input);
    currentMusic = "Music/" + input;
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
   musicPlayer.volume = NormalizeFloat(newVolume);
   volumeMemory = NormalizeFloat(newVolume);
  }
  public void StartMusic(bool fade)
  {
    musicPlayer.volume = 0;
    musicPlayer.Play();
    if(fade)
    {
      StartCoroutine(FadeVol(0.5f, volumeMemory, false, false));
    }
    if(!fade)
    {
     musicPlayer.volume = volumeMemory;
    }
  }
  public void StopMusic(bool fade)
  {
    if(fade)
    {
      StartCoroutine(FadeVol(0.5f, 0, true, false));
    }
    if(!fade)
    {
      musicPlayer.Stop();
    }
  }
  public void PauseMusic(bool fade)
  {
    if(fade)
    {
      StartCoroutine(FadeVol(0.5f, 0, false, true));
    }
    if(!fade)
    {
      musicPlayer.Pause();
    }
  }
  public void UnPauseMusic(bool fade)
  {
    musicPlayer.volume = 0;
    musicPlayer.UnPause();
    if(fade)
    {
      StartCoroutine(FadeVol(0.5f, volumeMemory, false, false));
    }
    else
    {
     musicPlayer.volume = 1f;
    }
  }
  private float NormalizeFloat(float value)
  {
    if(value <= 1f)
    {
      return value;
    }
    if(value > 1f)
    {
      return 1f;
    }
    return 1;
  }
  private IEnumerator FadeVol(float speed, float input, bool stopMusicManual, bool pauseMusicManual)
  {
    bool stopExecution = false;
    float newVolume = NormalizeFloat(input);
    if(newVolume == musicPlayer.volume)
    {
     stopExecution = true;
    }
    if(newVolume > musicPlayer.volume && !stopExecution)
    {
        while(musicPlayer.volume < newVolume)
        {
          musicPlayer.volume += Time.deltaTime * speed;
          yield return null;
        }
        musicPlayer.volume = newVolume;
        stopExecution = true;
    }
    if(newVolume < musicPlayer.volume && !stopExecution)
    {
        while(musicPlayer.volume > newVolume)
        {
          musicPlayer.volume -= Time.deltaTime * speed;
          yield return null;
        }
        musicPlayer.volume = newVolume;
    }
    if(stopMusicManual)
    {
      musicPlayer.Stop();
    }
    if(pauseMusicManual)
    {
      musicPlayer.Pause();
    }
  }
  private IEnumerator FadeVolAlt(float speed, float input, bool stopMusicManual, bool pauseMusicManual, bool playAfterSwitch, string newTrack)
  {
    yield return StartCoroutine(FadeVol(speed, input, stopMusicManual, pauseMusicManual));
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
      throw new ArgumentException("Failed to load track! Track is either invalid or does not exist!");
    }
  }
}
}
