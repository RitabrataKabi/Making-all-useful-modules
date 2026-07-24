using Unity.VisualScripting;
using UnityEngine;

public class sfx_manager : MonoBehaviour
{
    [SerializeField] private SoundBase[] sounds;


    public static sfx_manager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        { 
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        for (int i = 0; i <= sounds.Length - 1; i++)
        {
            AudioSource _tempSource = this.AddComponent<AudioSource>();
            _tempSource.clip = sounds[i].clip;
            _tempSource.volume = sounds[i].volume;
            _tempSource.pitch = sounds[i].pitch;
            sounds[i].source = _tempSource;
            continue;
        }
    }

    public void PlaySound(string soundName)
    {
        SoundBase sound = System.Array.Find(sounds, s => s.name == soundName);
        if (sound != null && sound.source != null)
        {
            sound.source.Play();
        }
        else
        {
#if UNITY_EDITOR
            Debug.LogWarning($"Sound '{soundName}' not found or AudioSource is null.");
#endif
        }
    }
}

