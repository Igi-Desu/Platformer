using System;
using UnityEngine;

public class audiomanager : MonoBehaviour
{
    
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
    //find and play the sound we want 
    public void Play(string name)
    {
        sound sound = Array.Find(sounds, sound => sound.name == name);
        if (sound == null)
        {
            return;
        }
        sound.source.Play();
    }
}
