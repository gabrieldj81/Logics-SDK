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
   try
   {
    if(musicPlayer.isPlaying)
    {
        StopMusic(true);
    }
    musicPlayer.clip = Resources.Load<AudioClip>("Music/" + input);
    currentMusic = "Music/" + input;
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
      StartCoroutine(FadeVol(0.5f, volumeMemory));
    }
    else
    {
     musicPlayer.volume = 1f;
    }
  }
  public void StopMusic(bool fade)
  {
    if(fade)
    {
      StartCoroutine(FadeVol(0.5f, 0));
    }
    musicPlayer.Stop();
  }
  public void PauseMusic(bool fade)
  {
    if(fade)
    {
      StartCoroutine(FadeVol(0.5f, 0));
    }
    musicPlayer.Pause();
  }
  public void UnPauseMusic(bool fade)
  {
    musicPlayer.volume = 0;
    musicPlayer.UnPause();
    if(fade)
    {
      StartCoroutine(FadeVol(0.5f, volumeMemory));
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
  private IEnumerator FadeVol(float speed, float input)
  {
    float newVolume = NormalizeFloat(input);
    if(newVolume == musicPlayer.volume)
    {
     yield return null;
    }
    if(newVolume > musicPlayer.volume)
    {
        while(musicPlayer.volume < newVolume)
        {
          musicPlayer.volume += Time.deltaTime * speed;
        }
        musicPlayer.volume = newVolume;
        volumeMemory = newVolume;
        yield return null;
    }
    if(newVolume < musicPlayer.volume)
    {
        while(musicPlayer.volume > newVolume)
        {
          musicPlayer.volume -= Time.deltaTime * speed;
        }
        musicPlayer.volume = newVolume;
        volumeMemory = newVolume;
        yield return null;
    }
  }
}
}
