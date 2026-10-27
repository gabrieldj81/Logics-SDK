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
<<<<<<< HEAD
<<<<<<< HEAD
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
=======
=======
>>>>>>> 2bd13ba94316402adad6308981968d89557da00c
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
<<<<<<< HEAD
>>>>>>> 2bd13ba94316402adad6308981968d89557da00c
=======
>>>>>>> 2bd13ba94316402adad6308981968d89557da00c
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
<<<<<<< HEAD
<<<<<<< HEAD
      StartCoroutine(FadeVol(0.5f, volumeMemory, false, false));
    }
    if(!fade)
    {
     musicPlayer.volume = volumeMemory;
=======
=======
>>>>>>> 2bd13ba94316402adad6308981968d89557da00c
      StartCoroutine(FadeVol(0.5f, volumeMemory));
    }
    else
    {
     musicPlayer.volume = 1f;
<<<<<<< HEAD
>>>>>>> 2bd13ba94316402adad6308981968d89557da00c
=======
>>>>>>> 2bd13ba94316402adad6308981968d89557da00c
    }
  }
  public void StopMusic(bool fade)
  {
    if(fade)
    {
<<<<<<< HEAD
<<<<<<< HEAD
      StartCoroutine(FadeVol(0.5f, 0, true, false));
    }
    if(!fade)
    {
      musicPlayer.Stop();
    }
=======
      StartCoroutine(FadeVol(0.5f, 0));
    }
    musicPlayer.Stop();
>>>>>>> 2bd13ba94316402adad6308981968d89557da00c
=======
      StartCoroutine(FadeVol(0.5f, 0));
    }
    musicPlayer.Stop();
>>>>>>> 2bd13ba94316402adad6308981968d89557da00c
  }
  public void PauseMusic(bool fade)
  {
    if(fade)
    {
<<<<<<< HEAD
<<<<<<< HEAD
      StartCoroutine(FadeVol(0.5f, 0, false, true));
    }
    if(!fade)
    {
      musicPlayer.Pause();
    }
=======
      StartCoroutine(FadeVol(0.5f, 0));
    }
    musicPlayer.Pause();
>>>>>>> 2bd13ba94316402adad6308981968d89557da00c
=======
      StartCoroutine(FadeVol(0.5f, 0));
    }
    musicPlayer.Pause();
>>>>>>> 2bd13ba94316402adad6308981968d89557da00c
  }
  public void UnPauseMusic(bool fade)
  {
    musicPlayer.volume = 0;
    musicPlayer.UnPause();
    if(fade)
    {
<<<<<<< HEAD
<<<<<<< HEAD
      StartCoroutine(FadeVol(0.5f, volumeMemory, false, false));
=======
      StartCoroutine(FadeVol(0.5f, volumeMemory));
>>>>>>> 2bd13ba94316402adad6308981968d89557da00c
=======
      StartCoroutine(FadeVol(0.5f, volumeMemory));
>>>>>>> 2bd13ba94316402adad6308981968d89557da00c
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
<<<<<<< HEAD
<<<<<<< HEAD
  private IEnumerator FadeVol(float speed, float input, bool stopMusicManual, bool pauseMusicManual)
  {
    bool stopExecution = false;
    float newVolume = NormalizeFloat(input);
    if(newVolume == musicPlayer.volume)
    {
     stopExecution = true;
    }
    if(newVolume > musicPlayer.volume && !stopExecution)
=======
=======
>>>>>>> 2bd13ba94316402adad6308981968d89557da00c
  private IEnumerator FadeVol(float speed, float input)
  {
    float newVolume = NormalizeFloat(input);
    if(newVolume == musicPlayer.volume)
    {
     yield return null;
    }
    if(newVolume > musicPlayer.volume)
<<<<<<< HEAD
>>>>>>> 2bd13ba94316402adad6308981968d89557da00c
=======
>>>>>>> 2bd13ba94316402adad6308981968d89557da00c
    {
        while(musicPlayer.volume < newVolume)
        {
          musicPlayer.volume += Time.deltaTime * speed;
<<<<<<< HEAD
<<<<<<< HEAD
          yield return null;
        }
        musicPlayer.volume = newVolume;
        stopExecution = true;
    }
    if(newVolume < musicPlayer.volume && !stopExecution)
=======
=======
>>>>>>> 2bd13ba94316402adad6308981968d89557da00c
        }
        musicPlayer.volume = newVolume;
        volumeMemory = newVolume;
        yield return null;
    }
    if(newVolume < musicPlayer.volume)
<<<<<<< HEAD
>>>>>>> 2bd13ba94316402adad6308981968d89557da00c
=======
>>>>>>> 2bd13ba94316402adad6308981968d89557da00c
    {
        while(musicPlayer.volume > newVolume)
        {
          musicPlayer.volume -= Time.deltaTime * speed;
<<<<<<< HEAD
<<<<<<< HEAD
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
=======
=======
>>>>>>> 2bd13ba94316402adad6308981968d89557da00c
        }
        musicPlayer.volume = newVolume;
        volumeMemory = newVolume;
        yield return null;
<<<<<<< HEAD
>>>>>>> 2bd13ba94316402adad6308981968d89557da00c
=======
>>>>>>> 2bd13ba94316402adad6308981968d89557da00c
    }
  }
}
}
