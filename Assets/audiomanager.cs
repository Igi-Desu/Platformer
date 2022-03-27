using System;
using UnityEngine;

public class audiomanager : MonoBehaviour
{
    // Start is called before the first frame update
    public sound[] sounds;
    AudioSource audiosource;
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        foreach(sound element in sounds)
        {
            element.source = gameObject.AddComponent<AudioSource>();
            element.source.clip = element.clip;
            element.source.volume = element.volume;
            element.source.loop = element.looping;
        }
        Play("Theme");
    }

    public void Play(string name)
    {
        sound sound = Array.Find(sounds, sound => sound.name == name);
        if (sound == null)
        {
            Debug.LogError("REEEEEEEEEEE");
            return;
        }
        sound.source.Play();
    }
}
