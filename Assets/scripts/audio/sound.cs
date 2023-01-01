using UnityEngine;

[System.Serializable]
public class sound 
{
    public string name;
    public AudioClip clip;
    public float volume;
    [HideInInspector]
    public AudioSource source;
    public bool looping;
}
