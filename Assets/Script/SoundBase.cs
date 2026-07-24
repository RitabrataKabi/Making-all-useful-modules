using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class SoundBase
{
    public string name = "No Sound";
    public AudioClip clip = null;

    public float volume = 1.0f;
    public float pitch = 1.0f;
    //[Range(0f, 1f)] public float spatialBend = 0f;

    //public bool playOnAwake = false;
    //public bool loop = false;

    internal AudioSource source;
}
