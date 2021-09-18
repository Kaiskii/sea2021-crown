using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager> {
  //Variable Declarations
  [SerializeField]
  AudioSource audioSource;
  SoundLibrary soundLibrary;

  float elapsedTime;
  int soundsPlayed;

  void Start()
  {
    soundLibrary = Resources.Load<SoundLibrary>("SoundLibrary");
    if(!soundLibrary){
      Debug.LogWarning("Failed to load Sound Library! Is it created?");
    }
  }

  void Update()
  {
    elapsedTime+= Time.deltaTime;
    if(elapsedTime>=soundLibrary.soundCapResetSpeed)
    {
      soundsPlayed = 0;
      elapsedTime = 0;
    }
  }
  
  // Play a single clip through the sound effects source.
  public void Play(string effect) {
    AudioClip clip = soundLibrary.GetClip(effect);
    if(!clip || soundsPlayed>soundLibrary.maxSounds) return;

    audioSource.clip = clip;
    audioSource.PlayOneShot(clip);

    soundsPlayed++;
  }

  public void PlayRandomPitch(string effect,float minPitch = 0.8f,float maxPitch = 1.2f) {
    AudioClip clip = soundLibrary.GetClip(effect);
    if(!clip || soundsPlayed>soundLibrary.maxSounds) return;

    audioSource.clip = clip;
    audioSource.pitch = Random.Range(minPitch, maxPitch);
    audioSource.PlayOneShot(clip);

    soundsPlayed++;
  }

  // Play a single clip through the music source.
  public void PlayMusic(string bgm) {
    AudioClip clip = soundLibrary.GetClip(bgm);
    if(!clip) return;

    audioSource.clip = clip;
    audioSource.PlayOneShot(clip);
  }
}
