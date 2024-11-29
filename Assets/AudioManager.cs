using System;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
   public string name;
   public AudioClip clip;
   [Range(0f, 1f)] public float volume = 1f;
   [Range(0.1f, 3f)] public float pitch = 1f;
   public bool loop;
}

public class AudioManager : MonoBehaviour
{
   public static AudioManager instance;

   public Sound[] sounds;

   private void Awake()
   {
      if (instance == null)
      {
         instance = this;
         DontDestroyOnLoad(gameObject);
      }
      else
      {
         Destroy(gameObject);
      }

      foreach (Sound s in sounds)
      {
         //s.audioSource = gameObject.AddComponent<AudioSource>();
         //s.audioSource.clip = s.clip;
         //s.audioSource.volume = s.volume;
         //s.audioSource.pitch = s.pitch;
         //s.audioSource.loop = s.loop;
      }
   }

   public void Play(string soundName)
   {
      Sound s = Array.Find(sounds, sound => sound.name == soundName);
      if (s != null)
      {
         //s.audioSource.Play();
      }
      else
      {
         Debug.LogWarning("Sound: " + soundName + " not found!");
      }
   }
}