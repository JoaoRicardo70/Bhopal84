using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class scrSons
{
    public AudioClip clip;

    public string name;
    [Range(0f,1f)]
    public float volume;
    [Range(0.1f,3f)]
    public float pitch;
    
    [Range(0f,1f)]
    public float spatialBlend;

    public bool loop;

    [HideInInspector]
    public AudioSource source;


}
